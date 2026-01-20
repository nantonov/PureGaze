using PureGaze.Domain.Enums;

namespace PureGaze.Application.Extensions;

public static class TranslationExtensions
{
    public static void SyncTranslate<TTranslate>(
        this ICollection<TTranslate> translates,
        Language language,
        Action<TTranslate> updateAction,
        Func<Language, TTranslate> createFunc,
        Func<TTranslate, bool> predicate)
        where TTranslate : class
    {
        var translate = translates.FirstOrDefault(predicate);
        if (translate != null)
        {
            updateAction(translate);
        }
        else
        {
            translates.Add(createFunc(language));
        }
    }
}
