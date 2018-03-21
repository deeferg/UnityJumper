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

/* <summary>
 * An enumeration of the GameStates I built to be available.</summary>
 * <remarks>Menu will be where the user will put in their score,
 * or have the option to navigate to the high score menu or exit.
 * inGame will be where the user plays the game itself.
 * GameOver will feature the users score and name projected overtop
 * of the inGame screen.
 * HighScore will list the top 10 players in the highScore.csv file.</remarks>
*/
public enum GameState {
	menu,
	inGame,
	gameOver,
	highScore
}
/* <summary>
 * Game manager is responsible for deciding which state the game is in.</summary>
 * <remarks>It will alert the player when the game state is inGame, and they are able to move.
 * It will alert when the player dies, and changes to gameOver state.
 * It will alert when user presses play game, to change to inGame state.
 * It will alert when user presses high scores, changing to the high score state
 * </remarks>
*/
public class GameManager : MonoBehaviour {
	//instance of the class
	public static GameManager instance;
	//Set the current game state to the menu to begin with.
	public GameState currentGameState = GameState.menu;
	//The canvases for each UI instance.
	public Canvas menuCanvas;
	public Canvas inGameCanvas;
	public Canvas gameOverCanvas;
	public Canvas highScoreCanvas;
	ThreadReader reader;

	public int collectedCoins = 0;

	/*
	 * Called when game is started. Instantiates object so start() is called
	*/
	void Awake() {
		instance = this;
	}
	/*
	 * Called when instantiated. Turns the currentGameState to the menu.
	*/
	void Start() {
		currentGameState = GameState.menu;
		reader = new ThreadReader ();
		reader.Start ();
	}
	
	/* <summary>
	 * Called by button in menu UI.</summary><remarks> turns gamestate to inGame, which
	 * is the trigger for playerController.</remarks>
	*/
	public void StartGame() {
		SetGameState(GameState.inGame);
	}
	
	/*<summary>
	 * GameOver called when player dies, and sets to gameOver
	</summary>*/
	public void GameOver() {
		SetGameState(GameState.gameOver);
	}
	/*<summary>
	 * Called when the player hits play again, and loads
	 * up a new game.
		</summary>*/
	public void Restart(){
		SceneManager.LoadScene (0);
	}

	/*<summary>
	* Called when a player hits a button to go back to the menu
	</summary>*/
	public void BackToMenu() {
		SetGameState(GameState.menu);
	}
	/* <summary>
	 * Called when a player chooses to look at the high score menu.
	 </summary>*/
	public void HighScoreMenu(){
		SetGameState (GameState.highScore);
	}
	/*<summary>
	 * Method which does if checks to see which gameState the canvas should be in.</summary>
	 * <remarks>Checks each of the 4 game states and then changes the currentGameState to that.
	</remarks>*/
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
		if (reader != null) {
			if (reader.Update ()) {
				reader = null;
			}
		}
	}

	/* <summary>
	 * Called when user quits the application with the exit button.
	</summary>*/
	public void Quit(){
		Application.Quit ();
	}


}



