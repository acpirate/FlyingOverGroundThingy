using UnityEngine;
using System.Collections;

public class RandomShatter : MonoBehaviour {

	public float shatterRotation;
	public float shatterSpeed;

	// Use this for initialization
	void Start () {
		Vector3 shatterRotationHolder=shatterRotation*Random.insideUnitSphere;
		Vector3 shatterSpeedHolder=shatterSpeed*Random.insideUnitSphere;
		if (shatterSpeedHolder.y<0) {
			shatterSpeedHolder=new Vector3(shatterSpeedHolder.x, -shatterSpeedHolder.y,shatterSpeedHolder.z);
		}

		GetComponent<Rigidbody>().velocity=shatterSpeedHolder;
		GetComponent<Rigidbody>().AddTorque(shatterRotationHolder);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
