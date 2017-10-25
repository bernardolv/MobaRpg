using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

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
	public float targetx;
	public float targety;
	public float myx;
	public float myy;
	float targetxy;
	float myxy;
	public float xydif;
	public float attackdistance;
	float xdif;
	float ydif;
	bool canattack;
	bool canmove;
	public float canmovecooldown;
	public float canmovecounter;
	bool canfollow;
	bool ishorizontal;
	bool inrange;
	bool inrangemove;
	bool inrangedi;
	public bool cancheck;//direciton inticator for left-right, up-down
	public bool isdead;
	public TileHandler tilescript;
	GameObject tileobject;
	bool istiletaken;
	bool canfollowretrig;

	// Use this for initialization
	void Start () {
		//myRigidbody = GetComponent<Rigidbody> ();
		moving = false;
		aggro = false;
		movedirection = transform.position;
		canfollow = true;
		canmovecooldown = 1;
		canmovecounter = 0;
		//inrange = false;
		isdead = false;
		FindTileTag ();
	}
	
	// Update is called once per frame
	void Update () { //Controls Autoattacks and Movement
		Invoke("FixCanFollow", 3);
		if (isdead == false){
			AliveBehaviour();
			//Animator state slime
		}
		if (isdead == true) {
		
		}
	}
	public void Stun(){
		Debug.Log ("Je suis stunned");
		}
	void Checkrange(){
		if (xydif <= attackdistance+1 && xdif <= 1 && ydif <= 1) {
			//Debug.Log ("enrango");
			inrange = true;
		} else {
			//Debug.Log ("sinrango");
			inrange = false;
		}
	}
	void Counters(){
		if (AutoAttackCurTime < AutoAttackCooldown) { // Attack timer
			AutoAttackCurTime += Time.deltaTime;
		} 
		if (AutoAttackCurTime >= AutoAttackCooldown) {
			canattack = true;
		}
		if (canmovecounter < canmovecooldown) { //Moving timer
			canmovecounter += Time.deltaTime;
		}
		if (canmovecounter >= canmovecooldown) {
			canmove = true;
		}
	}
	void AutofollowAI(){
		if(xydif > attackdistance) {
			if (xdif > ydif) { //for random purposes
				ishorizontal = true;
			}
			if (xdif < ydif) { // for random purposes
				ishorizontal = false;
			}
			if (ishorizontal == true && canfollow == true && moving == false) { // if horizontal is bigger difference than vertical
				if (myx < targetx) {
					movedirection += Vector3.right;
					moving = true;
					canfollow = false;
					FindTileTag ();
				}
				if (myx > targetx) {
					movedirection += Vector3.left;
					moving = true;
					canfollow = false;
					FindTileTag ();
				}
			}
			if (ishorizontal == false && canfollow == true && moving == false) { // if vertical is bigger diference
				if (myy < targety) {
					movedirection += Vector3.up;
					moving = true;
					canfollow = false;
					FindTileTag ();
				}
				if (myy > targety) {
					movedirection += Vector3.down;
					moving = true;
					canfollow = false;
					FindTileTag ();
				}
			}
			if (xdif == ydif ) { // if in perfect diagonal, it draws a random number to decide wether horizontal or vertical next move
				ishorizontal = Random.value > 0.5f;
			}
			if (moving == true) {//keep moving if not yet centered at a tile, turns shit off 
				if (istiletaken == true) {
					movedirection = transform.position;
					moving = false;
					canfollow = true;
				} 
				else {
					transform.position = Vector3.MoveTowards (transform.position, movedirection, Time.deltaTime * movespeed); 
					tilescript.isTaken = true;

				}
				if (movedirection == transform.position) {
					moving = false;
					canfollow = true;
				}
			}
		}
	}
	void AutoattackAI(){
		playerhealth = target.GetComponent<PlayerHealth> ();
		AutoAttackCurTime = 0;
		canattack = false;
		canmove = false;
		canmovecounter = 0;
		playerhealth.ReceiveDamage (10);
		//Debug.Log ("pew");
	}
	void Idlewalk(){
		if (moving == true) {//keep moving if not yet centered at a tile
			transform.position = Vector3.MoveTowards (transform.position, movedirection, Time.deltaTime * movespeed); 
			if (movedirection == transform.position) {
				moving = false;				}		
		} 
		else {
			timebetweenmovecounter -= Time.deltaTime;
			if (timebetweenmovecounter < 0f) {
				rng = (Random.Range (0, 4));
				if (rng == 0 && canmove == true) {
					//north
					movedirection += Vector3.up;
					moving = true;
					FindTileTag ();

				}
				if (rng == 1 && canmove==true) {
					//east
					movedirection += Vector3.right;
					moving = true;
					FindTileTag ();

				}
				if (rng == 2 && canmove == true) {
					//south
					movedirection += Vector3.down;
					moving = true;
					FindTileTag ();

				}
				if (rng == 3 && canmove == true) {
					//west
					movedirection += Vector3.left;
					moving = true;
					FindTileTag ();

				}
				timebetweenmovecounter = timebetweenmove;
				//movedirection = new
			}
		}
	}
	void Getxydif(){
		targetx = target.transform.position.x;
		targety = target.transform.position.y;
		myx = transform.position.x;
		myy = transform.position.y;
		ydif = Mathf.Abs(myy - targety);
		xdif = Mathf.Abs(myx - targetx);
		xydif = ydif+xdif;
	}
	void InrangeAutowalk(){
		if (moving == true) {//keep moving if not yet centered at a tile
			transform.position = Vector3.MoveTowards (transform.position, movedirection, Time.deltaTime * movespeed); 
			if (movedirection == transform.position) {
				moving = false;
			}		
		} else {

			if (canmove == true) {
				inrangemove = Random.value > 0.9955f;
			}
			if (inrangemove == true) {
				CheckHorizontal ();
				//Debug.Log (ishorizontal);
				if (xydif == 1 && ishorizontal == true) {
					inrangedi = Random.value > 0.5f;
					if (inrangedi == true) {
						movedirection += Vector3.right;
						moving = true;
						Debug.Log ("d");
						inrangemove = false;
						canmove = false;
						FindTileTag ();


					}
					if (inrangedi == false) {
						movedirection += Vector3.left;
						moving = true;
						Debug.Log ("a");
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
				}
				if (xydif == 1 && ishorizontal == false) {
					inrangedi = Random.value > 0.5f;
					if (inrangedi == true) {
						movedirection += Vector3.down;
						moving = true;
						Debug.Log ("s");
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
					if (inrangedi == false) {
						movedirection += Vector3.up;
						moving = true;
						Debug.Log ("w");
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
				}
				if (xydif == 2) { //for diagonally standing cases
					inrangedi = Random.value > 0.5f; //true is hor false is ver
					if(inrangedi == true && myx < targetx){
						Debug.Log ("caso 1");
						movedirection += Vector3.right;
						moving = true;
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
					if(inrangedi == true && myx > targetx){
							Debug.Log ("caso 2");
						movedirection += Vector3.left;
						moving = true;
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
					if(inrangedi == false && myy < targety){
						Debug.Log ("caso 3");
						movedirection += Vector3.up;
						moving = true;
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
					if(inrangedi == false && myy > targety){
						Debug.Log ("caso 3");
						movedirection += Vector3.down;
						moving = true;
						inrangemove = false;
						canmove = false;
						FindTileTag ();

					}
				}
			}
		}
	}//melee range
	void CheckHorizontal (){
		if (xdif > ydif) { //for random purposes
			ishorizontal = false;
		}
		if (xdif < ydif) { // for random purposes
			ishorizontal = true;
		}
	}
	void FindTileTag(){
		Collider[] colliders = Physics.OverlapSphere(movedirection, .1f); ///Presuming the object you are testing also has a collider 0 otherwise{
		foreach(Collider component in colliders){
			if (component.tag == "Ground") {
				//Debug.Log (component.tag);
				tileobject = component.gameObject;
				tilescript = tileobject.GetComponent<TileHandler> ();
				istiletaken = tilescript.isTaken;
				//Debug.Log (tilescript.isTaken);
			}

		}
	}
	void AliveBehaviour(){
		Counters();
		if (aggro == true) {//what to do when being targeted
			if (target != null) { //set it up
				Getxydif();
				Checkrange ();
				if(inrange == false && canmove == true) { //autofollow
					AutofollowAI();
				}
				if (inrange == true && canattack == true) { //auto attack when in range.
					AutoattackAI();
				} 
				if (inrange == true) {
					InrangeAutowalk ();
				}
			}
		}
		else {                  			//what to do when no target.
			Idlewalk();
		}
	}
	void FixCanFollow(){
		canfollow = true;
		CancelInvoke ();

	}
}
