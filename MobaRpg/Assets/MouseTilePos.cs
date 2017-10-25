using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTilePos : MonoBehaviour {
	public static float mouseTilePosX;
	public static float mouseTilePosY;
	public static GameObject grassTile;
	Vector3 rayorigin;
	Vector3 rayend;
	//public LayerMask layerMask;

	void Start () {
	}
	
	// Update is called once per frame
	void Update() {
		//SetOriginAndEnd ();
		RaycastHit hitPoint;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
	//	int skip1n2 = ~((1<<8)|(1<<9));
		if (Physics.Raycast (ray, out hitPoint, Mathf.Infinity)) {
			if (hitPoint.collider.tag == "Ground") {
				//Debug.Log ("Hit ground"); 
				//Debug.Log("SI");
				grassTile = hitPoint.transform.gameObject;
				mouseTilePosX = grassTile.transform.position.x;
				mouseTilePosY = grassTile.transform.position.x;
			}
		}
	}
	/*void SetOriginAndEnd(){
		rayorigin.Set (MouseTilePos.mouseTilePosX, MouseTilePos.mouseTilePosY, 10);
		rayend.Set (MouseTilePos.mouseTilePosX, MouseTilePos.mouseTilePosY, -1);
	}*/
}
