/*
Author : John Ferguson
Game : Catharsis Montes
Script : CameraFollow
*/

using UnityEngine;
using System.Collections;

/*<summary>
This is the class responsible with controlling the camera and following the player as they move throughout the playground.
</summary>*/
public class CameraFollow : MonoBehaviour {

	//<value>Value for offset allowances of 0.1 and 1 in float values.</value>
	public Vector2 offset = new Vector2 (0.1f, 1f);
	//<value>Value for dampener for the cameras movement</value>
	public float dampTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	//Part of Unity Engine which is called when the application begins
	void Awake() {
		Application.targetFrameRate = 60;
	}

	/*<summary>
	 * Method to reset the camera to the beginning position</summary>
	*/
	public void ResetToStartPosition() {
		Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
		Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
		Vector3 destination = transform.position + delta;
		
		destination = new Vector3(destination.x, offset.y, destination.z);
		transform.position = destination;
	}

	//Part of unity engine called when application resets each frame.
	void Update () {
	
		Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
		Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
		Vector3 destination = transform.position + delta;

		destination = new Vector3(destination.x, offset.y, destination.z);

		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		

	}
}
