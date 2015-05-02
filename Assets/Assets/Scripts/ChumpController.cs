using UnityEngine;
using System.Collections;

public class ChumpController : MonoBehaviour {

	enum CHUMPSTATE { WANDER,PANIC};

	public float speedMin;
	public float speedMax;
	public float mindChangeTimeMin;
	public float mindChangeTimeMax;
	public float panicDuration;

	public GameObject chumpCorpse;
	public GameObject bloodSplat;

	private CHUMPSTATE myState;
	private float mindChangeCountdown;
	//Rigidbody myBody;
	private NavMeshAgent myNav;
	private Color normalColor;
	private Material myMaterial;;
	private AnnouncerController announcerController;


	void Awake()
	{
		announcerController=GameObject.FindGameObjectWithTag("Announcer").GetComponent<AnnouncerController>();
		announcerController.OnUnitExplode+=ExplosionResponse;

		myMaterial=GetComponent<MeshRenderer>().material;
		normalColor=myMaterial.color;
		
		myState=CHUMPSTATE.WANDER;
		//myBody=GetComponent<Rigidbody>();
		myNav=GetComponent<NavMeshAgent>();
	}

	void OnDestroy() 
	{
		announcerController.OnUnitExplode-=ExplosionResponse;
	}

	// Use this for initialization
	void Start () {
		ChumpWander();
	}
	
	// Update is called once per frame
	void Update () {
		switch (myState)
		{
			case CHUMPSTATE.WANDER:
			{
				mindChangeCountdown-=Time.deltaTime;
				if (mindChangeCountdown<0) ChumpWander();
				break;
			}
			case CHUMPSTATE.PANIC:
			{
				PanicAction();
				break;
			}
		}
	}

	//called by the explosion or other item if the chump can see it
	public void ActivatePanic(Vector3 panicCauseLocation)
	{
		panicEndTime=Time.time+panicDuration;
		myState=CHUMPSTATE.PANIC;
		myMaterial.color=Color.yellow;
		myNav.speed=speedMax*1.5f;

		Vector3 panicHeading=transform.position-panicCauseLocation;
		Vector3 panicDestination=(panicHeading.normalized*Constants.mapRadius)+transform.position;
		panicDestination.y=transform.position.y;

		//Debug.Log(panicDestination);
			if (myNav.enabled)
			myNav.SetDestination(panicDestination);

	
	}

	//called when the chump stops panicing
	void DeactivatePanic()
	{
		myState=CHUMPSTATE.WANDER;
		myMaterial.color=normalColor;
		ChumpWander();
	}

	//chump panicing behavior
	void PanicAction()
	{
		Vector3 offset = myNav.destination - transform.position;
		float sqrLen = offset.sqrMagnitude;
		if (sqrLen < 1f)
			DeactivatePanic();

	}

	//called when the chump is killed
	public void ChumpDie()
	{				
		Quaternion chumpCorpseRotation=Quaternion.Euler(
			new Vector3(90f,0,Random.Range(0.0f,360.0f)));
		Vector3 chumpCorpsePosition = new Vector3 (transform.position.x,
		                                           -9.77f,transform.position.z);

		Vector3 splatPosition=new Vector3(transform.position.x,-9.94f,transform.position.z);
		Quaternion splatRotation=Quaternion.Euler(new Vector3(90f,0f,0f));

		Instantiate(chumpCorpse,chumpCorpsePosition,chumpCorpseRotation);
		Instantiate(bloodSplat,splatPosition,splatRotation);
		GetComponent<NavMeshAgent>().enabled=false;
		Destroy(gameObject);

	

	}

	void ExplosionResponse(GameObject explodedUnit, float explodeRaidus)
	{
		Vector3 offset =  explodedUnit.transform.position - transform.position;

		float squareDistance = offset.sqrMagnitude;

		if (squareDistance < explodeRaidus * explodeRaidus )
		{
			ChumpDie();
		}

		if (squareDistance < Constants.chumpVisionRadius * Constants.chumpVisionRadius)
		{
			ActivatePanic(explodedUnit.transform.position);
		}


		//Debug.Log("there was an explosion!");
	}

	//chump wandering behavior
	void ChumpWander() {
		//float rotationHolder=Random.Range(1,360);
		float speedHolder=Random.Range(speedMin,speedMax);

		//transform.rotation=Quaternion.Euler(0,rotationHolder,0);
		//myBody.velocity=transform.forward*speedHolder;
		Vector3 myDestination=new Vector3(Constants.RandomAroundRadius()*.75f,
		                                  transform.position.y,
		                                  Constants.RandomAroundRadius()*.75f);

		myNav.speed=speedHolder;
		myNav.SetDestination(myDestination);

		mindChangeCountdown=Random.Range(mindChangeTimeMin,mindChangeTimeMax);
	}
}
