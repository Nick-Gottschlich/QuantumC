using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Variable to determine which player
	public bool 		player1or2;

	public float 		speed;
	public Vector3		vel;

	// Use this for initialization
	void Start () {
		vel = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float xdirection1 = Input.GetAxisRaw("Horizontal1");
		float xdirection2 = Input.GetAxisRaw ("Horizontal2");
		float ydirection1 = Input.GetAxisRaw ("Vertical1");
		float ydirection2 = Input.GetAxisRaw ("Vertical2");

		// Player 1
		if (!player1or2) {
			vel.x = xdirection * speed;
			vel.y = ydirection1 * speed;
		} 
		// Player 2
		else {
		}


		rigidbody.velocity = vel;

	}
}
