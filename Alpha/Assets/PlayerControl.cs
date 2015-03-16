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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
				if (carriedBy) startPos.y -= 0.6f;
				Collider[] hits = Physics.OverlapSphere(startPos, 0.5f);
				if (hits.Length > 0) {
					Pad pad = null;
					bool hasPlayer = false;
					print (hits.Length);
					foreach (Collider c in hits) {
						print (c.tag);
						if (c.CompareTag("Player")) {
							Mount(c);
							hasPlayer = true;
						} else if (c.CompareTag("Pad")) {
							pad = c.GetComponentInParent<Pad>();
							print (pad);
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
				}
			}
		}

		Vector3 newPos = new Vector3(curPad.transform.position.x, curPad.transform.position.y + 0.6f, curPad.transform.position.z);
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
		}

		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
	}

	void Mount(Collider c) {
		PlayerControl player = c.GetComponent<PlayerControl>();
		player.carrying = this;
		carriedBy = player;
		curPad = player.head;
	}
}
