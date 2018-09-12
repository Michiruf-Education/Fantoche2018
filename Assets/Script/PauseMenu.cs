using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	GameObject[] pauseObjects;
	public bool Paused;

	void Start () {

		pauseObjects = GameObject.FindGameObjectsWithTag ("ShowOnPause");

		hidePaused ();

		Paused = false;
	}

	void Update (){
		if (Paused == false) {
			if (Input.GetKeyDown (KeyCode.P)) {
				showPaused ();
			}
		}
				
		if (Paused == true) {
			
				if (Input.GetKeyDown (KeyCode.P)) {
					hidePaused ();
				}
		}
	}


	public void showPaused(){

		foreach (GameObject g in pauseObjects) {
			g.SetActive (true);
		}
		Paused = true;
	}

	public void hidePaused(){
		foreach (GameObject g in pauseObjects) {
			g.SetActive (false);
		}

		Paused = false;
	}


				/*public void Load_Play(){

		SceneManager.LoadScene ("MainGame");
	}*/
	public void End_Game(){

		Application.Quit ();
	}


}

