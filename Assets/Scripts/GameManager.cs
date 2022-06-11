using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public BoardData boardData;
    public enum PawnColor
    {
        WHITE,
        BLACK
    }

    public enum State
    {
        WAITING_FOR_SOURCE,
        WAITING_FOR_DESTINATION,
        WHITE_VICTORY,
        BLACK_VICTORY,
        CALCULATING_AI
    }

    private State state = State.WAITING_FOR_SOURCE;
    private Position sourcePos, destinationPos;
    private Pawn selectedPawn;
    private PawnColor whoseTurn = PawnColor.WHITE;

    protected override void Awake()
    {
        boardData = new BoardData();
        boardData.Init();
        boardData.PlacePawnsOnDefaultPosition();

        Board.Instance.DeleteAllTiles();
        Board.Instance.CreateTiles();

        PieceManager.Instance.DeletePawnsInScene();
        PieceManager.Instance.CreatePawns(boardData);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOverTile(Tile tile)
    {
        char c = boardData.GetChar(tile.position);

        if(state == State.WAITING_FOR_SOURCE)
        {
            switch (c)
            {
                case 'P':
                    tile.inputIndicator.MakeYellow();
                    break;

                case 'p':
                    tile.inputIndicator.MakeRed();
                    break;

                case '.':
                    tile.inputIndicator.MakeBlue();
                    break;
            }
        }
        else if(state == State.WAITING_FOR_DESTINATION)
        {
            switch (c)
            {
                case 'P':
                    tile.inputIndicator.MakeYellow();
                    break;

                case 'p':
                    if (selectedPawn.CanMoveHere(tile.position, boardData))
                    {
                        tile.inputIndicator.MakeGreen();
                    }
                    else
                    {
                        tile.inputIndicator.MakeRed();
                    }
                    break;

                case '.':
                    if(selectedPawn.CanMoveHere(tile.position, boardData))
                    {
                        tile.inputIndicator.MakeGreen();
                    }
                    else
                    {
                        tile.inputIndicator.MakeBlue();
                    }
                    break;
            }
        }
    }

    public void OnMouseExitTile(Tile tile)
    {
        if(state == State.WAITING_FOR_SOURCE)
        {
            tile.inputIndicator.Hide();
        }
        else if(tile.position != sourcePos && !selectedPawn.CanMoveHere(tile.position, boardData))
        {
            tile.inputIndicator.Hide();
        }
    }

    public bool DidGameEnd()
    {
        return state == State.BLACK_VICTORY || state == State.WHITE_VICTORY;
    }

    public void OnMouseDownTile(Tile tile)
    {
        if(DidGameEnd())
        {
            return;
        }

        char c = boardData.GetChar(tile.position);

        if(c == 'P')
        {
            selectedPawn = PieceManager.Instance.GetPawnAtPosition(tile.position);
            Board.Instance.ClearBoardHighlight();
            Board.Instance.MarkPossibleDestinations(selectedPawn, boardData);
            tile.inputIndicator.MakeYellow();
            sourcePos = tile.position;
            
            SetState(State.WAITING_FOR_DESTINATION);
        }

        if(state == State.WAITING_FOR_DESTINATION)
        {
            if(selectedPawn.CanMoveHere(tile.position, boardData))
            {
                MakeMove(selectedPawn.position, tile.position, selectedPawn);

            }
        }
    }

    public void RunAi()
    {
        AI.Instance.Run(boardData);
    }

    public void MakeMove(Move move)
    {
        Pawn pawn = PieceManager.Instance.GetPawnAtPosition(move.sourcePos);
        print(move.ToString());
        MakeMove(move.sourcePos, move.destinationPos, pawn);
    }

    public void MakeMove(Position sourcePos, Position destinationPos, Pawn pawn)
    {
        Move move = new Move(sourcePos, destinationPos, (pawn.color == PawnColor.WHITE)? 'P' : 'p');
        char destinationChar = boardData.GetChar(destinationPos);

        if (whoseTurn == PawnColor.BLACK)
        {
            whoseTurn = PawnColor.WHITE;

        }
        else
        {
            whoseTurn = PawnColor.BLACK;
        }

        if (destinationChar == 'P')
        {
            SetState(State.BLACK_VICTORY);
            //willGameContinue = false;
            PieceManager.Instance.DestroyPawnAtPosition(destinationPos);
        }
        else if (destinationChar == 'p')
        {
            //willGameContinue = false;
            SetState(State.WHITE_VICTORY);
            PieceManager.Instance.DestroyPawnAtPosition(destinationPos);
        }

        boardData.MakeMove(move);

        Vector3 newWorldPosition = Board.Instance.GetTileWorldPosition(destinationPos);
        pawn.transform.position = newWorldPosition;
        pawn.SetPosition(destinationPos);
        Board.Instance.ClearBoardHighlight();

        if(state != State.BLACK_VICTORY && state != State.WHITE_VICTORY)
        {
            if (whoseTurn == PawnColor.WHITE)
            {
                SetState(State.WAITING_FOR_SOURCE);
            }
            else
            {
                SetState(State.CALCULATING_AI);
                RunAi();
            }
        }
        
        print("Turn: " + whoseTurn);
    }

    private void SetState(State newState)
    {
        state = newState;
        if(state == State.BLACK_VICTORY || state == State.WHITE_VICTORY)
        {
            GameUi.Instance.OpenGameEndScreen(state);
        }
    }

    public static void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


    public static void OpenMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
