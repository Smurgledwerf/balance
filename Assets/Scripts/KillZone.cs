using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	public Transform Spawn;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GameObject player = other.transform.parent.gameObject;
			player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			player.transform.position = Spawn.position;
		} else {
			Destroy (other.gameObject);
		}
	}

}
