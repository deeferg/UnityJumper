/*
Author : John Ferguson
Game : Catharsis Montes
Test Script : Test_One
*/

using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_One{

	[Test]
	public void CheckPlayerDoesntStartGrounded() {
		GameObject obj = new GameObject ();
		var controller = obj.AddComponent<PlayerController>();

		Assert.False (controller.isGrounded());
	}
		
}
