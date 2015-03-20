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
	bool 					feedbackMovement = false;
	
	public PlayerControl	carrying;
	public PlayerControl	carriedBy;
	
	Pad 					head;
	
	float lastMove = 0f;
	
	public char plane = 'Y';
	
	Quaternion platform_last;
	
	// Use this for initialization
	void Start () {
		head = transform.FindChild("Pad").GetComponent<Pad>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastMove > 0.1f) {
			// Player 1
			float xdirection = Input.GetAxisRaw("Horizontal" + playerNum);
			float zdirection = Input.GetAxisRaw ("Vertical" + playerNum);
			
			float teleMod = 1f;
			// Handle Up
			if (zdirection > 0) {
				if (curPad.teleportOnUp) {
					TeleportMovement(curPad.teleportOnUp, -teleMod, 0f);
				} else {
					StandardMovement(up);
				}
			}
			// Handle Down
			if (zdirection < 0) {
				if (curPad.teleportOnDown) {
					TeleportMovement(curPad.teleportOnDown, teleMod, 0f);
				} else {
					StandardMovement(down);
				}
			}
			if (xdirection < 0) {
				if (curPad.teleportOnLeft) {
					TeleportMovement(curPad.teleportOnLeft, -teleMod, 0f);
				} else {
					StandardMovement(left);
				}
			}
			if (xdirection > 0) {
				if (curPad.teleportOnRight) {
					TeleportMovement(curPad.teleportOnRight, teleMod, 0f);
				} else {
					StandardMovement(right);
				}
			}
			
			feedbackMovement = false;
		}
		
		Vector3 newPos;
		newPos = new Vector3 (curPad.transform.position.x, curPad.transform.position.y + 0.5f, curPad.transform.position.z);
		
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
			//			print (journeyDistance);
		}
		
		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
	}
	
	void StandardMovement(GameObject searchPos) {
		lastMove = Time.time;
		Vector3 startPos = searchPos.transform.position;
		Collider[] hits = Physics.OverlapSphere(startPos, 0.4f);
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
			if (Physics.Raycast(searchPos.transform.position, Vector3.up, out hitPad, 3f)) {
				if (hitPad.collider.GetComponentInParent<Pad>().isSlope || carriedBy)
					MoveToPad (hitPad.collider.GetComponentInParent<Pad>());
			} else if (Physics.Raycast(searchPos.transform.position, Vector3.down, out hitPad, 3f)) {
				if (hitPad.collider.GetComponentInParent<Pad>().isSlope || carriedBy)
					MoveToPad (hitPad.collider.GetComponentInParent<Pad>());
			}
			//give some feedback to player to show they can't move that direction
			if (searchPos == up && !feedbackMovement) {
				//slight movement in positive x direction
				Vector3 hold = transform.position;
				hold.x += .5f;
				transform.position = Vector3.Lerp (transform.position, hold, .75f);
				feedbackMovement = true;
			}
			if (searchPos == down && !feedbackMovement) {
				//slight movement in positive x direction
				Vector3 hold = transform.position;
				hold.x -= .5f;
				transform.position = Vector3.Lerp (transform.position, hold, .75f);
				feedbackMovement = true;
			}
			if (searchPos == left && !feedbackMovement) {
				//slight movement in positive x direction
				Vector3 hold = transform.position;
				hold.z += .75f;
				transform.position = Vector3.Lerp (transform.position, hold, .75f);
				feedbackMovement = true;
			}
			if (searchPos == right && !feedbackMovement) {
				//slight movement in positive x direction
				Vector3 hold = transform.position;
				hold.z -= .75f;
				transform.position = Vector3.Lerp (transform.position, hold, .75f);
				feedbackMovement = true;
			}
			
		}
	}
	
	void TeleportMovement(Pad teleportPad, float xMod, float zMod) {
		curPad = teleportPad;
		Vector3 newPos = curPad.transform.position;
		if (plane == 'X') {
			newPos = new Vector3 (curPad.transform.position.x + 0.5f, curPad.transform.position.y, curPad.transform.position.z);
		} else if (plane == 'Y') {
			newPos = new Vector3 (curPad.transform.position.x, curPad.transform.position.y + 0.5f, curPad.transform.position.z);
		} else{
			newPos = new Vector3 (curPad.transform.position.x, curPad.transform.position.y, curPad.transform.position.z + 0.5f);
		}
		newPos.x += xMod;
		newPos.z += zMod;
		transform.position = newPos;
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
