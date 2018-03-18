/*
Author : John Ferguson
Game : Catharsis Montes
Script : PlayerObject
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadWriteCsv;
using System;
using System.IO;


public class PlayerObject {

	string playerName;
	float points = 0;
	CsvRow readRow;

	public PlayerObject(){
		setPlayerName ("Edgar");
	}

	public PlayerObject(string s, float i){
		setPlayerName (s);
		setPoints (i);
	}

	public PlayerObject(string s){
		setPlayerName (s);
	}

	public PlayerObject(float i){
		setPoints (i);
		setPlayerName ("Unregistered");
	}

	public void WriteHighScore(){

		CsvFileWriter writer = new CsvFileWriter ("000001.csv");
		CsvRow row = new CsvRow();
		/*

			Method for writing the high score from the game to a single value
			in a csv file that is empty so that no other high scores are overwritten.
			Enables keeping a temp file, a building file, and one to send to server
			(if gets to point of multi-threading and sending requests with multiple clients)

		Add the players name who belongs to this instantiation of PlayerObject
		To the first row, and the points score in the second row, Then write the Row
		to the CsvFileWriter IO and dispose and close the writer to prevent leaks.
		*/
		row.Add (playerName);
		row.Add (string.Format("{0}" ,points));
		Console.Write (string.Format("You scored :{0}", points));
		writer.WriteRow (row);

		writer.Dispose ();
		writer.Close ();


	}
		


	public void FillHighScores(){
		//Row to read values from Other files(dataset, new high score in 000001.csv)
		CsvRow readerRow;
		//Row to insert values into CsvFileWriter below for HighScoreFile good copy
		CsvRow row = new CsvRow();
		//Reader Object for New High Score (or old if run early)
		CsvFileReader reader = new CsvFileReader ("HighScoreFile.csv");
		//Array of Player Objects to fill with readerRow's values read
		List<PlayerObject> pastPlayers = new List<PlayerObject> ();

		//Loop through the reader till the end
		while (!reader.EndOfStream) {
			//if (reader.BaseStream.ReadByte() == -1)
			//	break;
			//Build a new player Object to fill
			PlayerObject obj = new PlayerObject();
			//Instatiate the CsvRow
			readerRow = new CsvRow ();
			//Build a string array to hold the CsvRow values
			string[] list = new string[2];
			//Read the rows and Copy the data to the array
			reader.ReadRow (readerRow);
			readerRow.CopyTo (list);
			//Check for "Player" as the first column and skip it
			if (list [0].ToString ().Contains ("Player"))
				continue;

			//Set player names by formatting the string, and parsing to a float
			obj.setPlayerName (string.Format ("{0}", list[0].ToString ()));
			obj.setPoints (float.Parse(list [1]));
			//Add the player Object to the array
			pastPlayers.Add (obj);
			readerRow.Clear ();
		}//End of while loop

		reader.Dispose ();
		reader.Close ();

		//Reader to loop through Dataset for run just taken. Keeps the values safe.
		reader = new CsvFileReader ("000001.csv");

		while(!reader.EndOfStream){

			PlayerObject obj = new PlayerObject();
			readerRow = new CsvRow();
			string[] list = new string[2];
			//Read the rows and Copy the data to the array
			reader.ReadRow (readerRow);
			readerRow.CopyTo (list);
			//Set player names by formatting the string, and parsing to a float
			obj.setPlayerName (string.Format ("{0}", list [0].ToString ()));
			obj.setPoints (float.Parse(list [1]));
			//Add the player Object to the array
			pastPlayers.Add (obj);
			readerRow.Clear ();
		}

		reader.Dispose ();
		reader.Close ();
		//Reader to loop through Dataset given for assignment, which pulls the values
		//of the name of the commodity in the list, and the value of the commodity.
		//It has taken a shortened version, at the behest of Professor Stanley Pieda
		//when asked if I needed to keep the 100K+ file or revise it slightly.
	/*	reader = new CsvFileReader ("000000.csv");

		while(!reader.EndOfStream){
			
			PlayerObject obj = new PlayerObject();
			readerRow = new CsvRow();
			string[] list = new string[6];

			reader.ReadRow (readerRow);
			readerRow.CopyTo(list);
			obj.setPlayerName(string.Format("{0}", list [2].ToString ()));
			obj.setPoints (float.Parse(list[5]));

			pastPlayers.Add(obj);
			readerRow.Clear ();
		}

		reader.Dispose ();
		reader.Close ();
*/
		//CsvFileWriter Object to write the high score objects to the csv file
		CsvFileWriter writer = new CsvFileWriter ("HighScoreFile.csv");
		//Adding the headers for Player and Score in the table
		row.Add(string.Format("Player"));
		row.Add(string.Format("Score"));
		//Writing it to the file, and clearing the row which was written
		writer.WriteRow (row);
		row.Clear ();

		pastPlayers.Sort ((x, y) => y.points.CompareTo (x.points));
		//Looping through the number of playerObjects in the List. Printing each one
		//To it's own row and clearing that row after it's written.
		for(int i = 0; i < pastPlayers.Count; i++){
			row.Add (string.Format ("{0}",pastPlayers[i].getPlayerName()));
			row.Add (string.Format("{0}", pastPlayers[i].getPoints()));
			writer.WriteRow (row);
			row.Clear ();
		}
		//Clean up the writers
		writer.Dispose ();
		writer.Close ();

		//pastPlayers.Sort (0,pastPlayers.Count,pastPlayers [0].points);

	}

	public List<PlayerObject> ReturnObjects(){
	
		//Row to read values from Other files(dataset, new high score in 000001.csv)
		CsvRow readerRow;
		//Reader Object for New High Score (or old if run early)
		CsvFileReader reader = new CsvFileReader ("HighScoreFile.csv");
		//Array of Player Objects to fill with readerRow's values read
		List<PlayerObject> pastPlayers = new List<PlayerObject> ();

		//Loop through the reader till the end
		while (!reader.EndOfStream) {
			//if (reader.BaseStream.ReadByte() == -1)
			//	break;
			//Build a new player Object to fill
			PlayerObject obj = new PlayerObject();
			//Instatiate the CsvRow
			readerRow = new CsvRow ();
			//Build a string array to hold the CsvRow values
			string[] list = new string[2];
			//Read the rows and Copy the data to the array
			reader.ReadRow (readerRow);
			readerRow.CopyTo (list);
			//Check for "Player" as the first column and skip it
			if (list [0].ToString ().Contains ("Player")) {
				readerRow.Clear ();
				readerRow = new CsvRow ();
				list = new string[2];
				reader.ReadRow (readerRow);
				readerRow.CopyTo (list);
			}
			//Set player names by formatting the string, and parsing to a float

			obj.setPlayerName (string.Format ("{0}", list[0].ToString ()));
			obj.setPoints (float.Parse(list [1]));
			//Add the player Object to the array
			pastPlayers.Add (obj);
			readerRow.Clear ();
		}//End of while loop
		reader.Dispose ();
		reader.Close ();

		return pastPlayers;
	
	}

	public PlayerObject getTopPlayer(){
	
		List <PlayerObject> bestPlayers = ReturnObjects ();
		return bestPlayers[0];
	
	}


	//Getters and Setters for object Structure
	public void setPlayerName(string name){
		this.playerName = name;
	}

	public string getPlayerName(){
		return playerName;
	}

	public void setPoints(float points){
		this.points = points;
	}

	public float getPoints(){
		return points;
	}


}
