using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	Vector3 pos;
	public float speed;
	private Animator anim;
	public GameObject selectedenemy;	
	public EnemyHealth enemyhealthscript;
	// Use this for initialization
	void Start () {
		//int animvalue;
		pos = transform.position;
		anim = GetComponent<Animator> ();
		speed = 4;
		//animvalue = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		
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
		if(Input.GetKey(KeyCode.E) && transform.position == pos) {        // Left
			pos += Vector3.right;
			pos += Vector3.up;
			anim.Play("Walkingright");
		}
		if(Input.GetKey(KeyCode.Z) && transform.position == pos) {        // Left
			pos += Vector3.left;
			pos += Vector3.down;
			anim.Play("Walkingleft");
		}
		if(Input.GetKey(KeyCode.C) && transform.position == pos) {        // Left
			pos += Vector3.right;
			pos += Vector3.down;
			anim.Play("Walkingright");
		}
			
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed); 

		//Animation Transitions
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingright") && transform.position == pos)
		{
			anim.Play ("Playeridleright");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingleft") && transform.position == pos)
		{
			anim.Play ("Playeridleleft");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingup") && transform.position == pos)
		{
			anim.Play ("Playeridleup");
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walkingdown") && transform.position == pos)
		{
			anim.Play ("Playeridledown");
		}
		//ATTACK SELECTION
		if (Input.GetMouseButtonDown (1)) {
			RayCaster ();
			//BasicAttack ();
		}
		
	}
	void RayCaster (){
		Debug.Log ("Pressed right click.");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit,10)){
			if (hit.transform.tag == "Enemy") {
				selectedenemy = hit.transform.gameObject;
				Debug.Log ("You got an enemy biaaaatch and his name is " + selectedenemy.ToString()	);
				enemyhealthscript = selectedenemy.transform.gameObject.transform.GetComponentInChildren<EnemyHealth> ();
				enemyhealthscript.ReceiveDamage (1);
			}
		}
	}
}
