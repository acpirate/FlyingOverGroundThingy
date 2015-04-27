using UnityEngine;
using System.Collections;

public class WrapProjection : MonoBehaviour {

	public enum projectionPosition {NORTH,SOUTH,EAST,WEST,NORTHEAST,NORTHWEST,SOUTHEAST,SOUTHWEST};

	void Update() {

		foreach(Vector3 location in Constants.GhostLocations(transform.position)) {

			Vector3 scale = transform.localScale;
			Vector3 pos = location;
			Quaternion rot =  transform.rotation;
			Matrix4x4 matrix = Matrix4x4.TRS(pos, rot, scale);
			
			MeshFilter meshFilter = GetComponent<MeshFilter>();
			Mesh mesh = meshFilter.mesh;
			Material mat = GetComponent<Renderer>().sharedMaterial;
			
			
			Graphics.DrawMesh(mesh,matrix,mat,0,Camera.main,0,new MaterialPropertyBlock(),true);

		}


//		DrawProjection(projectionPosition.NORTH);
//		DrawProjection(projectionPosition.SOUTH);
//		DrawProjection(projectionPosition.EAST);
//		DrawProjection(projectionPosition.WEST);
//		DrawProjection(projectionPosition.NORTHEAST);
//		DrawProjection(projectionPosition.NORTHWEST);
//		DrawProjection(projectionPosition.SOUTHEAST);
//		DrawProjection(projectionPosition.SOUTHWEST);
	}

//	void DrawProjection(projectionPosition drawPosition) {
//		float offsetX=0f;
//		float offsetZ=0f;
//
//		switch (drawPosition) {
//		case (projectionPosition.NORTH):
//			offsetX=0f;
//			offsetZ=Constants.mapRadius*2f;
//			break;
//		case (projectionPosition.SOUTH):
//			offsetX=0f;
//			offsetZ=Constants.mapRadius*-2f;
//			break;
//		case (projectionPosition.EAST):
//			offsetX=Constants.mapRadius*2f;
//			offsetZ=0f;
//			break;
//		case (projectionPosition.WEST):
//			offsetX=Constants.mapRadius*-2f;
//			offsetZ=0f;
//			break;
//		case (projectionPosition.NORTHEAST):
//			offsetX=Constants.mapRadius*2f;
//			offsetZ=Constants.mapRadius*2f;
//			break;
//		case (projectionPosition.NORTHWEST):
//			offsetX=Constants.mapRadius*-2f;
//			offsetZ=Constants.mapRadius*2f;
//			break;
//		case (projectionPosition.SOUTHEAST):
//			offsetX=Constants.mapRadius*2f;
//			offsetZ=Constants.mapRadius*-2f;
//			break;
//		case (projectionPosition.SOUTHWEST):
//			offsetX=Constants.mapRadius*-2f;
//			offsetZ=Constants.mapRadius*-2f;
//			break;
//		}
//
//
//		Vector3 scale = transform.localScale;
//		Vector3 pos = new Vector3(transform.position.x+offsetX,transform.position.y,transform.position.z+offsetZ);
//		Quaternion rot =  transform.rotation;
//		Matrix4x4 matrix = Matrix4x4.TRS(pos, rot, scale);
//		
//		MeshFilter meshFilter = GetComponent<MeshFilter>();
//		Mesh mesh = meshFilter.mesh;
//		Material mat = GetComponent<Renderer>().sharedMaterial;
//
//
//		Graphics.DrawMesh(mesh,matrix,mat,0,Camera.main,0,new MaterialPropertyBlock(),true);
//
//
//
//
//	} //DrawProjection

}
