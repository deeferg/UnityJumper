/*
Author : John Ferguson
Game : Catharsis Montes
Script : InputName
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* <summary>
 * This class is responsible for taking the values typed into
 * the text InputField component in the UI.</summary><remarks> Responsible for
 * setting the player instance's name to the choice.</remarks>
*/
public class InputName : MonoBehaviour {

	/*<summary>
	 * At the start of the instantiation of the menu, the inputField object
	 * will be waiting for the user to finish the input</summary>, before submitting the event.
	 * The listener that is added makes sure that when the end edit is called, the listeners
	 * will call their respective functions (this one calls submitName).  
	*/
	void Start () {

		var input = gameObject.GetComponent<InputField> ();
		var submitEvent = new InputField.SubmitEvent ();
		submitEvent.AddListener (SubmitName);
		input.onEndEdit = submitEvent;
	}

	/* <summary>
	 * SubmitName takes a string parameter and simply sets the instance of the
	 * PlayerController's name to that choice.</summary>
	 * <param name = "name"> name to set player to</param>
	*/
	void SubmitName(string name){
		PlayerController.instance.setPlayerName (name);
	}
}