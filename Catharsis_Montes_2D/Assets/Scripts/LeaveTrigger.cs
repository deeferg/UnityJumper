/*
Author : John Ferguson
Game : Catharsis Montes
Script : LeaveTrigger
*/
using UnityEngine;
using System.Collections;
/*<summary>
 * Class responsible for triggering the new generation of pieces.</summary>
 * Also cleans up the old piece left behind.
*/
public class LeaveTrigger : MonoBehaviour {

	/*<summary>
	 * Generates a new random piece ahead of the player</summary>
	 * Deletes the old piece behind the player.
	*/
	void OnTriggerEnter2D(Collider2D other) {

		LevelGenerator.instance.AddPiece();
		LevelGenerator.instance.RemoveOldestPiece();
	}
	
}
