using PureGaze.Domain.Entities;
using PureGaze.Application.Contracts.Integrations.Hrm;

namespace PureGaze.Application.Extensions;

public static class DictionaryExtensions
{
    public static T ToEntity<T>(this BaseDictionaryDto dto) 
        where T : BaseDictionaryEntity, new() 
        => new ()
        {
            Id = dto.Id,
            Translation = dto.Translation, 
            Value = dto.Value, 
            OrderValue = dto.OrderValue
        };
    
    public static void Update(this BaseDictionaryEntity entity, BaseDictionaryDto dto)
    {
        entity.Id = dto.Id;
        entity.Translation = dto.Translation;
        entity.Value = dto.Value;
        entity.OrderValue = dto.OrderValue;
    }
}

