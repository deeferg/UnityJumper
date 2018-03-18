/*
Author : John Ferguson
Game : Catharsis Montes
Script : PlayerController
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ReadWriteCsv;
using System.IO;
using System.Text;
using System;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	float jumpForce = 9.5f;
	float runningSpeed = 6f;
	public int collectedCoins = 0;
	public Animator animator;//Used to change character Animation

	Vector3 startingPosition;//Plots position on game grid
	Rigidbody2D rigidBody;//rigidbody instance
	PlayerObject player;
	//int standings = 75;
	//List<PlayerObject> oldScores = new List<PlayerObject>();

	void Awake() {
		instance = this;
		rigidBody = GetComponent<Rigidbody2D>();
		startingPosition = this.transform.position;

		player = new PlayerObject();
		//oldScores.AddRange(player.ReturnObjects ());
	}
		
	public void StartGame() {
		//Begin by generating the new level and setting player in the alive animation
		animator.SetBool("isAlive", true);
		LevelGenerator.instance.GenerateInitialPieces ();
		this.transform.position = startingPosition;
	}
	
	void Update () {
	/*	if(player.getPoints() > oldScores[standings].getPoints()){
			Console.Write ("You Have passed people in the top 75!");
			Console.Write (string.Format("You have passed {0} in the standings!", oldScores[standings].getPlayerName().ToString()));
			standings = --standings;
		}
*/
		//Check to see if GameManager is running in the inGame state.
		if (GameManager.instance.currentGameState == GameState.inGame) {
			if (Input.GetMouseButtonDown(0)) {//Check for player to jump
				Jump ();
			}
			//Check each frame to make sure character is grounded and keep them running
			animator.SetBool ("isGrounded", isGrounded ());
		}
	}

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

	void Jump() {
		//Check if player is grounded then add jump force to the up vector, giving the force to rise
		if(isGrounded()){
			rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	public LayerMask groundLayer;

	public bool isGrounded() {

		if (Physics2D.Raycast(this.transform.position, Vector2.down, 1f, groundLayer.value)) {
			return true;
		}
		else {
			return false;
		}
	}
	//Setter for Speed
	public void setSpeed(float runningSpeed){
		this.runningSpeed = runningSpeed;
	}
	//Getter for Speed
	public float getSpeed(){
		return runningSpeed;
	}
	//Check for when character runs into collision zone
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Enemy") {
			Kill ();//Kill the character
		}
	}
		

	public void Kill() {//Run the death animation and change GameManagaer to gameOver scene
		player.setPoints(player.getPoints() + collectedCoins);
		Debug.Log (player.getPoints ());
		player.WriteHighScore ();
		player.FillHighScores ();
		animator.SetBool("isAlive", false);
		GameManager.instance.GameOver();

	}


	public float GetDistance() {
		float traveledDistance = Vector2.Distance(new Vector2(startingPosition.x, 0),
		                                          new Vector2(this.transform.position.x, 0));
		return traveledDistance;	                                                                               
	}

	public void CollectedCoin() {
		//To add coins to player total
		collectedCoins ++;
	}



}
	