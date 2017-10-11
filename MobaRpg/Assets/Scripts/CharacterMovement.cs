using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	Vector3 pos;
	public float speed;
	private Animator anim;
	GameObject selectedenemy;	
	EnemyHealth enemyhealthscript;
	CharacterSpells1 spells;
	public float wingsTime;
	GameObject Player;
	GameObject dummyObject;
	Vector3 rot;
	bool ishorizontal;
	public  float AutoAttackCooldown;
	public float AutoAttackCurTime;
	bool aggro;
	EnemyAI enemyAI;
	public float attackDistance;
	float xydif;
	float targetx;
	float targety;
	float myx;
	float myy;
	float xdif;
	float ydif;
	GameObject currenttarget;

	void Start () {
		//int animvalue;
		pos = transform.position;
		//rot = transform.rotation.eulerAngles;
		anim = GetComponent<Animator> ();
		speed = 4;
		spells = GetComponent<CharacterSpells1> ();
		//animvalue = 0;
		Player = this.gameObject;
		dummyObject = new GameObject("Dummy Object");
		rot = dummyObject.transform.rotation.eulerAngles;
		dummyObject.transform.parent = Player.transform;
		dummyObject.transform.position = pos;
		aggro = false;

	
	}
	void Update () {
		Movement ();
		if (selectedenemy == null){
			aggro = false;
		}
		//ATTACK SELECTION
		if (Input.GetMouseButtonDown (1)) {
			RayCaster ();
		}
		if (selectedenemy != null){
			if (enemyhealthscript.curhp <= 0) {
				GameObject frame = GameObject.FindGameObjectWithTag ("Selected_Frame");
				Destroy (frame);	
				Debug.Log ("DESTROY");
				selectedenemy = null;
				aggro = false;
			}
			targetx = selectedenemy.transform.position.x;  //All this to measure the block and then run against attackDistance
			targety = selectedenemy.transform.position.y;
			myx = transform.position.x;
			myy = transform.position.y;
			ydif = Mathf.Abs(myy - targety);
			xdif = Mathf.Abs(myx - targetx);
			xydif = ydif+xdif;
			if (AutoAttackCurTime < AutoAttackCooldown) {
				AutoAttackCurTime += Time.deltaTime;
			} 
			else if(xydif <= attackDistance){
				enemyhealthscript.ReceiveDamage (10);
				AutoAttackCurTime = 0;
				Debug.Log ("pew");
					//Debug.Log(xydif);
			}
			else{
			}
		}
		spellhotkeys ();
	}
	void RayCaster (){
		//Debug.Log (Vector3.down);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit,10)){
			if (hit.transform.tag == "Enemy") {
				selectedenemy = hit.transform.gameObject;
				Debug.Log ("You got an enemy biaaaatch and his name is " + selectedenemy.ToString()	);
				enemyhealthscript = selectedenemy.transform.gameObject.transform.GetComponentInChildren<EnemyHealth> ();
				enemyAI = selectedenemy.transform.gameObject.transform.GetComponentInChildren<EnemyAI> ();
				if (aggro == false) {
					GameObject frame = (GameObject)Instantiate (Resources.Load ("Selected_Frame"));
					frame.transform.position = selectedenemy.transform.position;
					frame.transform.parent = selectedenemy.transform;
					enemyAI.aggro = true;
					Debug.Log ("frane");
					//selectframe = frame;
					if (enemyAI.target == null) {
						enemyAI.target = this.gameObject;
					}
					//aggro = true;
					currenttarget = selectedenemy;
				}
				if (aggro == true && selectedenemy == currenttarget ) {
					GameObject frame = GameObject.FindGameObjectWithTag ("Selected_Frame");
					Destroy (frame);	
					Debug.Log ("DESTROY");
					selectedenemy = null;
					//aggro = false;
				}
				if (aggro == false) {
					aggro = true;
				}
				else {
					aggro = false;
				}
			}
		}
	}	
	void FireWings (){
		for (int y = 1; y < 4; y++) {
			if ((anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleright") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleleft")) && transform.position == pos) {
				//Spawning vertical
				GameObject cubeup = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
				cubeup.transform.position = new Vector3 (pos.x, pos.y + y, 0);
				GameObject cubedown = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
				cubedown.transform.position = new Vector3 (pos.x, pos.y - y, 0);
				Destroy (cubeup, wingsTime);
				Destroy (cubedown, wingsTime);
				Debug.Log ("FYWAINGS" + pos);
				cubeup.transform.parent = dummyObject.transform;
				cubedown.transform.parent = dummyObject.transform;
				ishorizontal = false;
			}
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleup") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridledown")) {
				GameObject cubeup = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
				//cube.AddComponent<Rigidbody>();
				cubeup.transform.position = new Vector3 (pos.x + y, pos.y, 0);
				GameObject cubedown = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
				cubedown.transform.position = new Vector3 (pos.x - y, pos.y, 0);
				Destroy (cubeup, wingsTime);
				Destroy (cubedown, wingsTime);
				Debug.Log ("FYWAINGS" + pos);
				cubeup.transform.parent = dummyObject.transform;
				cubedown.transform.parent = dummyObject.transform;
				ishorizontal = true;
			}
		}	
	} 
	void FireDash(){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleright")) {
			pos += 2*Vector3.left;
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed*8); 
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleleft")) {
			pos += 2*Vector3.right;
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed*8); 
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleup")) {
			pos += 2*Vector3.down;
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed*8); 
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridledown")) {
			pos += 2*Vector3.up;
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed*8); 
		}
	}
	void Movement(){
		if(Input.GetKey(KeyCode.A) && transform.position == pos) {        // Left
			if(Input.GetKey(KeyCode.LeftShift)){
				anim.Play("Playeridleleft");
			}
			else{	
				pos += Vector3.left;
				anim.Play("Walkingleft");
			}

		}
		if(Input.GetKey(KeyCode.D) && transform.position == pos) {        // Right
			if (Input.GetKey (KeyCode.LeftShift)) {
				anim.Play ("Playeridleright");
			}
			else {	
				pos += Vector3.right;
				anim.Play ("Walkingright");
			}
		}
		if(Input.GetKey(KeyCode.W) && transform.position == pos) {        // Up
			if (Input.GetKey (KeyCode.LeftShift)) {
				anim.Play ("Playeridleup");
			} 
			else {	
				pos += Vector3.up;
				anim.Play ("Walkingup");
			}
		}
		if(Input.GetKey (KeyCode.S) && transform.position == pos) {        // Down
			if (Input.GetKey (KeyCode.LeftShift)) {
				anim.Play ("Playeridledown");
			} 
			else {	
				pos += Vector3.down;
				anim.Play ("Walkingdown");
			}
		} 
		if(Input.GetKey(KeyCode.Q) && transform.position == pos) {        // Left
			pos += Vector3.left;
			pos += Vector3.up;
			anim.Play("Walkingleft");
		}
		if(Input.GetKey(KeyCode.E) && transform.position == pos) {        // Right
			pos += Vector3.right;
			pos += Vector3.up;
			anim.Play("Walkingright");
		}
		if(Input.GetKey(KeyCode.Z) && transform.position == pos) {        // Left
			pos += Vector3.left;
			pos += Vector3.down;
			anim.Play("Walkingleft");
		}
		if(Input.GetKey(KeyCode.C) && transform.position == pos) {        // Right
			pos += Vector3.right;
			pos += Vector3.down;
			anim.Play("Walkingright");
		}
			
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed); 

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingright") && transform.position == pos){
			anim.Play ("Playeridleright");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingleft") && transform.position == pos){
			anim.Play ("Playeridleleft");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingup") && transform.position == pos){
			anim.Play ("Playeridleup");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingdown") && transform.position == pos){
			anim.Play ("Playeridledown");
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleright") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleleft") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Walkingright") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Walkingleft")) {
			if (ishorizontal == true) {
				rot.z = 90;
				dummyObject.transform.rotation = Quaternion.Euler (rot);
				ishorizontal = false;
			}
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridledown") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleup") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Walkingdown") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Walkingup")) {
			if (ishorizontal == false) {
				rot.z = 0;
				dummyObject.transform.rotation = Quaternion.Euler (rot);
				ishorizontal = true;
			}
		}

	}
	void DashAndMash(){
		enemyAI.Stun ();
	}
	//void 
	void spellhotkeys (){
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			spells.FireBeam3by3();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			//			spells.drawcircle (0, 0, 15);	
			FireDash();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			FireWings ();
		}		
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			//spells.DrawFilledCircle (0,0,2);
			DashAndMash ();
		}
	}
}
