using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.IO;
using ReadWriteCsv;



public class InputMultithreading {
	/*
	IEnumerator ThreadStart(){
		bool isRunning = false;
	

		yield return null;
	}*/




	public void ReadThread(){
		CsvFileReader connection = new CsvFileReader ("000000.csv");
		CsvRow row = new CsvRow ();
		while (connection.ReadRow (row)) {
			foreach(string s in row){
				Debug.Log (s);
				Console.Write (s);
				Console.Write (s.Length);
				Console.Write ("");
			}
			Console.WriteLine ();
		
		}

	}
		

}
