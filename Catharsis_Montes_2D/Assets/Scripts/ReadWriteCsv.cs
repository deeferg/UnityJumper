/*
Author : John Ferguson
Game : Catharsis Montes
Script : ReadWrite namespace
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using UnityEngine;

/* <summary>
 * This namespace has the functionality for writing or reading rows to/from files.</summary>
 * <remarks>CsvFileWriter writes rows one at a time. 
 * CsvFileReader reads the rows one at a time from a file or stream. Also checks for
 * commas and other characters which may require attention.</remarks>
*/
namespace ReadWriteCsv{


	/*<summary>
	 * CsvRow inherits from List for strings, and has the capability
	 * to make a row of text</summary>, and modify it based on column, or read
	 * it all in at once.
	*/
	public class CsvRow : List<string>
	{
		public string LineText { get; set; }
	}

	/*<summary>
	 * The CsvFileWriter inherits from the StreamWriter class
	 * to read in files from a stream or a filename by string.</summary><remarks> Has the capability to write
	 * rows, and check for which column they are.</remarks>
	*/
	public class CsvFileWriter : StreamWriter{


		public CsvFileWriter(Stream stream) : base(stream)
		{
		}

		public CsvFileWriter(string filename) : base(filename)
		{
		}
		/*<summary>
		 * Write row takes a row as a parameter, and builds a string
		 * based on the data contained in the row.</summary><remarks> Then, the data is
		 * inserted in the row's line text from the builder, before
		 * it is written to the file stream.</remarks>
		 * <param name = "row"> the CsvRow being written in</param>

		*/
		public void WriteRow(CsvRow row)
		{
			StringBuilder builder = new StringBuilder ();
			bool firstColumn = true;

			foreach (string value in row) 
			{
				//Add comma to separate values in CSV files if
				//Value is not the first column in the input
				if (!firstColumn)
					builder.Append (',');
				// Check for values that contain commas or quotes
				// Put in special quotes and double up and double quotes
				if (value.IndexOfAny (new char[] { '"', ',' }) != -1)
					builder.AppendFormat ("\"{0}\"", value.Replace ("\"", "\"\""));
				else
					builder.Append (value);
				firstColumn = false;
			}//End of forEach
			row.LineText = builder.ToString ();
			WriteLine (row.LineText);

		}//End of WriteRow?

	}//End of CSVFileWriter
	/*<summary>
	 * Class to read data from CSV file.</summary><remarks> Also inherits functions from StreamReader,
	 * and is able to use it for the constructors. The readRow function is going to
	 * take the values being passed in in the row, and simply return the
	</remarks>*/
	public class CsvFileReader : StreamReader
	{
		public CsvFileReader(Stream stream) : base(stream)
		{
		}

		public CsvFileReader(string filename) : base(filename)
		{
		}
		/* <summary>
		 * ReadRow is a function to read all of the values from the file.</summary><remarks>
		 * pos is an integer which moves through the file one byte at a time.
		 * rows moves through one row at a time and is incremented.</remarks>
		 * <param name = "row"> the CsvRow being read in</param>
		 * <returns> boolean whether read or not</returns>
		*/
		public bool ReadRow(CsvRow row)
		{
			row.LineText = ReadLine ();
			if (String.IsNullOrEmpty (row.LineText))
				return false;
			
			int pos = 0;
			int rows = 0;
			while (pos < row.LineText.Length) {
				string value;
				//Handling for quotations
				if (row.LineText [pos] == '"') {
					pos++;//Skip initial quote
					//Parse Quoted value
					int start = pos;
					while (pos < row.LineText.Length) {
					
							if(row.LineText[pos] == '"'){
								pos++;
								//If two quotes together, keep one
								//Otherwise, end of value reached
								if(pos >= row.LineText.Length || row.LineText[pos] != '"'){
									pos--;
									break;
								}//End of second if in while
							}//End of First if in while
							pos++;
						}//End of while loop
						value = row.LineText.Substring(start, pos - start);
					value = value.Replace ("\"\"", "\"");
					}//End of first if in first while
					else{
						//Parse unquoted value
						int start = pos; 
					while (pos < row.LineText.Length && row.LineText [pos] != ',')
						pos++;
					value = row.LineText.Substring (start, pos - start);
					}//End of else
					//Add field to list
				if (rows < row.Count)
					row [rows] = value;
				else
					row.Add (value);
				rows++;
					//Eat up to and including next comma
				while (pos < row.LineText.Length && row.LineText [pos] != ',')
					pos++;
				if (pos < row.LineText.Length)
					pos++;
				}//End of while
				//Delete any unused items
			while (row.Count > rows)
				row.RemoveAt (rows);

			return(row.Count > 0);
			}//End of ReadRow


		}
	}