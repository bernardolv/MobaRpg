using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	Vector3 pos;
	public float speed;
	private Animator anim;

	// Use this for initialization
	void Start () {
		//int animvalue;
		pos = transform.position;
		anim = GetComponent<Animator> ();
		//animvalue = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.A) && transform.position == pos) {        // Left
			pos += Vector3.left;
			anim.Play("Walkingleft");
		}
		if(Input.GetKey(KeyCode.D) && transform.position == pos) {        // Right
			pos += Vector3.right;
			anim.Play ("Walkingright");
		}
		if(Input.GetKey(KeyCode.W) && transform.position == pos) {        // Up
			pos += Vector3.up;
			anim.Play ("Walkingup");
		}
		if(Input.GetKey (KeyCode.S) && transform.position == pos) {        // Down
			pos += Vector3.down;
			anim.Play ("Walkingdown");
		} 
			
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed); 

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
	}
}
