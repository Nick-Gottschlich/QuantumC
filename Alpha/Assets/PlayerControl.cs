using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Variable to determine which player
	public bool 			player1or2;
	public int				playerNum = 1;

	public GameObject		up;
	public GameObject		down;
	public GameObject		left;
	public GameObject		right;

	public float 			smooth = 10f;
	public Pad				curPad;
	float 					journeyDistance = 0f;
	bool 					distanceSet = false;

	public PlayerControl	carrying;
	public PlayerControl	carriedBy;

	public Pad 				head;

	float lastMove = 0f;

	public char plane = 'Y';

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation.z == 90 || transform.rotation.z == 270)
			plane = 'X';
		else if (transform.rotation.z == 0 && transform.rotation.x == 0)
			plane = 'Y';
		else if (transform.rotation.x == 90 || transform.rotation.x == 270)
			plane = 'Z';
		//else{
		//		add in code to keep the player fixed to the gamepad when it is on a rotating platform
		//}

		if (Time.time - lastMove > 0.1f) {
			// Player 1
			float xdirection = Input.GetAxisRaw("Horizontal" + playerNum);
			float zdirection = Input.GetAxisRaw ("Vertical" + playerNum);

			GameObject searchPos = null;

			if (zdirection > 0) {
				searchPos = up;
			}
			if (zdirection < 0) {
				searchPos = down;
			}
			if (xdirection < 0) {
				searchPos = left;
			}
			if (xdirection > 0) {
				searchPos = right;
			}

			if (searchPos) {
				lastMove = Time.time;
				Vector3 startPos = searchPos.transform.position;
				if (carriedBy) {
					startPos.y -= 0.6f;
				}
				Collider[] hits = Physics.OverlapSphere(startPos, 0.25f);
				if (hits.Length > 0) {
					Pad pad = null;
					bool hasPlayer = false;
					foreach (Collider c in hits) {
						if (c.CompareTag("Player")) {
							Mount(c);
							hasPlayer = true;
						} else if (c.CompareTag("Pad")) {
							pad = c.GetComponentInParent<Pad>();
						}
					}
					if (!hasPlayer && pad) {
						curPad = pad;
						if (carriedBy) {
							carriedBy.carrying = null;
							carriedBy = null;
						}
						distanceSet = false;
					}
				} else {
					RaycastHit hitPad;
					if (Physics.Raycast(searchPos.transform.position, Vector3.up, out hitPad, Mathf.Infinity)) {
						MoveToPad (hitPad.collider.GetComponentInParent<Pad>());
					} else if (Physics.Raycast(searchPos.transform.position, Vector3.down, out hitPad, Mathf.Infinity)) {
						MoveToPad (hitPad.collider.GetComponentInParent<Pad>());
					}

				}
			}
		}

		Vector3 newPos;

		if (plane == 'X') {
			newPos = new Vector3 (gameObject.transform.position.x, curPad.transform.position.y, curPad.transform.position.z);
		} else if (plane == 'Y') {
			newPos = new Vector3 (curPad.transform.position.x, gameObject.transform.position.y, curPad.transform.position.z);
		} else{
			newPos = new Vector3 (curPad.transform.position.x, curPad.transform.position.y, gameObject.transform.position.z);
		}
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
		}

		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
	}

	void MoveToPad (Pad pad) {
		if (pad) {
			curPad = pad;
			if (carriedBy) {
				carriedBy.carrying = null;
				carriedBy = null;
			}
			distanceSet = false;
		}
	}

	void Mount(Collider c) {
		PlayerControl player = c.GetComponent<PlayerControl>();
		player.carrying = this;
		carriedBy = player;
		curPad = player.head;
	}

	void changePlane(int p){
		if (p == 0)
			plane = 'X';
		else if (p == 1)
			plane = 'Y';
		else if (p == 2)
			plane = 'Z';
	}
}
