using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {

	public float TransitionTime = 0.5f;

	private bool Transitioning = false;
	private Vector3 StartPos;
	private Vector3 EndPos;
	private float StartTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Transitioning) {
			float currentTime = (Time.time - StartTime) / TransitionTime;
			transform.position = Vector3.Lerp (StartPos, EndPos, currentTime);
			if (currentTime > 1f) {
				Transitioning = false;
			}
		}
	}

	public void StartTransition(Vector3 newPos){
		//print ("starting a transition");
		StartPos = transform.position;
		EndPos = newPos;
		StartTime = Time.time;
		Transitioning = true;
	}
}
