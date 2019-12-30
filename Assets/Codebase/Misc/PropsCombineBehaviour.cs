using System;
using UnityEngine;
using System.Collections.Generic;

public class PropsCombineBehaviour : DebuggableBehaviour
{
    private List<Position> positions;
    public GameObject[] PropsDirections;
    public CombinedPropComponent PropComponent;

    public void Start()
    {
        InitializeCell();
    }

    private void InitializeCell()
    {
        Instantiate(PropsDirections[(int)GetDirection(GetSurroundingPositions())], PropComponent.transform);
    }

    private CombinedPropDirection GetDirection(List<Position> props)
    {
        var surroundingPropsCount = props.Count;
        var selfPosition = transform.position.ToPosition();
        switch (surroundingPropsCount)
        {
            case 1:
            {
                if (PropsDirections.Length <= 3)
                {
                    break;
                }
    
                if (props[0].Y > selfPosition.Y)
                {
                    return CombinedPropDirection.EndingDown;
                }
                
                if (props[0].Y < selfPosition.Y)
                {
                    return CombinedPropDirection.EndingUp;
                }
                
                if (props[0].X < selfPosition.X)
                {
                    return CombinedPropDirection.EndingLeft;
                }
                
                if (props[0].X > selfPosition.X)
                {
                    return CombinedPropDirection.EndingRight;
                }
                break;
                
            }
            case 2:
                {
                    if (props[0].X == props[1].X || props[0].Y == props[1].Y)
                    {
                        return selfPosition.X == props[0].X ? CombinedPropDirection.Vertical : CombinedPropDirection.Horizontal;
                    }

                    if (PropsDirections.Length > 3)
                    {
                        if ((props[0].X < selfPosition.X && props[1].Y < selfPosition.Y) || (props[1].X < selfPosition.X && props[0].Y < selfPosition.Y))
                        {
                            return CombinedPropDirection.CornerUpLeft;
                        }
                        
                        if ((props[0].X > selfPosition.X && props[1].Y < selfPosition.Y) || (props[1].X > selfPosition.X && props[0].Y < selfPosition.Y))
                        {
                            return CombinedPropDirection.CornerUpRight;
                        }
                        
                        if ((props[0].X < selfPosition.X && props[1].Y > selfPosition.Y) || (props[1].X < selfPosition.X && props[0].Y > selfPosition.Y))
                        {
                            return CombinedPropDirection.CornerDownLeft;
                        }
                        
                        if ((props[0].X > selfPosition.X && props[1].Y > selfPosition.Y) || (props[1].X > selfPosition.X && props[0].Y > selfPosition.Y))
                        {
                            return CombinedPropDirection.CornerDownRight;
                        }

                    }
                    else
                    {
                        return CombinedPropDirection.Default;
                    }
                    break;
                }
            default:
                break;
        }
        return CombinedPropDirection.Default;
    }

    private List<Position> GetSurroundingPositions()
    {
        var ownerPosition = transform.position;
        var point = ownerPosition.ToPosition();
        
        var crossPositions = new[] { ownerPosition + Vector3.forward,
            ownerPosition + Vector3.back, ownerPosition + Vector3.left,
            ownerPosition + Vector3.right};

        var props = new List<Position>();
        foreach (var position in crossPositions)
        {
            var cell = position.ToCellGameObject();
            if (cell != null)
            {
                var component = cell.GetComponentInChildren<CombinedPropComponent>();
                if (component != null && component.PropType == PropComponent.PropType)
                {
                    props.Add(position.ToPosition());
                }
            }
        }
       
        positions = props;
        return props;
    }

    protected override void GizmosDebug()
    {
        var point = transform.position.ToPosition();
        if (positions != null)
        {
            foreach (var item in positions)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(new Vector3(item.X, 0.5f, item.Y), Vector3.one);
            }
        }
    }
}