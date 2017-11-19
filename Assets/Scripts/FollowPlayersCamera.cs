using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayersCamera : MonoBehaviour {

	public float Distance = 20f;
	private Vector3 offset;
	private GameObject[] playerGameObjects;

	// Use this for initialization
	void Start () {
		playerGameObjects = GameObject.FindGameObjectsWithTag ("Player");
		Vector3 direction = transform.rotation * Vector3.forward;
		offset = direction * Distance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 midpoint = GetMidpoint (playerGameObjects);
		transform.position = midpoint - offset;
	}

	private Vector3 GetMidpoint(GameObject[] players){
		float x = 0f;
		float y = 0f;
		float z = 0f;
		foreach (GameObject obj in players) {
			Vector3 position = obj.transform.position;
			x += position.x;
			y += position.y;
			z += position.z;
		}

		return new Vector3 (x / players.Length, y / players.Length, z / players.Length);
	}
}
