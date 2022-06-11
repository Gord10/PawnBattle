using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Move
{
    public Position sourcePos, destinationPos;
    public char sourceChar, destinationChar;

    public Move(Position sourcePos, Position destinationPos, char sourceChar, char destinationChar = '.')
    {
        this.sourcePos = sourcePos;
        this.destinationPos = destinationPos;
        this.sourceChar = sourceChar;
        this.destinationChar = destinationChar;
    }
}
