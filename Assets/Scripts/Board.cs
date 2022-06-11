using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
    public Tile tilePrefab;
    public Tile[,] tiles;

    protected override void Awake()
    {
    }

    public Vector3 GetTileWorldPosition(int x, int y)
    {
        return tiles[x, y].transform.position;
    }

    public void DeleteAllTiles()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        int i;
        for(i = 0; i < tiles.Length; i++)
        {
            Destroy(tiles[i].gameObject);
        }
    }

    public void CreateTiles()
    {
        tiles = new Tile[8, 8];
        int i, j;
        for(i = 0; i < 8; i++)
        {
            for(j = 0; j < 8; j++)
            {
                Tile tile = Instantiate(tilePrefab, new Vector3(i - 3.5f, j - 3.5f, 1), Quaternion.identity, transform);
                tile.SetColor((i + j) % 2 == 0);
                tiles[i, j] = tile;
                tile.name = (i + 1).ToString() + "x" + (j + 1).ToString();
                tile.SetPosition(i, j);
            }
        }
    }

    public void ClearBoardHighlight()
    {
        int i, j;
        for(i = 0; i < 8; i++)
        {
            for(j = 0; j < 8; j++)
            {
                tiles[i, j].inputIndicator.Hide();
            }
        }
    }

    public void MarkPossibleDestinations(Pawn selectedPawn, BoardData boardData)
    {
        int i, j;
        for (i = 0; i < 8; i++)
        {
            for (j = 0; j < 8; j++)
            {
                if(selectedPawn.CanMoveHere(new Position(i, j), boardData))
                {
                    tiles[i, j].inputIndicator.MakeGreen();
                }
            }
        }
    }
}
