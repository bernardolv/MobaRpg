using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public Text myhp = null; 
	public float maxhp;
	public float curhp;
	bool triggerwhendead;
	SpawnBehaviour myspawn;
	GameObject me;
	//public SpriteRenderer rendererer;

	// Use this for initialization
	void Start () {
		myhp = GetComponentInChildren<Text> ();
		myhp.text = curhp.ToString() + "/" + maxhp.ToString();
		//triggerwhendead = false;
		myspawn = GetComponentInParent<SpawnBehaviour> ();
		//curhp = 100;
		//rendererer = GetComponentInParent<SpriteRenderer> ();
		me = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		myhp.text = curhp.ToString () + "/" + maxhp.ToString ();
		if (curhp <= 0) {
			myspawn.ischilddead = true;
			Destroy (me);
		} 
	}
	public void ReceiveDamage (float dmg){
		curhp = curhp - dmg;
		//Debug.Log (curhp);
	}
	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Fire") {
			ReceiveDamage (20);
			Debug.Log ("FIRE");
		}
	}
}