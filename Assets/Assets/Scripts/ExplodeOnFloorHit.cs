using UnityEngine;
using System.Collections;

public class ExplodeOnFloorHit : MonoBehaviour {

	public GameObject explosion;
	public float killRadius=4f;

	private AnnouncerController announcerController;

	void Awake()
	{
		announcerController=GameObject.FindGameObjectWithTag("Announcer").GetComponent<AnnouncerController>();
	}

	void OnCollisionEnter (Collision col) 
	{
		if (col.gameObject.CompareTag("Ground")) 
		{	
			//make explosion effect
			Destroy(Instantiate(explosion,transform.position,transform.rotation),5f);
			//announce explosion
			announcerController.AnnounceExplosion(gameObject,killRadius);
			//destroy the shrapnel
			Destroy(gameObject);

		}
	}



}
