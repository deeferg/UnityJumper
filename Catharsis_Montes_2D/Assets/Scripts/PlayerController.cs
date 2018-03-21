/*
Author : John Ferguson
Game : Catharsis Montes
Script : PlayerController
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ReadWriteCsv;
using System.IO;
using System.Text;
using System;
/*<summary>
 * PlayerController is the class responsible for the movement and effects of the player.</summary><remarks>
 * It is responsible for updating the input from the user on the player object, and
 * making sure to store the value to the PlayerObject for later storage.
 * Aside from updating each frame, also implements the jumping, dying of the character,
 * and function calls that stem from that. Also has a few checks, like assuring the
 * player is grounded on isGrounded(). Stores values for coins collected to add to score.
</remarks>*/
public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	float jumpForce = 9.5f;//<value>Strength of the jump against the gravity value in game.</value>
	float runningSpeed = 6f;//<value>Speed player can move to the right in game.</value>
	public int collectedCoins = 0;
	public Animator animator;//<value>Used to change character Animation</value>

	public Text countCoins;
	Vector3 startingPosition;//<value>Plots position on game grid</value>
	Rigidbody2D rigidBody;//<value>rigidbody instance</value>

	PlayerObject player;

	//Method called by UnityEngine at beginning of program running.
	void Awake() {
		instance = this;
		rigidBody = GetComponent<Rigidbody2D>();
		startingPosition = this.transform.position;
		player = new PlayerObject();
	}
	/* <summary>
	 * Function being called when user hits play Game in UI screens.</summary>
	 * <remarks>Sets player up in an alive state, animated, and sets up the
	 * level pieces generated in front of them. Also plots the 
	 * characters position to the beginning position.
	</remarks>*/
	public void StartGame() {
		//Begin by generating the new level and setting player in the alive animation
		setCountText();
		animator.SetBool("isAlive", true);
		LevelGenerator.instance.GenerateInitialPieces ();
		this.transform.position = startingPosition;
	}
	//Called every frame to check for user input. Looks for clicking to jump.
	void Update () {
		//Check to see if GameManager is running in the inGame state.
		if (GameManager.instance.currentGameState == GameState.inGame) {
			if (Input.GetMouseButtonDown(0)) {//Check for player to jump
				Jump ();
			}
			//Check each frame to make sure character is grounded and keep them running
			animator.SetBool ("isGrounded", isGrounded ());
		}
	}
	/* <summary>
	 * FixedUpdate is checking for whether or not the user is alive,
	 * and sets the points of the playerObject accordingly.</summary> Also implements
	 * auto-run so the user so they only need to jump.
	*/
	void FixedUpdate() {

		if(animator.GetBool("isAlive") == true)
		player.setPoints ((this.transform.position.x - startingPosition.x));

		//To turn on auto-run so player has to worry about jumping only
		if (GameManager.instance.currentGameState == GameState.inGame) {
			if (rigidBody.velocity.x < runningSpeed) {
				rigidBody.velocity = new Vector2 (runningSpeed, rigidBody.velocity.y);
			}
		}	
	}
	/* <summary>
	 * Simple function for changing the velocity of the force in the upwards motion.</summary>
	 * <remarks>Checks the isGrounded boolean first so the player doesn't jump away, and multiply/add
	 * the force values.</remarks>
	*/
	void Jump() {
		//Check if player is grounded then add jump force to the up vector, giving the force to rise
		if(isGrounded()){
			rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	public LayerMask groundLayer;

	/*<summary>
	 * boolean function to check whether the raycast of the current position
	 * returns a true value</summary> regarding the character being within 1 unit of the ground.
	 * Else it returns false.
	 * <returns> boolean true or false </returns>
	*/
	public bool isGrounded() {

		if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value)) {
			return true;
		}
		else {
			return false;
		}
	}

	//Setter for Speed
	//<param name = "runningSpeed">The value to set running speed to</param> 
	public void setSpeed(float runningSpeed){
		this.runningSpeed = runningSpeed;
	}

	//Getter for Speed
	//<returns> value for runningSpeed </returns>
	public float getSpeed(){
		return runningSpeed;
	}

	/*<summary>
	 * OnCollisionEnter checks with collision objects to see whether or not they are
	 * tagged as an 'enemy' in Unity</summary><remarks> (such as the ground prefab). When the character
	 * hits the enemy, the kill() function is called to kill the player.</remarks>
	 * <param name = "other"> Collision sensor for playerController to get killed</param>
	*/
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Enemy") {
			Kill ();//Kill the character
		}
	}
		
	/*<summary>
	 * The Kill function is to run the death animation and change the GameManager to the
	 * GameOver canvas.</summary><remarks> It is also responsible for writing the high scores for the player.
	 * Also it is where the points are added together with the distance and coins.</remarks>
	*/
	public void Kill() {//Run the death animation and change GameManagaer to gameOver scene
		player.setPoints(player.getPoints() + collectedCoins);

		if (animator.GetBool ("isAlive") == true) {
			player.WriteHighScore ();
			player.FillHighScores ();
		}
		animator.SetBool("isAlive", false);
		GameManager.instance.GameOver();

	}

	/* <summary>
	 * Function to find the distance between the starting x value and current x value for the player.</summary>
	 * <returns> float for the distance travelled</returns>
	*/
	public float GetDistance() {
		float traveledDistance = Vector2.Distance(new Vector2(startingPosition.x, 0),
		                                          new Vector2(this.transform.position.x, 0));
		return traveledDistance;	                                                                               
	}
	/*<summary>
	 * Function to add one coin to the total for the score</summary>, and also sets the value of
	 * coins in the inGame screen.
	*/
	public void CollectedCoin() {
		//To add coins to player total
		collectedCoins ++;
		setCountText ();
	}

	/*<summary>
	 * Uses Text for UI to update the collected coins value.</summary>
	*/
	void setCountText(){

		countCoins.text = collectedCoins.ToString ();
	}

	/* <summary>
	 * Getters and setters for the player name in the PlayerObject.</summary>
	 * <param name = "playerName"> the value to set player name to</param>
	 * second one <returns> string for the player name</returns> 
	*/
	public void setPlayerName(string playerName){
		player.setPlayerName (playerName);
	}

	public string getPlayerName(){
		return player.getPlayerName ();
	}


}
	