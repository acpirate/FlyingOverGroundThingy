using UnityEngine;
using System.Collections;

public class ChumpController : MonoBehaviour {

	enum CHUMPSTATE { WANDER, PANIC, HUNTING, SHOOTING};

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
	private Material myMaterial;
	private AnnouncerController announcerController;
	private bool isHostile=false;


	void Awake()
	{
		announcerController=GameObject.FindGameObjectWithTag("Announcer").GetComponent<AnnouncerController>();
		announcerController.OnUnitExplode+=ExplosionResponse;
		announcerController.OnChumpDied+=ChumpDiedResponse;

		myMaterial=GetComponent<MeshRenderer>().material;
		normalColor=myMaterial.color;
		
		myState=CHUMPSTATE.WANDER;
		myNav=GetComponent<NavMeshAgent>();
	}

	void OnDestroy() 
	{
		announcerController.OnUnitExplode-=ExplosionResponse;
		announcerController.OnChumpDied-=ChumpDiedResponse;
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

	//turn on hostile state
	void MakeHostile()
	{
		normalColor=Color.red;
		isHostile=true;
		myMaterial.color=normalColor;
	}


	//activate panic state, change color to panic color, increase speed to panic speed, choose panic destination directly
	//opposite from panice cause location
	void ActivatePanic(Vector3 panicCauseLocation)
	{
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

	//turn off panic, return to normal color and activate wander state
	void DeactivatePanic()
	{
		myState=CHUMPSTATE.WANDER;
		myMaterial.color=normalColor;
		ChumpWander();
	}

	//check every frame to see if the unit made it to its panic position, if it did then turn off panic
	void PanicAction()
	{
		Vector3 offset = myNav.destination - transform.position;
		float sqrLen = offset.sqrMagnitude;
		if (sqrLen < 1f)
			DeactivatePanic();

	}

	//destory self, create a randomly positioned and rotated corpse
	void ChumpDie()
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

		//announce that he died
		announcerController.AnnounceChumpDied(gameObject);
		Destroy(gameObject);

	

	}

	//react to chump died event, set self to hostile if within vision radius
	void ChumpDiedResponse(GameObject chumpThatDied)
	{
		Vector3 offset =  chumpThatDied.transform.position - transform.position;
		
		float squareDistance = offset.sqrMagnitude;
		
		if (squareDistance < Constants.chumpVisionRadius * Constants.chumpVisionRadius )
		{
			MakeHostile();
		}
	}

	//handle receiving an explosion event, if the unit is within the radius kill it, if the explosion is in sight range panic
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

	}

	//choose a random speed, random destination, and a random time when to change the chumps mind and go somewher else
	void ChumpWander() {
		float speedHolder=Random.Range(speedMin,speedMax);


		Vector3 myDestination=new Vector3(Constants.RandomAroundRadius()*.75f,
		                                  transform.position.y,
		                                  Constants.RandomAroundRadius()*.75f);

		myNav.speed=speedHolder;
		myNav.SetDestination(myDestination);

		mindChangeCountdown=Random.Range(mindChangeTimeMin,mindChangeTimeMax);
	}
}
