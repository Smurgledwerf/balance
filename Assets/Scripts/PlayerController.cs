using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	//private CharacterController controller; 
	private Rigidbody rb; 
	private float speed = 13f;
	private float verticalVelocity; 
	private float gravity = 14f; 
	private float jumpForce = 100f;
	private float turnSpeed = 4f; 
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
	}

	// Check if the player is on the ground by casting a ray directly downward
	// and checking if it hits a collider within a tolerance value 
	bool grounded(float tolerance){
		return Physics.Raycast (transform.position, new Vector3 (0, -1, 0), tolerance);	// tolerance should be roughly the length of the character
	}

	
	// Update is called once per frame
	void Update () {
		
		if(grounded(1.5f)){
			verticalVelocity = -gravity * Time.deltaTime; 

			if (Input.GetKeyDown(KeyCode.Space)){
				verticalVelocity = jumpForce; 
			}
		}
		else{
			verticalVelocity -= gravity * Time.deltaTime; 
		}
			
		// move in 3d based upon input 
		var x = Input.GetAxisRaw("P1Horizontal") * Time.deltaTime;
		var z = Input.GetAxisRaw("P1Vertical") * Time.deltaTime;

		Vector3 motion = new Vector3 (x, 0, z)*speed;

		// orient player along direction of motion 
		if (motion != Vector3.zero)
			transform.rotation = Quaternion.Lerp (transform.rotation,  Quaternion.LookRotation(-motion), Time.deltaTime*turnSpeed);

		transform.Translate (motion * speed * Time.deltaTime, Space.World);
		rb.AddForce (new Vector3 (0, verticalVelocity, 0), ForceMode.Impulse);

	}
}
