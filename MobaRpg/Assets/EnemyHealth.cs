using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public Text myhp = null; 
	public float maxhp;
	public float curhp;
	public SpriteRenderer rendererer;
	// Use this for initialization
	void Start () {
		maxhp = 10;
		curhp = 10;
		myhp = GetComponentInChildren<Text> ();
		myhp.text = curhp.ToString() + "/" + maxhp.ToString();
		rendererer = GetComponentInParent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		//if(Input.GetKey(KeyCode.P)){
		///	curhp = curhp - 1;
			myhp.text = curhp.ToString() + "/" + maxhp.ToString();

		//}
	}
	public void ReceiveDamage (float dmg){
		curhp = curhp- dmg;
		Debug.Log (curhp);
	}
}