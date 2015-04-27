using UnityEngine;
using System.Collections;

public class HazardController : MonoBehaviour {

	public GameObject explosion;
	public AudioClip explosionSound;
	public float heatMax;

	float heatLevel;

	MeshRenderer myRenderer;

	void Start() {
		myRenderer=GetComponentInChildren<MeshRenderer>();
		heatLevel=0f;
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag=="PlayerBullet") {
			//GetComponent<DestructibleObject>().Explode();
			AudioSource.PlayClipAtPoint(explosionSound,transform.position);
			Destroy(gameObject);
			Destroy(other.gameObject);
			transform.parent.gameObject.GetComponent<HazardGeneratorController>().GenerateHazard();
			GameObject tempExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			Destroy(tempExplosion,1f);
			foreach (Vector3 location in Constants.GhostLocations(transform.position)) {
				tempExplosion = Instantiate(explosion, location, transform.rotation) as GameObject;
				Destroy(tempExplosion,1f);
			}
		}
	}

	public void TakeLaserDamage() {
		heatLevel+=Time.deltaTime;
		float colorValue=(heatMax-heatLevel)/heatMax;

		float redValue=1f;
		float greenValue=colorValue;
		float blueValue=colorValue;

		Color newColor=new Color(redValue,greenValue,blueValue);

		myRenderer.material.color=newColor;


		if (heatLevel>=heatMax)
		{
			GetComponent<DestructibleObject>().Explode();
			Instantiate(explosion,transform.position,transform.rotation);
		}
	}

}
