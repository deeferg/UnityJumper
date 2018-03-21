/*
Author : John Ferguson
Game : Catharsis Montes
Script : LevelGenerator
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*<summary>
 * This is the class responsible for the random generation
 * of pieces for the player.</summary> New pieces go in front, old destroyed behind.
*/
public class LevelGenerator : MonoBehaviour {

	public static LevelGenerator instance;
	//<value> all level pieces blueprints used to copy from</value>
	public List<LevelPiece> levelPrefabs = new List<LevelPiece>();
	//<value>starting point of the very first level piece</value>
	public Transform levelStartPoint;
	//<value>all level pieces that are currently in the level</value>
	public List<LevelPiece> pieces = new List<LevelPiece>();
	
	void Awake() {
		instance = this;
	}
	/* <summary>
	 * Generates a new set when the instance is loaded.
	</summary>*/
	void Start() {
		GenerateInitialPieces();
	}

	/* <summary>
	 * Geneartes a set of pieces which are a call to AddPiece()
	</summary>*/
	public void GenerateInitialPieces() {
		for (int i=0; i<2; i++) {
			AddPiece();
		}
	}
	/*<summary>
	 * AddPiece takes a random prefab from the available list</summary>,
	 * and <remarks>instantiates it at the spot listed as the spawnPosition.
	 * The spawn position is decided depending on if it's the first piece or not.
	 * If it isn't, then it's wherever the exitPoints position is of the next Piece.
	</remarks>*/
	public void AddPiece() {

		//pick the random number
		int randomIndex = Random.Range(0, levelPrefabs.Count);

		//Instantiate copy of random level prefab and store it in piece variable
		LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[randomIndex]);
		piece.transform.SetParent(this.transform, false);

		Vector3 spawnPosition = Vector3.zero;

		//position
		if (pieces.Count == 0) {
			//first piece
			spawnPosition = levelStartPoint.position;
		}
		else {
			//take exit point from last piece as a spawn point to new piece
			spawnPosition = pieces[pieces.Count-1].exitPoint.position;
		}
		
		piece.transform.position = spawnPosition;
		pieces.Add(piece);
	}
	/* <summary>
	 * Function to remove the last piece of the array of objects.
	 * Removes the first object in the array of pieces.
	</summary>*/
	public void RemoveOldestPiece() {
		
		LevelPiece oldestPiece = pieces[0];
		
		pieces.Remove(oldestPiece);
		Destroy(oldestPiece.gameObject);
	}

}
