using UnityEngine;
using System.Collections;

public class GenerateChumps : MonoBehaviour {

	
	public GameObject chumpPasser;
	public int numChumps;
	public float chumpYPosPasser;

	static GameObject chump;
	static GameObject chumpContainer;
	static float chumpYPos;
	// Use this for initialization
	void Start () {
		chump=chumpPasser;
		chumpContainer=gameObject;
		chumpYPos=chumpYPosPasser;

		for (int i=0;i<numChumps;i++) {
			GenerateChump();
		}
	}


	public static void GenerateChump() {
		float chumpX=Constants.RandomAroundRadius()*.75f;
		float chumpZ=Constants.RandomAroundRadius()*.75f;
		float chumpY=chumpYPos;
		GameObject tempChump=Instantiate(chump,new Vector3(chumpX,chumpY,chumpZ),Quaternion.identity) as GameObject;
		tempChump.transform.SetParent(chumpContainer.transform); 
	}
}
