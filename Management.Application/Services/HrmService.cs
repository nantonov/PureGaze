using Common.DAL;
using Common.Domain.Entities;
using Management.Application.Abstractions.Providers;
using Management.Application.Abstractions.Services;
using Management.Application.Contracts.Integrations.Hrm;
using Management.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Management.Application.Services;

public class HrmService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IHrmDataProvider hrmDataProvider)
    : IHrmService
{
    public async Task UploadEmployeesAsync(CancellationToken ct)
    {
        await Parallel.ForEachAsync(hrmDataProvider.GetEmployeesAsync(ct),
            new ParallelOptions {MaxDegreeOfParallelism = 3},
            async (hrmEmployees, ct) =>
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
                
                IList<int> hrmIds = [.. hrmEmployees.Select(x => x.Id)];
            
                IDictionary<int, Employee> existingEmployees = 
                    await dbContext
                        .Employees
                        .Where(x => hrmIds.Contains(x.Id))
                        .ToDictionaryAsync(x => x.Id, ct);
                           
                foreach (var hrmEmployee in hrmEmployees)
                {
                    if (!existingEmployees.TryGetValue(hrmEmployee.Id, out var existing))
                        await dbContext.Employees.AddAsync(hrmEmployee.ToEntity(), ct);
                
                    else if (existing.Hash != hrmEmployee.Hash)
                        existing.Update(hrmEmployee);
                }
            
                await dbContext.SaveChangesAsync(ct);
            });
    }
    
    public async Task UploadDictionariesAsync(CancellationToken ct)
    {
        DictionariesDto? dictionaries = await hrmDataProvider.GetDictionariesAsync(ct);

        if (dictionaries == null)
            return;
        
        var managerialLevels = 
            ProcessDictionaryAsync<ManagerialLevel>(dictionaries.ManagerialLevels, dbContextFactory, ct); 
        
        var professionalLevels = 
            ProcessDictionaryAsync<ProfessionalLevel>(dictionaries.ProfessionalLevels, dbContextFactory, ct); 
        
        var processConfirmationStatuses = 
            ProcessDictionaryAsync<ProcessConfirmationStatus>(dictionaries.ProcessConfirmationStatuses, dbContextFactory, ct);

        await Task.WhenAll(managerialLevels, professionalLevels, processConfirmationStatuses);
    }
    
    private static async Task ProcessDictionaryAsync<T>(
        IList<BaseDictionaryDto> hrmDictionary,
        IDbContextFactory<AppDbContext> dbContextFactory,
        CancellationToken ct) 
        where T : BaseDictionaryEntity, new()
    {
        if(!hrmDictionary.Any())
            return;
            
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);

        IList<Guid> hrmIds = [.. hrmDictionary.Select(x => x.Id)];
        
        IDictionary<Guid, T> existingDictionary = 
            await dbContext.Set<T>()
                .Where(x => hrmIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, ct);
        
        foreach (var item in hrmDictionary)
        {
            if (existingDictionary.TryGetValue(item.Id, out var existing))
                existing.Update(item);
                
            else 
                await dbContext.AddAsync(item.ToEntity<T>(), ct);
        }
            
        await dbContext.SaveChangesAsync(ct);
    }
}