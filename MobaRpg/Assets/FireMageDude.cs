using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageDude : MonoBehaviour {
	Vector3 pos;
	Vector3 pos2;
	public float speed;
	private Animator anim;
	public float explosionTime;
	public float wingsTime;
	public GameObject Player;
	GameObject dummyObject;
	Vector3 rot;
	public bool ishorizontal;
	CharacterMovement Playermovement;
	GameObject currenttarget;
	TileHandler tilescript;
	GameObject tileobject;
	bool istiletaken;
	//public Vector3 dadpos;

	void Start () {
		anim = GetComponent<Animator> ();
		Player = this.gameObject;
		Playermovement = GetComponent<CharacterMovement> ();
		pos = transform.position;
		speed = 4;
		Player = this.gameObject;
		dummyObject = new GameObject("Dummy Object");
		rot = dummyObject.transform.rotation.eulerAngles;
		dummyObject.transform.parent = Player.transform;
		dummyObject.transform.position = pos;
		//dadpos = Playermovement.pos;
	}
	
	// Update is called once per frame
	void Update () {
		pos = Playermovement.pos;
		spellhotkeys ();
		DummyObjectRotation ();
	}
	void DummyObjectRotation(){
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
	void spellhotkeys (){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			FireBeam3by3 ();
			Debug.Log ("A");
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			FireDash ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			FireWings ();
		}		
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			//DashAndMash ();
		}
	}
	void FindTileTag(){
		Collider[] colliders = Physics.OverlapSphere(pos, .1f); ///Presuming the object you are testing also has a collider 0 otherwise{
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
	void FireBeam3by3 () {
		for (int y = 0; y < 3; y++) {
			for (int x = 0; x < 3; x++) {
				pos = transform.position;
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleright")) {
					GameObject cube = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
					//cube.AddComponent<Rigidbody>();
					cube.transform.position = new Vector3 (pos.x + x + 2, pos.y + y - 1, 0);
					Destroy (cube, explosionTime);
				}
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleleft")) {
					GameObject cube = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
					//cube.AddComponent<Rigidbody>();
					cube.transform.position = new Vector3 (pos.x + x -4, pos.y + y - 1, 0);
					Destroy (cube, explosionTime);
				}
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleup")) {
					GameObject cube = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
					//cube.AddComponent<Rigidbody>();
					cube.transform.position = new Vector3 (pos.x + x -1, pos.y + y +2, 0);
					Destroy (cube, explosionTime);
				}
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridledown")) {
					GameObject cube = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
					//cube.AddComponent<Rigidbody>();
					cube.transform.position = new Vector3 (pos.x + x -1, pos.y + y -4, 0);
					Destroy (cube, explosionTime);
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
			pos2 = pos - Vector3.left;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleleft")) {
			pos += 2*Vector3.right;
			pos2 = pos - Vector3.right;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleup")) {
			pos += 2*Vector3.down;
			pos2 = pos - Vector3.down;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridledown")) {
			pos += 2*Vector3.up;
			pos2 = pos - Vector3.up;
		}
		FindTileTag ();
		if (istiletaken == true) {
			pos = pos2;
			FindTileTag();
			if (istiletaken == true) {
				pos = transform.position;
			}
			else {
				tilescript.isTaken = true;
				transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed); 
			}
		} 
		else {
			tilescript.isTaken = true;
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed); 
		}
	}

}
