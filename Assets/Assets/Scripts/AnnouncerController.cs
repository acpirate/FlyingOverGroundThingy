using UnityEngine;
using System.Collections;

public delegate void UnitExploded(GameObject unit, float explodeRadius);
public delegate void ChumpDied(GameObject chumpThatDied);

public class AnnouncerController : MonoBehaviour {

	public event UnitExploded OnUnitExplode;
	public event ChumpDied OnChumpDied;

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

	public void AnnounceChumpDied(GameObject chump)
	{
		OnChumpDied(chump);
	}
}
