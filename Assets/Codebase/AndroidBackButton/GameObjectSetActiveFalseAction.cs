using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameObjectSetActiveFalseAction",menuName = "BackButtonActions/GameObjectSetActiveFalseAction")]
public class GameObjectSetActiveFalseAction : OnPressButtonActionBase
{
    public override void OnPressBackButton(GameObject o)
    {
        o.SetActive(false);
    }
}
