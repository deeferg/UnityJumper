/*
Author : John Ferguson
Game : Catharsis Montes
Script : KillTrigger
*/


using UnityEngine;
using System.Collections;

/*<summary>
 * Class responsible for killing the player when he falls below the levels platforms.</summary><remarks>
 * Once attached to a gameObject, and a player collides with it, sends that player a kill().</remarks>
*/
public class KillTrigger : MonoBehaviour {


	/*<summary>
	 * Sends the kill trigger to the player instance.
	 *</summary><param name = "other"> A Collider object</param>
	*/
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
			PlayerController.instance.Kill();
		}
	}
		
}
