using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour {

	GameObject player; 

	// Use this for initialization
	void Start () {
		player = null; 
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player && player.GetComponent<PlayerController> ().getOnRope ()){

			// apply rotation 
			player.transform.rotation = gameObject.transform.rotation; 
			player.transform.position = gameObject.transform.position - .5f*gameObject.transform.up.normalized;
			
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			player = other.gameObject;
			if (!player.GetComponent<PlayerController> ().getOnRope ()) {
				player.GetComponent<PlayerController> ().setOnRope (gameObject);
			}

		}


	}


}
