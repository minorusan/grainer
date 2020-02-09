using UnityEngine;

[CreateAssetMenu(fileName = "New localizable string", menuName = "Grainer/Localisation/String")]
public class LocalizableString : LocalizableStringBase
{
    public string EnglishString;
    public string RussianString;


    protected override string GetString()
    {
        return Application.systemLanguage == SystemLanguage.Russian ? RussianString : EnglishString;
    }
}
