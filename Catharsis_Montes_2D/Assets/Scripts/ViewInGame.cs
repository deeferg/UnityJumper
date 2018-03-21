/*
Author : John Ferguson
Game : Catharsis Montes
Script : ViewInGame
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
Class responsible for the UI in the game when they are running along.
Responsible for users score, and the high score stat on the top right
*/
public class ViewInGame : MonoBehaviour {
	//<value>Text label declaration for the score, coins, and high score in the UI.</value>
	public Text scoreLabel;
	public Text coinLabel;
	public Text highscoreLabel;
	//<value>Instatiation of playerObject to find top player in the high scores to list</value>
	PlayerObject topPlayer = new PlayerObject();


	void Update() {//Called each frame
		//While the game instance is inGame, make sure to keep the labels in their places.
		if (GameManager.instance.currentGameState == GameState.inGame) {
			scoreLabel.text = PlayerController.instance.GetDistance().ToString("f0");
			coinLabel.text = PlayerController.instance.collectedCoins.ToString();
			highscoreLabel.text = topPlayer.getTopPlayer().getPlayerName() + " = "
				+  topPlayer.getTopPlayer ().getPoints ().ToString();
		}
	}
}



