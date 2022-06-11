using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Position position;

    public void SetPosition(Position position)
    {
        SetPosition(position.x, position.y);
    }

    public void SetPosition(int x, int y)
    {
        this.position.x = x;
        this.position.y = y;
    }

    public bool CanMoveHere(Position source, Position destination, BoardData boardData)
    {
        if(source.x == destination.x && source.y + 1 == destination.y && boardData.GetChar(destination) == '.')
        {
            return true;
        }

        return false;
    }

    public bool CanMoveHere(Position destination, BoardData boardData)
    {
        return CanMoveHere(position, destination, boardData);
    }
}
