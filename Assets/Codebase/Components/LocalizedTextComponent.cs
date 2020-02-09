using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTextComponent : MonoBehaviour
{
    public LocalizableStringBase LocalizableString;

    private void OnEnable()
    {
        if (LocalizableString != null)
        {
            GetComponent<TextMeshProUGUI>().text = $"{LocalizableString.Value}";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = $"[NO_STRING_SET]";
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (LocalizableString != null)
            {
                GetComponent<TextMeshProUGUI>().text = $"[{LocalizableString.name}]";
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = $"[NO_STRING_SET]";
            }
        }
    }
}