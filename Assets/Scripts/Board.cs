using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 8;
    public int height = 8;

    public Tile tilePrefab;
    public Pawn whitePawnPrefab, blackPawnPrefab;

    private void Awake()
    {
        DeleteAllBoardElements();
        CreateTiles();
        CreatePawns();
    }

    private void DeleteAllBoardElements()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        int i;
        for(i = 0; i < tiles.Length; i++)
        {
            Destroy(tiles[i].gameObject);
        }

        Pawn[] pawns = FindObjectsOfType<Pawn>();
        
        for(i = 0; i < pawns.Length; i++)
        {
            Destroy(pawns[i].gameObject);
        }
    }

    private void CreateTiles()
    {
        int i, j;
        for(i = 0; i < width; i++)
        {
            for(j = 0; j < height; j++)
            {
                Tile tile = Instantiate(tilePrefab, new Vector3(i - 3.5f, j - 3.5f, 1), Quaternion.identity, transform);
                tile.SetColor((i + j) % 2 == 0);
            }
        }
    }

    private void CreatePawns()
    {
        int i;
        for(i = 0; i < width; i++)
        {
            Pawn pawn = Instantiate(whitePawnPrefab, new Vector3(i - 3.5f, -3.5f, 0), Quaternion.identity, transform);
            pawn = Instantiate(blackPawnPrefab, new Vector3(i - 3.5f, 3.5f, 0), Quaternion.identity, transform);
        }
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
