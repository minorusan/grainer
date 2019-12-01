using DG.Tweening;
using UnityEngine;

public class MoveToTransformBehaviour : MonoBehaviour
{
    public Transform MoveThis;
    public float Duration;
    public Ease Ease;

    public void Move()
    {
        var tween = MoveThis.DORotate(transform.eulerAngles, Duration).SetEase(Ease);
        tween.timeScale = 1f;
        var tween1 = MoveThis.DOMove(transform.position, Duration).SetEase(Ease);
        tween1.timeScale = 1f;
    }
}