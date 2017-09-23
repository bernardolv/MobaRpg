using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Text myhp = null; 
	public float maxhp;
	public float curhp;
	public bool totemheal;
	//public SpriteRenderer rendererer;

	// Use this for initialization
	void Start () {
		myhp = GetComponentInChildren<Text> ();
		myhp.text = curhp.ToString() + "/" + maxhp.ToString();
		totemheal = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		myhp.text = curhp.ToString() + "/" + maxhp.ToString();
	}
	public void ReceiveDamage (float dmg){
		curhp = curhp- dmg;
		Debug.Log (curhp);
	}
	void OnTriggerEnter (Collider other){
		Debug.Log ("EZ");
		if (other.transform.tag == "Fire") {
			ReceiveDamage (20);
			Debug.Log ("DAMAGE");
		}
		if (other.transform.tag == "TotemHeal") {
			ReceiveDamage (-10);	
			totemheal = true;
		}
	}
}
