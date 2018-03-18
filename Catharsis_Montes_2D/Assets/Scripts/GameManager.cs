/*
Author : John Ferguson
Game : Catharsis Montes
Script : GameManager
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public enum GameState {
	menu,
	inGame,
	gameOver,
	highScore
}

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameState currentGameState = GameState.menu;

	public Canvas menuCanvas;
	public Canvas inGameCanvas;
	public Canvas gameOverCanvas;
	public Canvas highScoreCanvas;

	PlayerObject player;
	List <PlayerObject> highScores;
	public int collectedCoins = 0;


	void Awake() {
		instance = this;
		//highScores = player.ReturnObjects ();
	}

	void Start() {
		currentGameState = GameState.menu;
	}
	
	//called to start the game
	public void StartGame() {
		//player = new PlayerObject ();
		//highScores = player.ReturnObjects ();
		//PlayerController.instance.StartGame ();
		SetGameState(GameState.inGame);
	}
	
	//called when player die
	public void GameOver() {
		//highScores = player.ReturnObjects ();
		SetGameState(GameState.gameOver);
	}

	public void Restart(){
		SceneManager.LoadScene (0);
	}

	//called when player decide to go back to the menu
	public void BackToMenu() {
		SetGameState(GameState.menu);
	}

	public void HighScoreMenu(){
		SetGameState (GameState.highScore);
	}

	void SetGameState (GameState newGameState) {
		
		if (newGameState == GameState.menu) {
			//setup Unity scene for menu state
			menuCanvas.enabled = true;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			highScoreCanvas.enabled = false;

		} else if (newGameState == GameState.inGame) {
			//setup Unity scene for inGame state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
			highScoreCanvas.enabled = false;

		} else if (newGameState == GameState.gameOver) {
			//setup Unity scene for gameOver state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
			highScoreCanvas.enabled = false;

		} else if (newGameState == GameState.highScore) {
			//setup Unity scene for highScore state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			highScoreCanvas.enabled = true;
		}
		
		currentGameState = newGameState;
	}

	void Update(){

	}


	public void CollectedCoin() {
		//To add coins to player total
		collectedCoins ++;
	}

	public void Quit(){
		Application.Quit ();
		Debug.Log ("Quit button is running");
	}


}



