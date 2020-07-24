using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Codebase.Input;
using UnityEngine;

public static class ResourceHelper 
{
    public static InputProviderBase GetProvider(bool useRecords = true)
    {
        var inputProviders = Resources.LoadAll<InputProviderBase>(Strings.INPUT_PROVIDERS_PATH);
        if (useRecords)
        {
            return inputProviders.FirstOrDefault(x=>x is RecordInputProvisionBehaviour);
        }
        InputProviderBase required = inputProviders.FirstOrDefault(x=>x is DesktopInputProvisionBehaviour);
        
#if !UNITY_EDITOR
        required = inputProviders.FirstOrDefault(x=>x is SwipeInputProviderBehaviour);
#endif
        return required;
    }
}
