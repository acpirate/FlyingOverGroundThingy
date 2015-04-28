using UnityEngine;
using System.Collections;

//[ExecuteInEditMode()]  

public class ExplosionMat : MonoBehaviour {
	
	bool doUpdate = false;
	public Texture2D ramp;
	
	// Use this for initialization
	void Start () {
		//string[] kw = renderer.sharedMaterial.shaderKeywords;
		GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
		//renderer.sharedMaterial.shaderKeywords = kw;
		GetComponent<Renderer>().material.SetTexture("_RampTex", ramp);
		doUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (doUpdate) {
			GetComponent<Renderer>().material.SetVector("_SpherePos", transform.position);
			float minscale = Mathf.Min(transform.lossyScale.x, Mathf.Min(transform.lossyScale.y, transform.lossyScale.z));
			GetComponent<Renderer>().material.SetFloat("_Radius", minscale/2 - 2);
		}
	}
}
