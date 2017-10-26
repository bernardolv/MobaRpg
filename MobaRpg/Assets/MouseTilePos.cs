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
	void Update() {
		RaycastHit hitPoint;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
		if (Physics.Raycast (ray, out hitPoint, Mathf.Infinity)) {
			if (hitPoint.collider.tag == "Ground") {
				grassTile = hitPoint.transform.gameObject;
				mouseTilePosX = grassTile.transform.position.x;
				mouseTilePosY = grassTile.transform.position.x;
			}
		}
	}
}
