using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	[SerializeField] GameObject sparkle;
	[SerializeField] float sparkleTimer;
	float sparkleCountdown;

	void Start() {
		sparkleCountdown=0;
	}

	void Update() {
		if (sparkleCountdown>=0) sparkleCountdown-=Time.deltaTime;

	}

	void OnCollisionEnter(Collision collision) {

		if (collision.collider.gameObject.CompareTag("Player")) {
			Instantiate(sparkle,collision.contacts[0].point,transform.rotation);
			sparkleCountdown=sparkleTimer;
		}
		
	}

	void OnCollisionStay(Collision collision) {

		if (collision.collider.gameObject.CompareTag("Player")) {
			if (sparkleCountdown<=0) {
			Instantiate(sparkle,collision.contacts[0].point,transform.rotation);
				sparkleCountdown=sparkleTimer; }
		}
	}
}
