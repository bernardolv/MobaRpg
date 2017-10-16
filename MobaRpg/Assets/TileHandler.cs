using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour {
	public bool isTaken;
	Vector3 pos;
	// Use this for initialization
	void Start () {
		isTaken = false;
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		isTaken = true;
//		Debug.Log ("istaken");
//		Debug.Log (pos);
	}
	void OnTriggerExit(Collider other){
		isTaken = false;
		//Debug.Log ("ain't taken");
	}
}
