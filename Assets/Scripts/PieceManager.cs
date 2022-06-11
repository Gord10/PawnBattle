using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : Singleton<PieceManager>
{
    public Pawn whitePawnPrefab, blackPawnPrefab;
    public List<Pawn> allPawns;

    public void DeletePawnsInScene()
    {
        Pawn[] pawns = FindObjectsOfType<Pawn>();
        int i;
        for (i = 0; i < pawns.Length; i++)
        {
            Destroy(pawns[i].gameObject);
        }
    }

    public void CreatePawns(BoardData boardData)
    {
        allPawns = new List<Pawn>();

        int i, j;
        for(i = 0; i < 8; i++)
        {
            for(j = 0; j < 8; j++)
            {
                char c = boardData.GetChar(i, j);
                if(c == 'P')
                {
                    Vector3 worldPosition = Board.Instance.GetTileWorldPosition(i, j);
                    Pawn pawn = Instantiate(whitePawnPrefab, worldPosition, Quaternion.identity, transform);
                    pawn.SetPosition(i, j);
                    allPawns.Add(pawn);
                    //print("P "+ i + " " + j);
                }
                else if(c == 'p')
                {
                    Vector3 worldPosition = Board.Instance.GetTileWorldPosition(i, j);
                    Pawn pawn = Instantiate(blackPawnPrefab, worldPosition, Quaternion.identity, transform);
                    pawn.SetPosition(i, j);
                    allPawns.Add(pawn);
                    //print("p " + i + " " + j);
                }
            }
        }
    }

    public Pawn GetPawnAtPosition(Position position)
    {
        int i;
        for(i = 0; i < allPawns.Count; i++)
        {
            if(allPawns[i].position == position)
            {
                return allPawns[i];
            }
        }

        return null;
    }

    public void DestroyPawnAtPosition(Position position)
    {
        Destroy(GetPawnAtPosition(position).gameObject);
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
