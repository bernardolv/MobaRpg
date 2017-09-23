using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour {

	public float movespeed;
	//private Rigidbody myRigidbody;
	private bool moving;
	public float timebetweenmove;
	public float timebetweenmovecounter;
	public Vector3 movedirection;
	public float rng;
	public bool aggro;
	public GameObject target;
	public float AutoAttackCurTime;
	public float AutoAttackCooldown;
	public PlayerHealth playerhealth;

	// Use this for initialization
	void Start () {
		//myRigidbody = GetComponent<Rigidbody> ();
		moving = false;
		aggro = false;
		movedirection = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (aggro == true) {
			if (target != null) {
				if (AutoAttackCurTime < AutoAttackCooldown) {
					AutoAttackCurTime += Time.deltaTime;
				} else {
					playerhealth = target.GetComponent<PlayerHealth> ();
					playerhealth.ReceiveDamage (10);
					AutoAttackCurTime = 0;
				}
			}
		}
		else {
			if (moving == true) {
				transform.position = Vector3.MoveTowards (transform.position, movedirection, Time.deltaTime * movespeed); 
				if (movedirection == transform.position) {
					moving = false;
					Debug.Log ("moviendo hacia" + rng.ToString());
				}		
			} 
			else {
				timebetweenmovecounter -= Time.deltaTime;
				if (timebetweenmovecounter < 0f) {
					rng = (Random.Range (0, 4));
					Debug.Log (rng);
					if (rng == 0) {
						//north
						movedirection += Vector3.up;
						moving = true;
					}
					if (rng == 1) {
						//east
						movedirection += Vector3.right;
						moving = true;
					}
					if (rng == 2) {
						//south
						movedirection += Vector3.down;
						moving = true;
					}
					if (rng == 3) {
						//west
						movedirection += Vector3.left;
						moving = true;
					}
					timebetweenmovecounter = timebetweenmove;
					//movedirection = new
				}
			}
		}
	}
}
