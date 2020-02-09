using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalizableStringBase : ScriptableObject
{
    public string Value => GetString();

    protected abstract string GetString();
}
