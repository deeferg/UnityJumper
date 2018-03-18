using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ViewHighScore : MonoBehaviour {

	public Text[] topScores = new Text[10];
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.currentGameState == GameState.highScore) {
			PlayerObject player = new PlayerObject ();
			List <PlayerObject> topPlayers = player.ReturnObjects ();
			for (int i = 0; i < 10; i++) {
				topScores [i].text = "Player: " + topPlayers [i].getPlayerName ().ToString () + "      Score : " + topPlayers [i].getPoints ().ToString ();
			}
		
		}
	}
}
