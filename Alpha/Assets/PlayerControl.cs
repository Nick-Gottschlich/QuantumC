using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Variable to determine which player
	public bool 		player1or2;

	public float 		speed;
	public Vector3		vel;
	public Collider		nextPad;
	public Collider		curPad;

	// Use this for initialization
	void Start () {
		vel = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 1.5f);

		// Player 1
		if (!player1or2) {
			float xdirection1 = Input.GetAxisRaw("Horizontal1");
			float zdirection1 = Input.GetAxisRaw ("Vertical1");


			// Find the current pad its on
			foreach (Collider nearcollider in hitColliders) {
				if (Mathf.Abs(nearcollider.transform.position.x - this.gameObject.transform.position.x) < .2f &&
				    Mathf.Abs(nearcollider.transform.position.z - this.gameObject.transform.position.z) < .2f &&
				    nearcollider.tag == "Pad") {
					curPad = nearcollider;
					break;
				}
			}

			//Go left
			if (curPad.GetComponent<Pad>().left && xdirection1 < 0) {
				foreach (Collider nearcollider in hitColliders) {
					if (nearcollider.transform.position.x < curPad.transform.position.x &&
					    nearcollider.transform.position.z == curPad.transform.position.z &&
					    nearcollider.tag == "Pad") {
						nextPad = nearcollider;
						break;
					}
				}
				Vector3 newPos = new Vector3(nextPad.transform.position.x, gameObject.transform.position.y, nextPad.transform.position.z);
				transform.position = Vector3.Lerp(gameObject.transform.position, newPos, .15f);
			}

			//Go right
			else if (curPad.GetComponent<Pad>().right && xdirection1 > 0) {
				foreach (Collider nearcollider in hitColliders) {
					if (nearcollider.transform.position.x > curPad.transform.position.x &&
					    nearcollider.transform.position.z == curPad.transform.position.z &&
					    nearcollider.tag == "Pad") {
						nextPad = nearcollider;
						break;
					}
				}
				Vector3 newPos = new Vector3(nextPad.transform.position.x, gameObject.transform.position.y, nextPad.transform.position.z);
				transform.position = Vector3.Lerp(gameObject.transform.position, newPos, .15f);
			}

			//Go up
			else if (curPad.GetComponent<Pad>().up && zdirection1 > 0) {
				foreach (Collider nearcollider in hitColliders) {
					if (nearcollider.transform.position.x == curPad.transform.position.x &&
					    nearcollider.transform.position.z > curPad.transform.position.z &&
					    nearcollider.tag == "Pad") {
						nextPad = nearcollider;
						break;
					}
				}
				Vector3 newPos = new Vector3(nextPad.transform.position.x, gameObject.transform.position.y, nextPad.transform.position.z);
				transform.position = Vector3.Lerp(gameObject.transform.position, newPos, .15f);

			}

			//Go down
			else if (curPad.GetComponent<Pad>().down && zdirection1 < 0) {
				foreach (Collider nearcollider in hitColliders) {
					if (nearcollider.transform.position.x == curPad.transform.position.x &&
					    nearcollider.transform.position.z < curPad.transform.position.z &&
					    nearcollider.tag == "Pad") {
						nextPad = nearcollider;
						break;
					}
				}
				Vector3 newPos = new Vector3(nextPad.transform.position.x, gameObject.transform.position.y, nextPad.transform.position.z);
				transform.position = Vector3.Lerp(gameObject.transform.position, newPos, .15f);
			}

//			vel.x = xdirection1 * speed;
//			vel.z = ydirection1 * speed;

		} 
		// Player 2
		else {
		}

		rigidbody.velocity = vel;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		//Gizmos.DrawSphere(transform.position, 1.5f);
	}
}
