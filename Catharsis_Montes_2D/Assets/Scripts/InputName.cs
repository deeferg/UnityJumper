using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var input = gameObject.GetComponent<InputField> ();
		var submitEvent = new InputField.SubmitEvent ();
		submitEvent.AddListener (SubmitName);
		input.onEndEdit = submitEvent;
	}
	
	// Update is called once per frame
	void SubmitName(string name){
		PlayerController.instance.setPlayerName (name);
	}
}
