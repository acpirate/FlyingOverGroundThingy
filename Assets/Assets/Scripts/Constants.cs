using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GHOSTPOSITON {NORTH,SOUTH,EAST,WEST,NORTHEAST,NORTHWEST,SOUTHEAST,SOUTHWEST};

public static class Constants {
	public static float mapRadius=200f;
	public static float chumpVisionRadius=15f;


	public static List<Vector3> GhostLocations(Vector3 ghostOrigin) {
		List<Vector3> ghostLocationHolder=new List<Vector3>();
		//North
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x,ghostOrigin.y,ghostOrigin.z+2*Constants.mapRadius));
		//South
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x,ghostOrigin.y,ghostOrigin.z-2*Constants.mapRadius));
		//East
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x+2*Constants.mapRadius,ghostOrigin.y,ghostOrigin.z));
		//West
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x-2*Constants.mapRadius,ghostOrigin.y,ghostOrigin.z));
		//NorthEast
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x+2*Constants.mapRadius,ghostOrigin.y,ghostOrigin.z+2*Constants.mapRadius));
		//NorthWest
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x-2*Constants.mapRadius,ghostOrigin.y,ghostOrigin.z+2*Constants.mapRadius));
		//SouthEast
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x+2*Constants.mapRadius,ghostOrigin.y,ghostOrigin.z-2*Constants.mapRadius));
		//SouthWest
		ghostLocationHolder.Add(new Vector3(ghostOrigin.x-2*Constants.mapRadius,ghostOrigin.y,ghostOrigin.z-2*Constants.mapRadius));

		return ghostLocationHolder;
	}


	public static float RandomAroundRadius() {
		return Random.Range(-mapRadius,mapRadius);
	}
}
