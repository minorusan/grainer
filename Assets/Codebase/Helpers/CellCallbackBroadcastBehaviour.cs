using System.Collections.Generic;
using UnityEngine;

public class CellCallbackBroadcastBehaviour : DebuggableBehaviour
{
    private readonly Vector3 debugGizmoSize = new Vector3(1f, 2f, 1f);
    private Dictionary<int, GameEntityMovement> objectsMoveHistory = new Dictionary<int, GameEntityMovement>();

    protected override void Awake()
    {
        base.Awake();
        MovementBehaviour.WillLeaveCell += OnWillLeaveCell;
        MovementBehaviour.LeftCell += OnLeftCell;
        MovementBehaviour.WillEnterCell += OnWillEnterCell;
        MovementBehaviour.EnteredCell += OnEnteredCell;
    }

    private void OnWillLeaveCell(GameObject sender, Vector3 cellPosition)
    {
        if (objectsMoveHistory.TryGetValue(sender.GetInstanceID(), out var entityMovement))
        {
            entityMovement.WillLeavePosition = cellPosition;
            cellPosition.ToColorDefinition().InvokeEvents(cellPosition.ToCellGameObject(), CellEventType.WillLeave);
        }
        else
        {
            objectsMoveHistory.Add(sender.GetInstanceID(), new GameEntityMovement());
        }
    }

    private void OnLeftCell(GameObject sender, Vector3 cellPosition)
    {
        if (objectsMoveHistory.TryGetValue(sender.GetInstanceID(), out var entityMovement))
        {
            entityMovement.LeftPosition = cellPosition;
            cellPosition.ToColorDefinition().InvokeEvents(cellPosition.ToCellGameObject(), CellEventType.Left);
        }
        else
        {
            objectsMoveHistory.Add(sender.GetInstanceID(), new GameEntityMovement());
        }
    }

    private void OnWillEnterCell(GameObject sender, Vector3 cellPosition)
    {
        if (objectsMoveHistory.TryGetValue(sender.GetInstanceID(), out var entityMovement))
        {
            entityMovement.WillEnterPosition = cellPosition;
            cellPosition.ToColorDefinition().InvokeEvents(cellPosition.ToCellGameObject(), CellEventType.WillEnter);
        }
        else
        {
            objectsMoveHistory.Add(sender.GetInstanceID(), new GameEntityMovement());
        }
    }

    private void OnEnteredCell(GameObject sender, Vector3 cellPosition)
    {
        if (objectsMoveHistory.TryGetValue(sender.GetInstanceID(), out var entityMovement))
        {
            entityMovement.EnteredPosition = cellPosition;
            cellPosition.ToColorDefinition().InvokeEvents(cellPosition.ToCellGameObject(), CellEventType.Entered);
        }
        else
        {
            objectsMoveHistory.Add(sender.GetInstanceID(), new GameEntityMovement());
        }
    }

    protected override void GizmosDebug()
    {
        foreach (var valuePair in objectsMoveHistory)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(valuePair.Value.LeftPosition, debugGizmoSize);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(valuePair.Value.EnteredPosition, debugGizmoSize);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(valuePair.Value.WillEnterPosition, debugGizmoSize);
        }
    }
}