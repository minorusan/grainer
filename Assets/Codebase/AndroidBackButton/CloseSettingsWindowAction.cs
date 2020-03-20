using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CloseSettingsWindowAction",menuName = "BackButtonActions/CloseSettingsWindowAction")]
public class CloseSettingsWindowAction : OnPressButtonActionBase
{
    public override void OnPressBackButton(GameObject o)
    {
        var obj = GameObject.Find("MAIN_ANCHOR");
        var moveToTransformBehaviour = obj.GetComponent<MoveToTransformBehaviour>();
        moveToTransformBehaviour.Move();
    }
}
