using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

	public float maxhp;
	public float curhp;
	// Use this for initialization
	void Start () {	
	
	}
	
	// Update is called once per frame
	void Update () {
		if (curhp <= 0) {
			curhp = 0;
		}
	}
}
