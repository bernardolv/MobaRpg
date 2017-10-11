using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour {

	Vector3 pos;
	public GameObject placeHolderSprite;
	public bool ischilddead;

	public GameObject monsterToSpawn;
//	public Vector3 position;
	// Use this for initialization
	void Start () {
		//position = transform.position;
		ischilddead = false;
		pos = transform.position;
		Destroy (placeHolderSprite);
		SummonAction ();
		/*
		GameObject enemy = (GameObject)Instantiate(Resources.Load ("Slime_Prefab"));
		enemy.transform.position = new Vector3 (pos.x, pos.y, 0);
		enemy.transform.SetParent (this.transform);
*/
	}
	
	// Update is called once per frame
	void Update () {
		if (ischilddead == true){
			ischilddead = false;
			Invoke ("SummonAction", 5);

		}
	}

	void SummonAction () {
		GameObject enemy = (GameObject)Instantiate (Resources.Load ("Slime_Prefab"));
		enemy.transform.position = new Vector3 (pos.x, pos.y, 0);
		enemy.transform.SetParent (this.transform);
	}
}
