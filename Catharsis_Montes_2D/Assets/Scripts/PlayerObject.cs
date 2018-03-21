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

/*<summary>Player Object is a class that does most of the computing of
* the .csv files, and the players in the high score standings.</summary>
* It is a container class for the data in the files.
*/
public class PlayerObject {
	//<value>Objects for the players stats</value>
	string playerName;
	float points = 0;
	//<value>A reference to ReadWrite's namespace object</value>
	CsvRow readRow;

	/*<summary>
	 * Constructor for PlayerObject, and overloaded constructors</summary>
	*/
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
	/*<summary>
	*Method for writing the high score from the game to a single value
	*in a csv file</summary><remarks> that is empty so that no other high scores are overwritten.
	*Enables keeping a temp file, a building file, and one to send to server
	*(if gets to point of multi-threading and sending requests with multiple clients)
	*
	*Add the players name who belongs to this instantiation of PlayerObject
	*To the first row, and the points score in the second row, Then write the Row
	*to the CsvFileWriter IO and dispose and close the writer to prevent leaks.</remarks>
	*/
	public void WriteHighScore(){

		CsvFileWriter writer = new CsvFileWriter ("000001.csv");
		CsvRow row = new CsvRow();
		row.Add (playerName);
		row.Add (string.Format("{0}" ,points));
		Console.Write (string.Format("You scored :{0}", points));
		writer.WriteRow (row);
		writer.Dispose ();
		writer.Close ();


	}
		

	/*<summary>
	 * Method for filling out HighScoreFile.csv after a user has run the track.</summary><remarks>
	 *Instantiates List of PlayerObjects, and builds it by iterating through
	 *the last known copy of HighScoreFile.csv, then the most recent copy of 000001.csv,
	 *which is responsible for holding the last players score and create a new PlayerObject
	 *for each row, and inserting it into the List of PlayerObjects. 000000.csv works in the same
	 *capacity, but reads from a modified dataset(pre-approved) provided by Stanley Pieda, and disects 
	 *the 6 columns of data and only takes the name, and value columns to build a playerObject.
	 *At the end of the loops, it opens a writer and builds a new HighScoreFile.csv with the updated list,
	 *after it performs a sort algorithm to list the Objects by highest score to lowest.</remarks>
	 */
	public void FillHighScores(){
		//<value>Row to read values from Other files(dataset, new high score in 000001.csv)</value>
		CsvRow readerRow;
		//<value>Row to insert values into CsvFileWriter below for HighScoreFile good copy</value>
		CsvRow row = new CsvRow();
		//<value>Reader Object for New High Score (or old if run early)</value>
		CsvFileReader reader = new CsvFileReader ("HighScoreFile.csv");
		//<value>Array of Player Objects to fill with readerRow's values read</value>
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

		/*
		* loop reading through the dataset supplied by professor.
		* Attempting to build break to refrain from overwriting records supplied.
		*/
		reader = new CsvFileReader ("000000.csv");

		while(!reader.EndOfStream){
			
			PlayerObject obj = new PlayerObject();
			readerRow = new CsvRow();
			string[] list = new string[6];

			reader.ReadRow (readerRow);
			readerRow.CopyTo(list);
			obj.setPlayerName(string.Format("{0}", list [2].ToString ()));
			obj.setPoints (float.Parse(list[5]));

			/*
			 * Attempt to check if row has same value in high scores already
			*/
			for (int i = 0; i < pastPlayers.Count; i++) {
				if (obj.getPlayerName ().Equals (pastPlayers [i].getPlayerName ()) && obj.getPoints () == pastPlayers [i].getPoints ())
					continue;
			}

			pastPlayers.Add(obj);
			readerRow.Clear ();
		}

		reader.Dispose ();
		reader.Close ();

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

	}
	/*<summary>
	 * Method responsible for filling the PlayerObject List for use elsewhere in code.</summary>
	 * <remarks>Only uses HighScores file, so can be used before player finishes their run to
	 * find the highest score to display, or top 10, or can return it with the user
	 * after they have completed their run.</remarks>
	*/
	public List<PlayerObject> ReturnObjects(){
	
		//Row to read values from Other files(dataset, new high score in 000001.csv)
		CsvRow readerRow;
		//Reader Object for New High Score (or old if run early)
		CsvFileReader reader = new CsvFileReader ("HighScoreFile.csv");
		//Array of Player Objects to fill with readerRow's values read
		List<PlayerObject> pastPlayers = new List<PlayerObject> ();

		//Loop through the reader till the end
		while (!reader.EndOfStream) {
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

	/*<summary>
	 * Function responsible for returning the best player in the list.</summary>
	 * <remarks>Calls to return all of the current players with high scores,
	 * and simply returns whoever is at the top of the list.</remarks>
	*/
	public PlayerObject getTopPlayer(){
		
		List <PlayerObject> bestPlayers = ReturnObjects ();
		return bestPlayers[0];
	
	}
	/*<summary>
	 * Function for returning a playerObject of the last person to run the course.</summary>
	 * Mostly used to call the players score who has just run in the GameOver canvas.
	*/
	public PlayerObject returnLastPlayer(){
		CsvFileReader reader = new CsvFileReader ("000001.csv");
		PlayerObject obj;

		obj = new PlayerObject();
		CsvRow readerRow = new CsvRow();
		string[] list = new string[2];

		//Read the rows and Copy the data to the array
		reader.ReadRow (readerRow);
		readerRow.CopyTo (list);

		//Set player names by formatting the string, and parsing to a float
		obj.setPlayerName (string.Format ("{0}", list [0].ToString ()));
		obj.setPoints (float.Parse(list [1]));

		//Add the player Object to the array
		readerRow.Clear ();
		reader.Dispose ();
		reader.Close ();

		return obj;
	}


	//Getters and Setters for object Structure
	//<param name = "name"> string value for name to set to</param>
	public void setPlayerName(string name){
		this.playerName = name;
	}
	//<returns> returns player name as string</value>
	public string getPlayerName(){
		return playerName;
	}
	//<param name = "points"> float value for number of points</param>
	public void setPoints(float points){
		this.points = points;
	}
	//<returns> returns number of points with player Object</returns>
	public float getPoints(){
		return points;
	}


}
