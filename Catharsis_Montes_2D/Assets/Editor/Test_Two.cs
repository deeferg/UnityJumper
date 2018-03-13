/*
Author : John Ferguson
Game : Catharsis Montes
Test Script : Test_Two
*/

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_Two {

	[Test]
	public void CheckSetSpeedWorks() {
		GameObject obj = new GameObject ();
		var controller = obj.AddComponent<PlayerController>();
		controller.setSpeed (12);
		Assert.AreEqual (12 ,controller.getSpeed());

	}
		
}
