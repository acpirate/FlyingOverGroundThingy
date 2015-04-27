using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GenerateObstacles : MonoBehaviour {

	public List<GameObject> obstacleTypes=new List<GameObject>();
	public int numObstacles;
	public float exclusionRange;
	public float minScale;
	public float maxScale;

	void Start() {
		for(int i=0;i<numObstacles;i++) {
			GameObject tempObstacle=Instantiate(obstacleTypes[Random.Range(0,obstacleTypes.Count)]);
			float tempX=0f;
			float tempZ=0f;
			//while (Mathf.Abs(tempX)<exclusionRange) {
				tempX=Random.Range(Constants.mapRadius*-1,Constants.mapRadius);
			//}
			//while (Mathf.Abs(tempZ)<exclusionRange) {
				tempZ=Random.Range(Constants.mapRadius*-1,Constants.mapRadius);
			//}
			tempObstacle.transform.localScale=new Vector3(
				Random.Range(minScale,maxScale),
				Random.Range(minScale,maxScale),
				Random.Range(minScale,maxScale));
			tempObstacle.transform.position=new Vector3(
				tempX,
				tempObstacle.transform.localScale.y*.5f-10f,
				tempZ);
			tempObstacle.transform.localEulerAngles=new Vector3(0f,Random.Range(0,359),0);
			tempObstacle.transform.parent=transform;
			tempObstacle.AddComponent(System.Type.GetType("WrapProjection"));

		}
	}


}
