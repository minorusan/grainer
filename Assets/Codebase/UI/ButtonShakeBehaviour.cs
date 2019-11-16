using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonShakeBehaviour : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.rotation = Quaternion.identity;
            transform.DOShakeRotation(0.2f, Vector3.one * 7f);
        });
    }
}