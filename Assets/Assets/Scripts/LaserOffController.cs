using UnityEngine;
using System.Collections;

public class LaserOffController : MonoBehaviour {

	public float lifeTime=2f;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().startLifetime=lifeTime;
		Destroy(gameObject,lifeTime);
	}
}
