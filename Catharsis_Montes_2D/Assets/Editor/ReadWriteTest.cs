using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text;
using System.IO;
using System;
using ReadWriteCsv;

public class ReadWriteTest {

	[Test]//Print 100 records onto a csv file
	public void WriteTest(){
		using (CsvFileWriter writer = new CsvFileWriter ("000002.csv")) {
			for (int i = 0; i < 100; i++) {
				CsvRow row = new CsvRow ();
				for (int j = 0; j < 5; j++) {
					row.Add (string.Format ("John Ferguson{0}", j));

				}
				writer.WriteRow (row);
			}
			Console.Write ("Tested by John Ferguson");
		}
	
	}
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[Test]//Read the records inserted into the csv file
	public void ReadTest(){
		using (CsvFileReader reader = new CsvFileReader("HighScoreFile.csv"))
		{
			CsvRow row = new CsvRow();
			while(reader.ReadRow(row))
			{
				foreach(string s in row)
				{
					Console.Write (s);
					Console.Write (" ");
				}
				Console.WriteLine();
			}
		}
	
	}
}
