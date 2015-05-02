using UnityEngine;
using System.Collections;

public class AsteroidLootController : MonoBehaviour {

	public float lifeTime;
	public float lootSpeed;

	private Rigidbody myBody;

	// Use this for initialization
	void Start () {
		myBody=GetComponent<Rigidbody>();

		transform.rotation=Quaternion.Euler(0,Random.Range(0,360),0);
		myBody.velocity=transform.forward*lootSpeed;

		//Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
