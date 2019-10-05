using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceHelper 
{
    public static InputProviderBase GetProvider()
    {
        var inputProviders = Resources.LoadAll<InputProviderBase>(Strings.INPUT_PROVIDERS_PATH);
        InputProviderBase required = inputProviders.FirstOrDefault(x=>x is DesktopInputProvisionBehaviour);
#if !UNITY_EDITOR
        required = something else;
#endif
        return required;
    }
}
