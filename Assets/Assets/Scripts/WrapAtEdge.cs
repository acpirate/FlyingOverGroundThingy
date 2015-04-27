using UnityEngine;
using System.Collections;

public class WrapAtEdge : MonoBehaviour {

	enum POSITIONCODE {XPOS,XNEG,ZPOS,ZNEG};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x>Constants.mapRadius) PostionWrap(POSITIONCODE.XPOS);
		if (transform.position.x<-Constants.mapRadius) PostionWrap(POSITIONCODE.XNEG);
		if (transform.position.z>Constants.mapRadius) PostionWrap(POSITIONCODE.ZPOS);
		if (transform.position.z<-Constants.mapRadius) PostionWrap(POSITIONCODE.ZNEG);

	}

	void PostionWrap(POSITIONCODE positionCode) {
		float positionX=transform.position.x;
		float positionY=transform.position.y;
		float positionZ=transform.position.z;

		switch (positionCode) {
		case POSITIONCODE.XNEG:
			positionX=transform.position.x+Constants.mapRadius*2;
			break;
		case POSITIONCODE.XPOS:
			positionX=transform.position.x-Constants.mapRadius*2;
			break;
		case POSITIONCODE.ZNEG:
			positionZ=transform.position.z+Constants.mapRadius*2;
			break;
		case POSITIONCODE.ZPOS:
			positionZ=transform.position.z-Constants.mapRadius*2;
			break;
		}

		transform.position=new Vector3(positionX,positionY,positionZ);
	}
}
