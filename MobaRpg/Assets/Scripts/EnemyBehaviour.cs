using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Ray ray;
		//RaycastHit hit;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			Debug.Log ("Pressed right click.");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit,10)){
				Debug.Log(hit.collider.name);
			}
		}
	}
}
