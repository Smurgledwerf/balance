using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	private MenuCamera CameraControl;

	// Use this for initialization
	void Start () {
		CameraControl = Camera.main.GetComponent<MenuCamera> ();
	}
	
	public void StartGame(){
		//print ("starting the game");
		SceneManager.LoadScene ("physics_test");
	}

	public void ShowOptions(){
		//print ("showing options menu");
		GameObject options = GameObject.Find ("OptionsMenu");
		Vector3 position = options.transform.position;
		CameraControl.StartTransition (position);
	}

	public void ShowMainMenu(){
		GameObject options = GameObject.Find ("MainMenu");
		Vector3 position = options.transform.position;
		CameraControl.StartTransition (position);
	}
}
