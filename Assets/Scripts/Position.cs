using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public int x, y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static bool operator ==(Position A, Position B)
    {
        return (A.x == B.x && A.y == B.y);
    }

    public static bool operator !=(Position A, Position B)
    {
        return (A.x != B.x || A.y != B.y);
    }

    public override string ToString()
    {
        return (x + 1).ToString() + "x" + (y + 1).ToString();
    }
}
