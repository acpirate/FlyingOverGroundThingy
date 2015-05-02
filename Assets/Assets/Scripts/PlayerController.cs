using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;
	public float thrustVolume;
	public float minThrustVolume;
	public Text scoreText;

	Rigidbody myBody;
	AudioSource audiosource;
	int score=0;

	void Start() {
		myBody=GetComponent<Rigidbody>();
		audiosource=GetComponent<AudioSource>();

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Loot"))
		{
			Destroy(col.gameObject);
			score++;
			scoreText.text="Score: "+score.ToString();
		}

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
