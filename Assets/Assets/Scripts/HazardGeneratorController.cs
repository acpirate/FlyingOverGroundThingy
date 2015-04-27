using UnityEngine;
using System.Collections;

public class HazardGeneratorController : MonoBehaviour {

	public GameObject hazard;
	public float hazardSpeedMax;
	public float hazardSpeedMin;
	public float hazardRotation;
	public float hazardNumber;

	// Use this for initialization
	void Start () {
		for (int i=0;i<hazardNumber;i++) {
			GenerateHazard();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateHazard() {
		GameObject hazardHolder = Instantiate(hazard);
		Vector3 hazardPositionHolder=new Vector3(Random.Range(-Constants.mapRadius,Constants.mapRadius),
		                                   0f,
		                                   Random.Range(-Constants.mapRadius,Constants.mapRadius));
		Vector3 hazardRotationHolder=Random.insideUnitCircle*hazardRotation;
		Vector3 hazardVelocityHolder=new Vector3(Random.Range(-hazardSpeedMax,hazardSpeedMax),
		                                         0f,
		                                         Random.Range(-hazardSpeedMax,hazardSpeedMax));


		hazardHolder.transform.position=hazardPositionHolder;
		hazardHolder.GetComponent<Rigidbody>().AddTorque(hazardRotationHolder);
		hazardHolder.GetComponent<Rigidbody>().velocity=hazardVelocityHolder;
		hazardHolder.transform.SetParent(transform);


	}
}
