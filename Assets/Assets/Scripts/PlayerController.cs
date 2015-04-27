using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;
	public float thrustVolume;
	public float minThrustVolume;

	Rigidbody myBody;
	AudioSource audiosource;

	void Start() {
		myBody=GetComponent<Rigidbody>();
		audiosource=GetComponent<AudioSource>();

	}

	void FixedUpdate() {
		float moveInput=Input.GetAxis("Vertical");
		float turnInput=Input.GetAxis("Horizontal");


		myBody.velocity=transform.forward*moveSpeed*moveInput;
		transform.Rotate(transform.up,turnSpeed*turnInput*Time.deltaTime);
		audiosource.volume=moveInput*thrustVolume+minThrustVolume;
		//Debug.Log(GetComponent<Rigidbody>().velocity);
	}


}
