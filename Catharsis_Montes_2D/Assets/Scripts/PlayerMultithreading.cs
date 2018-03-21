/*
Author : John Ferguson
Game : Catharsis Montes
Script : PlayerMultithreading
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*<summary>
 * This class is responsible for the multithreading at the beginning of the game</summary>
 * <remarks>It is a super class for ThreadReader, which supplies already filled functions,
 * and functions to be overridden and filled with what to run during the thread.</remarks>
*/
public class PlayerMultithreading  {


	private bool isDone = false;
	//<value> handle is the lock object to check for isDone</value>
	private object handle = new object();
	private System.Threading.Thread thread = null;
	/*
	 * <summary>isDone checks whether the lock is still on the handle object</summary>,
	 * and sets the temp boolean to either false, or the isDone boolean
	 * to the handles value.
	*/

	public bool IsDone{
		get{
			bool tmp;
			lock (handle) {
				tmp = isDone;
			}
			return tmp;
		}
		set{ 
			lock (handle) {
				isDone = value;
			}
		}
	}
	/*<summary> 
	 * Start is the instantiation of the thread when run is called.
	 * </summary>
	*/
	public virtual void Start(){
		thread = new System.Threading.Thread (Run);
		thread.Start ();
	}
	public virtual void Abort(){
		thread.Abort ();
	}
	/* <summary>
	 * ThreadFunction is a class to be overridden in sub-classes.</summary>
	 * the code that is to be run in the thread all goes in here.
	*/
	protected virtual void ThreadFunction(){}
	/*<summary>
	 * OnFinished is called when ThreadFunction ends, and everything can be
	 * cleaned up</summary>
	*/
	protected virtual void OnFinished(){}
	/*<summary>
	 * Update checks the IsDone status and either puts it to true, and triggers 
	 * OnFinished, or sets it to false.</summary>
	*/
	public virtual bool Update(){
		if(IsDone){
			OnFinished ();
			return true;
		}
		return false;
	}
	/*<summary>
	 * WaitFor is a check on update to hold the thread if necessary.</summary>
	*/
	public IEnumerator WaitFor(){
		while (!Update ()) {
			yield return null;
		}
	}
	/*<summary>
	 * Run is what is called to begin the thread.</summary>
	*/
	private void Run(){
		ThreadFunction ();
		IsDone = true;
	}

}
