using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Singleton<AI>
{
    public enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }

    public Difficulty difficulty = Difficulty.EASY;
    public static bool didPlayerChooseDifficulty = false;
    public static Difficulty difficultyThatPlayerChose = Difficulty.EASY;
    public int easyMinimaxDepth = 1;
    public int normalMinimaxDepth = 3;
    public int hardMinimaxDepth = 6;
    
    private int maxMinimaxDepth = 3;

    private List<Move> possibleMoves;
    private const float maxAiScore = 100;
    private List<AiMove> rootMoves;
    private const float decay = 0.98f;

    protected override void Awake()
    {
        base.Awake();

        if(didPlayerChooseDifficulty)
        {
            difficulty = difficultyThatPlayerChose;
            print("Player chose " + difficulty);
        }

        if(difficulty == Difficulty.EASY)
        {
            maxMinimaxDepth = easyMinimaxDepth;
        }
        else if(difficulty == Difficulty.NORMAL)
        {
            maxMinimaxDepth = normalMinimaxDepth;
        }
        else
        {
            maxMinimaxDepth = hardMinimaxDepth;
        }

        print(difficulty);
        print("Minimax depth: " + maxMinimaxDepth);

    }

    private List<AiMove> GetPossibleMoves(GameManager.PawnColor side, BoardData boardData)
    {
        int i, x, y;
        List<AiMove> possibleAiMovesWithScores = new List<AiMove>();
        List<Pawn> pawns = (side == GameManager.PawnColor.WHITE) ? PieceManager.Instance.whitePawns : PieceManager.Instance.blackPawns;

        for (i = 0; i < pawns.Count; i++)
        {
            Pawn pawn = pawns[i];

            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    Position destinationPos = new Position(x, y);
                    //print(destinationPos.ToString());
                    if (pawn.CanMoveHere(destinationPos, boardData))
                    {
                        Move possibleMove = new Move(pawn.position, destinationPos, 'p', boardData.GetChar(destinationPos));
                        AiMove possibleMoveWithScore = new AiMove();
                        possibleMoveWithScore.move = possibleMove;
                        //print(possibleMove.ToString());
                        possibleAiMovesWithScores.Add(possibleMoveWithScore);
                    }
                }
            }
        }

        return possibleAiMovesWithScores;
    }

    public void Run(BoardData boardData)
    {
        print("Executing AI");

        float timeWhenAiStarted = Time.realtimeSinceStartup;

        //List<AiMove> possibleMoves = GetPossibleMoves(currentBoard);
        rootMoves = new List<AiMove>();
        //AiMove rootMove = new AiMove()
        float alpha = float.MinValue;
        float beta = float.MaxValue;

        int i;

        List<AiMove> possibleMovesWithScores = GetPossibleMoves(GameManager.PawnColor.BLACK, GameManager.Instance.boardData);
        
        for(i = 0; i < possibleMovesWithScores.Count; i++)
        {
            BoardData boardDataCopy = new BoardData();
            boardDataCopy.Init();
            boardDataCopy.Copy(boardData);
            boardDataCopy.MakeMove(possibleMovesWithScores[i].move);

            possibleMovesWithScores[i].score = Minimax(0, possibleMovesWithScores[i].move, boardDataCopy, GameManager.PawnColor.BLACK, alpha, beta);
            //print(possibleMovesWithScores[i].move.ToString() + " " + possibleMovesWithScores[i].score);
        }

        AiMove bestAiMove = null;
        float bestMoveScore = float.MinValue;

        for(i = 0; i < possibleMovesWithScores.Count; i++)
        {
            if(possibleMovesWithScores[i].score > bestMoveScore)
            {
                bestAiMove = possibleMovesWithScores[i];
                bestMoveScore = possibleMovesWithScores[i].score;
            }
        }

        print(bestMoveScore);

        GameManager.Instance.MakeMove(bestAiMove.move);
    }

    private float Minimax(int depth, Move move, BoardData boardData, GameManager.PawnColor sideWhoseScoreIsCalculated, float alpha, float beta)
    {
        //print(move.destinationChar);

        if(move.destinationChar == 'P')
        {
            return maxAiScore * Mathf.Pow(decay, depth) + (Random.value);
        }

        if(move.destinationChar == 'p')
        {
            return -maxAiScore * Mathf.Pow(decay, depth) + (Random.value);
        }

        if(depth >=maxMinimaxDepth)
        {
            return Random.Range(0f, 1f);
        }

        bool isMaximizing = sideWhoseScoreIsCalculated == GameManager.PawnColor.BLACK;

        GameManager.PawnColor oppositeSide = GameManager.GetOppositePawnColor(sideWhoseScoreIsCalculated);
        List<float> scores = new List<float>();
        List<AiMove> possibleAiMovesWithScores = GetPossibleMoves(sideWhoseScoreIsCalculated, boardData);
        int i;

        float best = (isMaximizing) ? float.MinValue : float.MaxValue;

        for (i = 0; i < possibleAiMovesWithScores.Count; i++)
        {
            BoardData boardDataCopy = new BoardData();
            boardDataCopy.Init();
            boardDataCopy.Copy(boardData);
            boardDataCopy.MakeMove(move);

            float score = Minimax(depth + 1, possibleAiMovesWithScores[i].move, boardDataCopy, oppositeSide, alpha, beta);
            possibleAiMovesWithScores[i].score = (score);
            scores.Add(possibleAiMovesWithScores[i].score);

            if (depth == 0)
            {
                rootMoves.Add(possibleAiMovesWithScores[i]);
            }

            if (isMaximizing)
            {
                best = Mathf.Max(score, best);
                alpha = Mathf.Max(alpha, best);

                if (beta <= alpha)
                {
                    break;
                }
            }
            else
            {
                best = Mathf.Min(best, score);
                beta = Mathf.Min(beta, best);

                // Alpha Beta Pruning 
                if (beta <= alpha)
                {
                    break;
                }
            }
        }

        return (isMaximizing) ? GetMaxOfList(scores) : GetMinOfList(scores);
    }

    private float GetMaxOfList(List<float> list)
    {
        int i;
        float maxValue = float.MinValue;

        for (i = 0; i < list.Count; i++)
        {
            if (list[i] > maxValue)
            {
                maxValue = list[i];
            }
        }

        return maxValue;
    }

    private float GetMinOfList(List<float> list)
    {
        int i;
        float minValue = float.MaxValue;

        for (i = 0; i < list.Count; i++)
        {
            if (list[i] < minValue)
            {
                minValue = list[i];
            }
        }

        return minValue;
    }

}
