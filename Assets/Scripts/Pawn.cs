using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Position position;
    public GameManager.PawnColor color;

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
        if(source.x == destination.x && boardData.GetChar(destination) == '.')
        {
            if(source.y + 1 == destination.y && color == GameManager.PawnColor.WHITE)
            {
                return true;
            }

            if (source.y - 1 == destination.y && color == GameManager.PawnColor.BLACK)
            {
                return true;
            }
        }

        if(Mathf.Abs(source.x - destination.x) == 1)
        {
            if(color == GameManager.PawnColor.WHITE && source.y + 1 == destination.y && boardData.GetChar(destination) == 'p')
            {
                return true;
            }

            if (color == GameManager.PawnColor.BLACK && source.y - 1 == destination.y && boardData.GetChar(destination) == 'P')
            {
                return true;
            }
        }

        return false;
    }

    public bool CanMoveHere(Position destination, BoardData boardData)
    {
        return CanMoveHere(position, destination, boardData);
    }
}
