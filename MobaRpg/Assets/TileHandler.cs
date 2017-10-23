using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour {
	public bool isTaken;
	Vector3 pos;
	Color ogcolor;
	// Use this for initialization
	void Start () {
		isTaken = false;
		//pos = transform.position;
		ogcolor = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTaken == true) {
			GetComponent<SpriteRenderer> ().color = new Color(.2f,.2f,.2f);
		}
		if (isTaken == false) {
			GetComponent<SpriteRenderer> ().color = ogcolor;
		}
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
