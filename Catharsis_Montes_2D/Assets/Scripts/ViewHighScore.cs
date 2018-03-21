/*
Author : John Ferguson
Game : Catharsis Montes
Script : ViewHighScore
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*<summary>
This class is responsible for listing the top 10 players in the high score .csv file.
Uses an array of Text objects in a UI screen.
</summary>*/
public class ViewHighScore : MonoBehaviour {
	//<value>Array of text objects to be filled with scores.</value>
	public Text[] topScores = new Text[10];

	// Update is called once per frame
	void Update () {
		/*Check to make sure the Game State is in the high score instance, 
		 *and loop 10 times, filling each Text object with scores.
		 */
		if (GameManager.instance.currentGameState == GameState.highScore) {
			PlayerObject player = new PlayerObject ();
			List <PlayerObject> topPlayers = player.ReturnObjects ();
			for (int i = 0; i < 10; i++) {
				topScores [i].text = "Player: " + topPlayers [i].getPlayerName ().ToString () + "      Score : " + topPlayers [i].getPoints ().ToString ();
			}

		}
	}
}