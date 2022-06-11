using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Singleton<AI>
{
    public enum Mode
    {
        RANDOM,
        MINIMAX
    }

    public Mode mode = Mode.RANDOM;
    private List<Move> possibleMoves;

    public void Run(BoardData boardData)
    {
        if(possibleMoves != null)
        {
            possibleMoves.Clear();
        }
        else
        {
            possibleMoves = new List<Move>();
        }

        int i, x, y;

        for(i = 0; i < PieceManager.Instance.blackPawns.Count; i++)
        {
            Pawn blackPawn = PieceManager.Instance.blackPawns[i];

            for(x = 0; x < 8; x++)
            {
                for(y = 0; y < 8; y++)
                {
                    Position destinationPos = new Position(x, y);
                    //print(destinationPos.ToString());
                    if(blackPawn.CanMoveHere(destinationPos, boardData))
                    {
                        Move possibleMove = new Move(blackPawn.position, destinationPos, 'p', boardData.GetChar(destinationPos));
                        //print(possibleMove.ToString());
                        possibleMoves.Add(possibleMove);
                    }
                }
            }
        }

        Move decidedMove;

        if(mode == Mode.RANDOM || true)
        {
            for (i = 0; i < possibleMoves.Count; i++)
            {
                if (possibleMoves[i].destinationChar == 'P')
                {
                    decidedMove = possibleMoves[i];
                    GameManager.Instance.MakeMove(decidedMove);
                    return;
                }
            }

            int randomIndex = Random.Range(0, possibleMoves.Count);

            int counter = 0;
            while(!IsMoveSafe(possibleMoves[randomIndex], boardData) && counter < possibleMoves.Count)
            {
                randomIndex++;
                randomIndex %= possibleMoves.Count;
                counter++;
            }

            decidedMove = possibleMoves[randomIndex];
        }

        GameManager.Instance.MakeMove(decidedMove);
    }

    bool IsMoveSafe(Move move, BoardData boardData)
    {
        if(move.destinationChar == 'P')
        {
            return true;
        }
        BoardData boardDataCopy = new BoardData();
        boardDataCopy.Init();

        int x, y;
        for(x = 0; x < 8; x++)
        {
            for(y = 0; y < 8; y++)
            {
                boardDataCopy.charMap[x, y] = boardData.charMap[x, y];
            }
        }

        boardDataCopy.MakeMove(move);
        int i;
        for(i = 0; i < PieceManager.Instance.whitePawns.Count; i++)
        {
            Pawn whitePawn = PieceManager.Instance.whitePawns[i];
            if(whitePawn.CanMoveHere(move.destinationPos, boardDataCopy))
            {
                print("Move " + move.ToString() + " is dangerous");
                return false;
            }
        }

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
