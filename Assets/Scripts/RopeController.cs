using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour {

	private Vector3 prevPos; 
	Quaternion prevRot; 
	bool playerOn;
	GameObject player; 

	// Use this for initialization
	void Start () {
		prevPos = gameObject.transform.position;
		prevRot = gameObject.transform.rotation;
		playerOn = false; 
		player = null; 
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerOn && player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ().getOnRope ()){

			// get calculate position delta and apply
			Vector3 currPos = gameObject.transform.position; 
			Vector3 delta = currPos - prevPos; 
			//print (delta); 

			//apply delta to character 
			// player.transform.position += delta; 


			Vector3 localRot = gameObject.transform.localEulerAngles.normalized;

			// apply rotation 
			player.transform.rotation = gameObject.transform.rotation; 
			player.transform.position = gameObject.transform.position - .7f*gameObject.transform.forward.normalized;
			
		}
		else{
			playerOn = false; 
		}
		prevPos = gameObject.transform.position;
	}

	public void setPlayerOn(bool p){
		playerOn = p;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			playerOn = true; 
			print ("player on");
			player = other.gameObject;
			player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ().setOnRope (true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player"){
			//playerOn = false; 
			//print ("player off");
			//player = null; 
			//other.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ().setOnRope (false);
		}
	}




}
