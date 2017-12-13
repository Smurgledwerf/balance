using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody rb; 
	private float speed = 13f;
	private Vector3 jumpVector; 
	private float gravity = 14f; 
	private float jumpForce = 100f;
	private float turnSpeed = 4f; 
	private bool onRope = false; 
	private GameObject rope; 

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		jumpVector = Vector3.zero;
	}

	// Check if the player is on the ground by casting a ray directly downward
	// and checking if it hits a collider within a tolerance 
	bool grounded(float tolerance){
		return Physics.Raycast (transform.position, new Vector3 (0, -1, 0), tolerance);	// tolerance should be roughly the length of the character
	}

	public void setOnRope(GameObject r){
		onRope = true; 	
		rope = r; 
	}

	public bool getOnRope(){
		return onRope;
	}
		
	// Update is called once per frame
	void Update () {

		// TODO make it do the goddamn thing it's fucking supposed to
		// also stop using so many magic numbers or at least make them macros
		if (grounded (1.5f)) {	

			jumpVector.y = -gravity * Time.deltaTime;
			if (Input.GetKeyDown (KeyCode.Space)) {
				jumpVector.y = jumpForce; 
			}
		} else if (onRope) {
			/*
			Vector3 negative = (Vector3.zero - jumpVector).normalized; 

			jumpVector.x += negative.x * 100 * Time.deltaTime; 
			jumpVector.z += negative.z * 100 * Time.deltaTime;

			//jumpVector.x += (0 - jumpVector.x) * gravity * Time.deltaTime;
			*/
			rb.Sleep (); 
			if (Input.GetKeyDown (KeyCode.Space)) {

				// jump vector perpendicular to the player's current rotation 
				//var cross = Vector3.Cross (transform.right, transform.localEulerAngles.normalized);
				//jumpVector = cross * 12; 

				// using rope velocity  
				rb.WakeUp();
				Vector3 jump = rope.GetComponent<Rigidbody> ().velocity * 5; 
				jump.y = 5; 
				jumpVector = jump; 
				transform.localEulerAngles = Vector3.zero;
				rb.velocity = Vector3.zero;
				onRope = false;
			}
		}

		// jump dampening force 
		Vector3 negative = (Vector3.zero - jumpVector);
		float deltaX = negative.x * 10 * Time.smoothDeltaTime;
		float deltaZ = negative.z * 10 * Time.smoothDeltaTime;
		Vector3 newVel = new Vector3 (rb.velocity.x + deltaX, rb.velocity.y, rb.velocity.z + deltaZ);

		jumpVector.x += deltaX;
		jumpVector.z += deltaZ;
		jumpVector.y -= gravity * Time.deltaTime; 

		// move in 3d based upon input 
		var x = Input.GetAxis ("P1Horizontal") * Time.smoothDeltaTime;
		var z = Input.GetAxis ("P1Vertical") * Time.smoothDeltaTime;

		Vector3 motion = new Vector3 (x, 0, z) * speed;

		// orient player along direction of motion 
		if (motion != Vector3.zero)
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (-motion), Time.deltaTime * turnSpeed);

		transform.Translate (motion * speed * Time.deltaTime, Space.World);

		if (onRope){	// TODO distinguish a "swingable" rope vs pendulum 
			rope.GetComponent<Rigidbody>().AddForce (motion * speed * Time.deltaTime, ForceMode.VelocityChange);
		}
			
		rb.AddForce (jumpVector, ForceMode.Impulse);

	}
}
