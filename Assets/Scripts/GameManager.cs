using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public BoardData boardData;

    private enum State
    {
        WAITING_FOR_SOURCE,
        WAITING_FOR_DESTINATION
    }

    private State state = State.WAITING_FOR_SOURCE;
    private Position sourcePos, destinationPos;
    private Pawn selectedPawn;

    protected override void Awake()
    {
        boardData = new BoardData();
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
        else if(tile.position != sourcePos)
        {
            tile.inputIndicator.Hide();
        }
    }

    public void OnMouseDownTile(Tile tile)
    {
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
    }

    private void SetState(State newState)
    {
        state = newState;
    }
}
