using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    // Use this for initialization
    public static LevelGenerator instance;
    public List<LevelPiece> levelPrefabs = new List<LevelPiece>();
    public Transform levelStartPoint;
    public List<LevelPiece> pieces = new List<LevelPiece>();
    private const float holeProbability = (float)0.87;


    void Awake()
    {
        instance = this;
    }

	void Start () {
        GenerateInitialPieces();
	}

    public void GenerateInitialPieces()
    {
        for(int i=0; i<2; i++)
        {
            AddPiece();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPiece()
    {
        int randomIndex = Random.Range(0, levelPrefabs.Count);
        Debug.Log("randomIndex:" + randomIndex);
        Debug.Log("levelPrefabs:" + levelPrefabs.Count);
        float myRandom = Random.value;
//        Debug.Log(myRandom);
//        if(myRandom > holeProbability)
//        {
//            return;
//        }
        // Instantiate -> making a copy od an object
        LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[randomIndex]);
        piece.transform.SetParent(this.transform, false);

        Vector3 spawnPosition = Vector3.zero;

        if(pieces.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        } else
        {
            spawnPosition = pieces[pieces.Count - 1].exitPoint.position;
        }

        piece.transform.position = spawnPosition;
        pieces.Add(piece);
    }

    public void RemoveOldestPiece()
    {
        LevelPiece oldestPiece = pieces[0];

        pieces.Remove(oldestPiece);
        Destroy(oldestPiece.gameObject);
    }

}
