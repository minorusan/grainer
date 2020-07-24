using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
    using UnityEditor;

#endif

public class InputVisualizeBehaviour : MonoBehaviour
{
    private InputHistory currentHistory;
    private int currentIndex;
    private float startTimeStamp;
    private float currentTime;
    private MovementBehaviour mover;
    public GameObject Mark;

    public UnityEvent OnFinished;

    public void Visualize()
    {
        currentHistory = LevelsStorage.GetLevelInputHistory(AppState.GameplayLevelNumber);
        Mark.transform.DOKill();
        Mark.transform.position = AreaHelper.StartPosition;
        currentIndex = 0;
        Mark.transform.DOScale(1f, 0.5f);
        Move();
    }

    public void Move()
    {
        var last = currentIndex >= currentHistory.Inputs.Count;
        var nextPosition = last ? AreaHelper.EndPosition : currentHistory.Inputs[currentIndex].InputPosition;
        var position = nextPosition.ToPosition();
        var cellPosition = AreaHelper.GetCell(position).transform.position;
        Mark.transform.DOLookAt(cellPosition, 1f).OnComplete(() =>
        {
            var animationTime = Vector3.Distance(Mark.transform.position, cellPosition) * 0.5f;
            if (last)
            {
                Mark.transform.DOScale(0f, animationTime).OnComplete(OnFinished.Invoke);
            }
            Mark.transform.DOMove(cellPosition, animationTime).OnComplete(() =>
            {
                Move();
            });
            currentIndex++;
        });
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(InputVisualizeBehaviour))]
public class Visualizer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Visualize"))
        {
            (target as InputVisualizeBehaviour).Visualize();
        }
    }
}
#endif