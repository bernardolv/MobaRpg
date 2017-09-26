using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpells1 : MonoBehaviour {
	Vector3 pos;
	public float speed;
	private Animator anim;
	public float explosionTime;
	public float wingsTime;
	public GameObject Player;
	public GameObject dummyObject;
	Vector3 rot;
	public bool ishorizontal;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Player = this.gameObject;

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void FireBeam3by3 () {
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
		/*if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleright")) {           //Dos de adelante
			GameObject cube = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
			//cube.AddComponent<Rigidbody>();
			cube.transform.position = new Vector3 (pos.x + 1, pos.y, 0);
			GameObject cube2 = (GameObject)Instantiate(Resources.Load("Fire Prefab"));
			cube2.transform.position = new Vector3 (pos.x + 2, pos.y, 0);
			Destroy (cube, explosionTime);
			Destroy (cube2, explosionTime);
		}*/
	}
	/*public void FireWings (){
		dummyObject = new GameObject("Dummy Object");
		rot = dummyObject.transform.rotation.eulerAngles;
		for (int y = 1; y < 4; y++) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleright") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleleft")) {
				pos = transform.position;
				GameObject cubeup = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cubeup.transform.position = new Vector3 (pos.x, pos.y + y, 0);
				GameObject cubedown = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cubedown.transform.position = new Vector3 (pos.x, pos.y - y, 0);
				Destroy (cubeup, wingsTime);
				Destroy (cubedown, wingsTime);
				Debug.Log ("FYWAINGS" + pos);
				cubeup.transform.parent = dummyObject.transform;
				cubedown.transform.parent = dummyObject.transform;
				ishorizontal = false;
			}
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridleup") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Playeridledown")) {
				pos = transform.position;
				GameObject cubeup = GameObject.CreatePrimitive (PrimitiveType.Cube);
				//cube.AddComponent<Rigidbody>();
				cubeup.transform.position = new Vector3 (pos.x + y, pos.y, 0);
				GameObject cubedown = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cubedown.transform.position = new Vector3 (pos.x - y, pos.y, 0);
				Destroy (cubeup, wingsTime);
				Destroy (cubedown, wingsTime);
				Debug.Log ("FYWAINGS" + pos);
				cubeup.transform.parent = dummyObject.transform;
				cubedown.transform.parent = dummyObject.transform;
				ishorizontal = true;
			}
		}
		if (Input.GetKey(KeyCode.A)){
			rot.z = 90;
			dummyObject.transform.rotation = Quaternion.Euler(rot);
			Debug.Log ("SPINTHATHIT");
		} 
	}*/
	public void FireDash(){
	}
	public void drawcircle(int x0, int y0, int radius){
		int x = radius-1;
		int y = 0;
		int dx = 1;
		int dy = 1;
		int err = dx - (radius << 1);

		while (x >= y)
		{
			GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube1.transform.position = new Vector3(x0 + x, y0 + y, 0);
			GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube2.transform.position = new Vector3(x0 + y, y0 + x, 0);
			GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube3.transform.position = new Vector3(x0 - y, y0 + x, 0);
			GameObject cube4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube4.transform.position = new Vector3(x0 - x, y0 + y, 0);
			GameObject cube5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube5.transform.position = new Vector3(x0 - x, y0 - y, 0);
			GameObject cube6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube6.transform.position = new Vector3(x0 - y, y0 - x, 0);
			GameObject cube7 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube7.transform.position = new Vector3(x0 + y, y0 - x, 0);
			GameObject cube8 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube8.transform.position = new Vector3(x0 + x, y0 - y, 0);

			if (err <= 0)
			{
				y++;
				err += dy;
				dy += 2;
			}
			if (err > 0)
			{
				x--;
				dx += 2;
				err += (-radius << 1) + dx;
			}
		}
	}
	public void DrawFilledCircle(int x0, int y0, int radius)
	{
		int x = radius;
		int y = 0;
		int xChange = 1 - (radius << 1);
		int yChange = 0;
		int radiusError = 0;

		while (x >= y)
		{
			for (int i = x0 - x; i <= x0 + x; i++)
			{
				GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube1.transform.position = new Vector3(i, y0 + y);
				GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube2.transform.position = new Vector3(i, y0 - y);
			}
			for (int i = x0 - y; i <= x0 + y; i++)
			{
				GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube1.transform.position = new Vector3(i, y0 + x);
				GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube2.transform.position = new Vector3(i, y0 - x);
			}

			y++;
			radiusError += yChange;
			yChange += 2;
			if (((radiusError << 1) + xChange) > 0)
			{
				x--;
				radiusError += xChange;
				xChange += 2;
			}
		}
	}
}
