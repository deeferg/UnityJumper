/*
Author : John Ferguson
Game : Catharsis Montes
Script : Collectable
*/

using UnityEngine;
using System.Collections;
/*<summary>
 * Collectable is a class which deals with the coins being displayed
 * in game.</summary><remarks> The basics are that it handles when to show the coins,
 * when to hide the coins, and what to do when they are collected.</remarks>
*/
public class Collectable : MonoBehaviour {

	//<value>isCollected Begins with false collected boolean to make it be shown.</value>
	bool isCollected = false;

	/* <summary>
	 * Show is to take the sprite of the coin and the collider of it,
	 * and essentially turn them on so they can be seen.</summary> It sets the collected boolean
	 * to false.
	*/
	void Show() {
		this.GetComponent<SpriteRenderer>().enabled = true;
		this.GetComponent<CircleCollider2D>().enabled = true;
		isCollected = false;
	}
	/* <summary>Hide is what is called when the coins get collected
	 * and need to be removed.</summary> Similar to Show, it just turns the
	 * enabled boolean to false instead of true.
	*/
	void Hide() {
		this.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponent<CircleCollider2D>().enabled = false;
	}

	/* <summary>
	 * Collect is responsible for what happens when the player collides
	 * with the coin's circleCollider.</summary> <remarks> It hides the coin, turns it's
	 * boolean for colletion to false, and gives a coin to the PlayerController
	 * and the GameManager instance.</remarks>
	*/
	void Collect() {

		isCollected = true;
		Hide();
		PlayerController.instance.CollectedCoin ();
	}

	/*<summary>
	 * On trigger enter is called when the collider for the coin sprite
	 * has it's space entered by the player.</summary> The collider surrounds the sprite.
	 * Once it's bubble is burst by the player, the collect() function is called, 
	 * hiding the coin. <param name ="other"> collider object player hits</param>
	*/
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") {
			Collect();
		}
	}	
}
