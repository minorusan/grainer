using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New random localizable string", menuName = "Grainer/Localisation/Random string")]
public class RandomLocalizableString : LocalizableStringBase
{
    public string[] RandomEnglishStrings;
    public string[] RandomRussianStrings;

    protected override string GetString()
    {
        return Application.systemLanguage == SystemLanguage.Russian
            ? RandomRussianStrings[Random.Range(0, RandomRussianStrings.Length)]
            : RandomEnglishStrings[Random.Range(0, RandomEnglishStrings.Length)];
    }
}