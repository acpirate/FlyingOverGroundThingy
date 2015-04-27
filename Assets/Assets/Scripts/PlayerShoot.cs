using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public GameObject bullet;
	public float shootDelay;
	public float shotSpeed;
	public float bulletOffset;

	float nextShotTime;
	AudioSource audioSource;

	void Start() {

		audioSource=GetComponent<AudioSource>();
		nextShotTime=Time.time;
	}

	void Update () {

		if (Input.GetButton("Fire1")) {
			if (Time.time>nextShotTime) {
				audioSource.Play();

				GameObject shotHolder=Instantiate(bullet,
				            			transform.position+transform.forward*bulletOffset,
				            			transform.rotation) as GameObject;
				shotHolder.GetComponent<Rigidbody>().velocity=transform.forward*shotSpeed;
				nextShotTime=Time.time+shootDelay;
			}
		}
		
	}
}
