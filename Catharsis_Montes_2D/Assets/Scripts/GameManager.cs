/*
Author : John Ferguson
Game : Catharsis Montes
Script : GameManager
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

	public int collectedCoins = 0;

	void Awake() {
		instance = this;
	}

	void Start() {
		currentGameState = GameState.inGame;
	}
	
	//called to start the game
	public void StartGame() {
		SetGameState(GameState.inGame);
		PlayerController.instance.StartGame ();
	}
	
	//called when player die
	public void GameOver() {
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


	void Update() {
		//Check each second to begin with to check if user begins new game with S key
		if (Input.GetButtonDown("s")) {
			StartGame();
		}
	}


	public void CollectedCoin() {
		//To add coins to player total
		collectedCoins ++;
	}

}



