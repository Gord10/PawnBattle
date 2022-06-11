using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public char[,] charMap;

    public BoardData()
    {
        charMap = new char[8, 8];
        int i, j;
        for(i = 0; i < 8; i++)
        {
            for(j = 0; j < 8; j++)
            {
                charMap[i, j] = '.';
            }
        }
    }

    public char GetChar(int x, int y)
    {
        return charMap[x, y];
    }

    public char GetChar(Position position)
    {
        return GetChar(position.x, position.y);
    }

    public void PlacePawnsOnDefaultPosition()
    {
        int i, j;
        for (i = 0; i < 8; i++)
        {
            charMap[i, 1] = 'P';
            charMap[i, 6] = 'p';
        }
    }

    public void MakeMove(Move move)
    {
        charMap[move.sourcePos.x, move.sourcePos.y] = '.';
        charMap[move.destinationPos.x, move.destinationPos.y] = move.sourceChar;
    }
}
