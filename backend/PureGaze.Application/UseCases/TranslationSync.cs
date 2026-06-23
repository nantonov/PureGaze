using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases;

public static class TranslationSync
{
    public static void Update<TTranslate>(
        ICollection<TTranslate> translates,
        Language language,
        Action<TTranslate> updateAction,
        Func<Language, TTranslate> createFunc,
        Func<TTranslate, bool> predicate)
        where TTranslate : class
    {
        TTranslate? translate = translates.FirstOrDefault(predicate);
        if (translate is not null)
        {
            updateAction(translate);
            return;
        }

        translates.Add(createFunc(language));
    }
}
