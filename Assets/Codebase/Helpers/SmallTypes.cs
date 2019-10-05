using System;

public enum MovementDirection
{
    None, Left, Right, Up, Down
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