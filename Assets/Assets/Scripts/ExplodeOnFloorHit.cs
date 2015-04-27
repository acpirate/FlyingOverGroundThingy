using UnityEngine;
using System.Collections;

public class ExplodeOnFloorHit : MonoBehaviour {

	public GameObject explosion;
	public float killRadius=4f;
	public GameObject chumpCorpse;

	void OnCollisionEnter (Collision col) 
	{
		if (col.gameObject.CompareTag("Ground")) 
		{
			Instantiate(explosion,transform.position,transform.rotation);
			PanicChumpsInRadius();
			KillChumpsInRadius();

			Destroy(gameObject);

		}
	}

	void KillChumpsInRadius() {

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, killRadius);
		int i = 0;
		while (i < hitColliders.Length) {

			Collider hitCollider=hitColliders[i];
			if (hitCollider.gameObject.CompareTag("Chump"))
			{
				hitCollider.gameObject.GetComponent<ChumpController>().ChumpDie();
			}
			i++;
		}

	}

	void PanicChumpsInRadius() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, Constants.chumpVisionRadius);
		int i = 0;
		while (i < hitColliders.Length) {
			
			Collider hitCollider=hitColliders[i];
			if (hitCollider.gameObject.CompareTag("Chump"))
			{
				hitCollider.gameObject.GetComponent<ChumpController>().ActivatePanic(transform.position);
				
			}
			i++;
		}
	}

}
