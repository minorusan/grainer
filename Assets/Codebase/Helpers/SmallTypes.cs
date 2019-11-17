using System;
using UnityEngine;

#region Delegates

public delegate void DirectionChangedHandler(GameObject sender, DirectionChangedEventArgs changedEventArgs);
public delegate void CellCalbackHandler(GameObject sender, Vector3 cellPosition);

#endregion

#region Enums

public enum CombinedPropDirection
{
    Default, Horizontal, Vertical, CornerUpLeft, CornerUpRight, CornerDownLeft, CornerDownRight, EndingDown, EndingUp, EndingLeft, EndingRight
}

public enum CellEventType
{
    WillEnter, Entered, WillLeave, Left
}

public enum GameEntityType
{
    Player
}

public enum MovementDirection
{
    None, Left, Right, Up, Down
}

#endregion

#region EventArgs

public class DirectionChangedEventArgs : EventArgs
{
    private readonly MovementDirection previous, current;

    public MovementDirection Previous => previous;
    public MovementDirection Current => current;

    public DirectionChangedEventArgs(MovementDirection prev, MovementDirection cur)
    {
        previous = prev;
        current = cur;
    }
}

public class InputChangedEventArgs : EventArgs
{
    private readonly MovementDirection direction;
    private readonly float timeSinceLevelLoad;

    public MovementDirection Direction
    {
        get
        {
            return direction;
        }
    }

    public float TimeSinceLevelLoad
    {
        get
        {
            return timeSinceLevelLoad;
        }
    }

    public InputChangedEventArgs(MovementDirection dir, float time)
    {
        direction = dir;
        timeSinceLevelLoad = time;
    }
}

#endregion

#region Structs

public class GameEntityMovement
{
    public Vector3 WillLeavePosition, WillEnterPosition, EnteredPosition, LeftPosition;
}

public struct Position
{
    private readonly int x, y;

    public int X => x;

    public int Y => y;

    public Position(float x, float y)
    {
        this.x = Mathf.RoundToInt(x);
        this.y = Mathf.RoundToInt(y);
    }

    public static bool operator ==(Position obj1, Position obj2)
    {
        return obj1.X == obj2.X && obj1.Y == obj2.Y;
    }

    public static bool operator !=(Position obj1, Position obj2)
    {
        return obj1.X != obj2.X || obj1.Y != obj2.Y;
    }
}

#endregion