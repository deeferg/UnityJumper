/*
Author : John Ferguson
Game : Catharsis Montes
Script : ThreadReader
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*<summary>
 * This is the sub-class to PlayerMultithreading which performs the operations.</summary>
 * It is instantiated in GameManager at Start(), and simply changes one records
 * name and value. Prints the old name to the console, and shows the new one at the end. 
*/
public class ThreadReader : PlayerMultithreading {
	//<value>PlayerObject for modifying values</value>
	PlayerObject player;
	//<value>List of players to change one random value</value>
	List<PlayerObject> playerList;

	/*<summary>
	 * Overridden function to begin thread, and begin changing players stats.</summary>
	 * Makes a new player, returns the list of objects, and changes one object.
	*/
	protected override void ThreadFunction(){
	
		player = new PlayerObject ();
		playerList = player.ReturnObjects ();
		Debug.Log (playerList.Count);
		player = playerList[50];
		Debug.Log (player.getPlayerName ());
		player.setPoints (54);
		player.setPlayerName("John");
	
	}
	/*<summary>
	 * OnFinished is called right after ThreadFunction and writes the high scores
	 * with the updated joke value so it can be found before the game begins.</summary>
	*/
	protected override void OnFinished(){
		Debug.Log (player.getPlayerName ());
		player.WriteHighScore();
		player.FillHighScores();
	}
}
