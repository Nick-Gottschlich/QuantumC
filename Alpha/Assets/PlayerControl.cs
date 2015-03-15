using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Variable to determine which player
	public bool 		player1or2;

	public GameObject	up;
	public GameObject	down;
	public GameObject	left;
	public GameObject	right;

	public float 		smooth = 10f;
	public Pad			curPad;
	float 				journeyDistance = 0f;
	bool 				distanceSet = false;

	float lastMove = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastMove > 0.1f) {
			// Player 1
			if (!player1or2) {
				float xdirection1 = Input.GetAxisRaw("Horizontal1");
				float zdirection1 = Input.GetAxisRaw ("Vertical1");

				GameObject searchPos = null;

				if (zdirection1 > 0) {
					searchPos = up;
				}
				if (zdirection1 < 0) {
					searchPos = down;
				}
				if (xdirection1 < 0) {
					searchPos = left;
				}
				if (xdirection1 > 0) {
					searchPos = right;
				}

				if (searchPos) {
					lastMove = Time.time;
					Collider[] hits = Physics.OverlapSphere(searchPos.transform.position, 0.5f);
					if (hits.Length > 0) {
						Pad[] pads = hits[0].GetComponentsInParent<Pad>();
						curPad = pads[0];
						distanceSet = false;
					}
				}
			} 

			// Player 2
			else {
				float xdirection2 = Input.GetAxisRaw("Horizontal2");
				float zdirection2 = Input.GetAxisRaw ("Vertical2");

			}
		}

		Vector3 newPos = new Vector3(curPad.transform.position.x, gameObject.transform.position.y, curPad.transform.position.z);
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
		}

		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		//Gizmos.DrawSphere(transform.position, 1.5f);
	}
}
