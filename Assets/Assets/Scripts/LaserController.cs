using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaserController : MonoBehaviour {

	enum LASERSTATE {OFF, CHARGING, FIRING, COOLDOWN};
	enum PLAYSTATE {FORWARD, BACKWARD};


	public GameObject player;
	public GameObject laserOff;
	public float laserOffset;
	public AudioClip clip_laserCharge;
	public AudioClip clip_laserChargeReverse;
	public AudioClip clip_laserFire;
	public Slider slider_ChargeDisplay;

	float laserChargeTimeHolder;
	float laserChargeTimer;
	GameObject laserFire;
	LineRenderer laserLine;
	AudioSource myAudioSource;
	LASERSTATE laserState;
	ParticleSystem laserChargeVisual;
	LayerMask shootableMask;
	Transform particleBackstop;


	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Shootable");

		laserState=LASERSTATE.OFF;
		//charge time is equal to the length of the audio clip - dont think there is a way to make it dynamic
		laserChargeTimeHolder=clip_laserCharge.length;
		particleBackstop=GameObject.FindGameObjectWithTag("ParticleBackstop").transform;

		laserFire=transform.FindChild("LaserFire").gameObject;
		laserLine=laserFire.GetComponent<LineRenderer>();
		myAudioSource=GetComponent<AudioSource>();
		laserChargeVisual=transform.FindChild("LaserChargeVisual").GetComponent<ParticleSystem>();
		laserChargeVisual.Stop();

		laserFire.SetActive(false);
		laserChargeTimer=0;

	}
	
	// Update is called once per frame
	void Update () {



		//firebutton hit
		if (Input.GetButton("Fire1")) {
			switch (laserState) {
			case LASERSTATE.OFF:
				laserState=LASERSTATE.CHARGING;
				break;
			case LASERSTATE.CHARGING:
				laserChargeTimer+=Time.deltaTime;
				myAudioSource.clip=clip_laserCharge;
				if (!(laserChargeVisual.isPlaying)) laserChargeVisual.Play();
				if (!(myAudioSource.isPlaying)) myAudioSource.Play();
				if (laserChargeTimer>=laserChargeTimeHolder) 
					laserState=LASERSTATE.FIRING;
				break;
			case LASERSTATE.FIRING:
				LaserShoot();
				laserChargeVisual.Stop();
				if (!laserFire.activeSelf) laserFire.SetActive(true);
				break;

			}
		}
		//fire button not hit
		if (!Input.GetButton("Fire1")) {
			switch (laserState) {
			case LASERSTATE.CHARGING:
			case LASERSTATE.COOLDOWN:
				laserChargeVisual.Stop();
				myAudioSource.clip=clip_laserChargeReverse;
				if (laserChargeTimer>0)
					if (!(myAudioSource.isPlaying)) myAudioSource.Play();
				laserChargeTimer-=Time.deltaTime;
				if (laserChargeTimer<=0) laserState=LASERSTATE.OFF;
				break;
			case LASERSTATE.FIRING:
				laserFire.SetActive(false);
				LaserSmokeDislay();

				laserState=LASERSTATE.COOLDOWN;
				break;
			}
		}

		slider_ChargeDisplay.value=100*(1-(laserChargeTimeHolder-laserChargeTimer)/laserChargeTimeHolder);
		laserChargeTimer=Mathf.Clamp(laserChargeTimer,0,laserChargeTimeHolder);
	}

	void LaserSmokeDislay() {
		GameObject tempLaserOff = (GameObject) Instantiate (laserOff,player.transform.position,player.transform.rotation);
		
		tempLaserOff.transform.SetParent(gameObject.transform);
		
		Vector3 tempLaserOffPosition = new Vector3(
			0,0,particleBackstop.localPosition.z/2);
		
		Vector3 tempLaserOffScale = new Vector3(
			1,1,particleBackstop.localPosition.z);

			
			tempLaserOff.transform.localPosition=tempLaserOffPosition;

		
		tempLaserOff.transform.SetParent(null);
		tempLaserOff.transform.localScale=tempLaserOffScale;


	}

	void LaserShoot() {
		Ray shootRay = new Ray();
		RaycastHit shootHit = new RaycastHit();


		shootRay.origin = transform.position;
		shootRay.direction = player.transform.forward;
		//Debug.Log("transform forward = "+transform.forward+" shootRay.direction ="+shootRay.direction + "player direction = " + player.transform.forward
		 //         );
		if(Physics.Raycast (shootRay, out shootHit, 40, shootableMask))
		{
			/*EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (damagePerShot, shootHit.point);
			} */

			shootHit.collider.gameObject.GetComponent<HazardController>().TakeLaserDamage();

			float hitDistance=Vector3.Distance(transform.position,shootHit.point);
			laserLine.SetPosition(1, new Vector3(0,0,hitDistance));
			particleBackstop.localPosition=new Vector3(0,0,hitDistance);
		}
		else
		{
			laserLine.SetPosition (1, new Vector3(0,0,40));
			particleBackstop.localPosition=new Vector3(0,0,40);
		}

	}



}
