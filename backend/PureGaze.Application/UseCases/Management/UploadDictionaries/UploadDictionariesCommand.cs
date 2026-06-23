using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Management.UploadDictionaries;

public sealed record UploadDictionariesCommand : IRequest
{
    public static T ToEntity<T>(BaseDictionaryDto dictionary)
        where T : BaseDictionaryEntity, new()
        => new()
        {
            Id = dictionary.Id,
            Translation = dictionary.Translation,
            Value = dictionary.Value,
            OrderValue = dictionary.OrderValue
        };

    public static void Update(BaseDictionaryEntity target, BaseDictionaryDto source)
    {
        target.Id = source.Id;
        target.Translation = source.Translation;
        target.Value = source.Value;
        target.OrderValue = source.OrderValue;
    }
}
