using UnityEngine;
using System.Collections;

public delegate void UnitExploded(GameObject unit, float explodeRadius);

public class AnnouncerController : MonoBehaviour {

	public event UnitExploded OnUnitExplode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnnounceExplosion(GameObject unit, float explodeRadius)
	{
		OnUnitExplode(unit,explodeRadius);
	}
}
