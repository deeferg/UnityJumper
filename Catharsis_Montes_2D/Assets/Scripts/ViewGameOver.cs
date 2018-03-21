/*
Author : John Ferguson
Game : Catharsis Montes
Script : ViewGameOver
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*<summary>
This class is responsible for the UI in the game over view for the user. Will show their total score.
</summary>*/
public class ViewGameOver : MonoBehaviour {
	//<value>Text object for player in the UI</value>
	public Text playerName;

		/*<summary>
		* Assures the game manager instance is in the GameOver gamestate</summary>,
		* and shows the users last score in the UI.
		*/
	void Update () {
		if (GameManager.instance.currentGameState == GameState.gameOver) {
			PlayerObject player = new PlayerObject ();

			playerName.text = player.returnLastPlayer().getPlayerName() + "   " +
				player.returnLastPlayer().getPoints ().ToString ();

		}
	}
}
