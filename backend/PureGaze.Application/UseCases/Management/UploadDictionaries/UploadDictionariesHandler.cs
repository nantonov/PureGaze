using Microsoft.Extensions.DependencyInjection;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Management.UploadDictionaries;

public class UploadDictionariesHandler(
    IServiceScopeFactory scopeFactory,
    IHrmDataProvider hrmDataProvider)
    : IRequestHandler<UploadDictionariesCommand>
{
    public async Task Handle(UploadDictionariesCommand command, CancellationToken ct)
    {
        DictionariesDto? dictionaries = await hrmDataProvider.GetDictionariesAsync(ct);

        if (dictionaries == null)
            return;

        var managerialLevels =
            ProcessDictionaryAsync<ManagerialLevel>(dictionaries.ManagerialLevels, scopeFactory, ct);

        var professionalLevels =
            ProcessDictionaryAsync<ProfessionalLevel>(dictionaries.ProfessionalLevels, scopeFactory, ct);

        await Task.WhenAll(managerialLevels, professionalLevels);
    }

    private static async Task ProcessDictionaryAsync<T>(
        IList<BaseDictionaryDto> hrmDictionary,
        IServiceScopeFactory scopeFactory,
        CancellationToken ct)
        where T : BaseDictionaryEntity, new()
    {
        if (!hrmDictionary.Any())
            return;

        await using var scope = scopeFactory.CreateAsyncScope();
        IDictionaryRepository<T> dictionaryRepository =
            scope.ServiceProvider.GetRequiredService<IDictionaryRepository<T>>();

        IReadOnlyList<Guid> hrmIds = [.. hrmDictionary.Select(x => x.Id)];

        IDictionary<Guid, T> existingDictionary = await dictionaryRepository.GetByIdsAsync(hrmIds, ct);

        foreach (var item in hrmDictionary)
        {
            if (existingDictionary.TryGetValue(item.Id, out var existing))
                existing.Update(item);

            else
                await dictionaryRepository.AddAsync(item.ToEntity<T>(), ct);
        }

        await dictionaryRepository.SaveChangesAsync(ct);
    }
}