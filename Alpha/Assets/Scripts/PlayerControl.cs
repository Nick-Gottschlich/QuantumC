using UnityEngine;
using System.Collections;

public enum Direction { UP, DOWN, LEFT, RIGHT };

public class PlayerControl : MonoBehaviour {
	
	// Variable to determine which player
	public int				playerNum = 1;
	
	GameObject				up;
	GameObject				down;
	GameObject				left;
	GameObject				right;
	
	public float 			smooth = 10f;
	public Pad				startPad;
	Pad						curPad;
	bool 					feedbackMovement = false;
	float					timeSinceFeedback = 0f;
	
	public PlayerControl	carrying;
	public PlayerControl	carriedBy;

	Direction				lastMoveDir;
	
	Pad 					head;
	
	float lastMove = 0f;
	Quaternion platform_last;
	
	// Use this for initialization
	void Start () {
		head = transform.FindChild("Pad").GetComponent<Pad>();

		up = transform.FindChild("Front").gameObject;
		down = transform.FindChild("Back").gameObject;
		left = transform.FindChild("Left").gameObject;
		right = transform.FindChild("Right").gameObject;

		curPad = startPad;
	}
	
	// Update is called once per frame
	void Update () {
		CheckMyPadForSomeoneElse();
	
		if (Time.time - timeSinceFeedback > .5f) {
			feedbackMovement = false;
		}

		if (Time.time - lastMove > 0.1f) {
			// Player 1
			float xdirection = Input.GetAxisRaw("Horizontal" + playerNum);
			float zdirection = Input.GetAxisRaw ("Vertical" + playerNum);
			
			float teleMod = 1f;
			// Handle Up
			if (zdirection > 0) {
				lastMove = Time.time;
				lastMoveDir = Direction.UP;
				if (curPad.teleportOnUp) {
					TeleportMovement(curPad.teleportOnUp, -teleMod, 0f);
				} else {
					StandardMovement(up);
				}
			}
			// Handle Down
			if (zdirection < 0) {
				lastMove = Time.time;
				lastMoveDir = Direction.DOWN;
				if (curPad.teleportOnDown) {
					TeleportMovement(curPad.teleportOnDown, teleMod, 0f);
				} else {
					StandardMovement(down);
				}
			}
			// Handle Left
			if (xdirection < 0) {
				lastMove = Time.time;
				lastMoveDir = Direction.LEFT;
				if (curPad.teleportOnLeft) {
					TeleportMovement(curPad.teleportOnLeft, 0f, -teleMod);
				} else {
					StandardMovement(left);
				}
			}
			// Handle Right
			if (xdirection > 0) {
				lastMove = Time.time;
				lastMoveDir = Direction.RIGHT;
				if (curPad.teleportOnRight) {
					TeleportMovement(curPad.teleportOnRight, 0f, teleMod);
				} else {
					StandardMovement(right);
				}
			}
		}
		
		Vector3 newPos;
		newPos = new Vector3 (curPad.transform.position.x, curPad.transform.position.y + 0.5f, curPad.transform.position.z);

		transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * smooth);
	}

	void CheckMyPadForSomeoneElse() {
		if (curPad.IsEmpty()) return;
		if (Time.time - lastMove < 0.1f) return;
		PlayerControl p = curPad.heldObject.GetComponent<PlayerControl>();
		if (p != this) {
			moveBack ();
		}
	}
	
	void StandardMovement(GameObject searchPos) {
		Pad pad = null;

		Vector3 startPos = searchPos.transform.position;
		Collider[] hits = Physics.OverlapSphere(startPos, 0.4f);
		if (hits.Length > 0) {
			foreach (Collider c in hits) {
				if (c.CompareTag("Pad")) {
					pad = c.GetComponentInParent<Pad>();
				}
			}
		} else {
			RaycastHit hitPad;
			if (Physics.Raycast(searchPos.transform.position, Vector3.up, out hitPad, 1f)) {
				if (hitPad.collider.GetComponentInParent<Pad>().isSlope || carriedBy) {
					pad = hitPad.collider.GetComponentInParent<Pad>();
				}
			} else if (Physics.Raycast(searchPos.transform.position, Vector3.down, out hitPad, 1f)) {
				if (hitPad.collider.GetComponentInParent<Pad>() || carriedBy) {
					pad = hitPad.collider.GetComponentInParent<Pad>();
				}
			}
		}

		if (pad) {
			if(pad.IsEmpty()){
				MoveToPad(pad);
			} else if (pad.heldObject.CompareTag("Player")) {
				if (pad.heldObject.GetComponent<PlayerControl>().playerNum != playerNum && !carrying) {
					Mount(pad.heldObject.GetComponent<PlayerControl>());
				}
			} else if (pad.heldObject.CompareTag("MoveableBlock")) {
				Push(pad);
			}
		} 
		else if (Time.timeScale != 0) {
			//give some feedback to player to show they can't move that direction
			if (searchPos == up && !feedbackMovement) {
				//slight movement in positive x direction
				Vector3 hold = transform.position;
				hold.x += .75f;
				transform.position = Vector3.Lerp (transform.position, hold, .8f);
				feedbackMovement = true;
				timeSinceFeedback = Time.time;
			}
			if (searchPos == down && !feedbackMovement) {
				//slight movement in negative x direction
				Vector3 hold = transform.position;
				hold.x -= .75f;
				transform.position = Vector3.Lerp (transform.position, hold, .8f);
				feedbackMovement = true;
				timeSinceFeedback = Time.time;
			}
			if (searchPos == left && !feedbackMovement) {
				//slight movement in positive z direction
				Vector3 hold = transform.position;
				hold.z += .75f;
				transform.position = Vector3.Lerp (transform.position, hold, .8f);
				feedbackMovement = true;
				timeSinceFeedback = Time.time;
			}
			if (searchPos == right && !feedbackMovement) {
				//slight movement in negative z direction
				Vector3 hold = transform.position;
				hold.z -= .75f;
				transform.position = Vector3.Lerp (transform.position, hold, .8f);
				feedbackMovement = true;
				timeSinceFeedback = Time.time;
			}
		}
	}
	
	void TeleportMovement(Pad teleportPad, float xMod, float zMod) {
		// Fake a transition to the pad
		Vector3 newPos = teleportPad.transform.position;
		newPos.x += xMod;
		newPos.z += zMod;
		newPos.y += 0.5f;
		transform.position = newPos;
	
		// Do the standard pad movement stuff
		if(teleportPad.IsEmpty()){
			MoveToPad(teleportPad);
		} else if (teleportPad.heldObject.CompareTag("Player")) {
			if (teleportPad.heldObject.GetComponent<PlayerControl>().playerNum != playerNum && !carrying) {
				Mount(teleportPad.heldObject.GetComponent<PlayerControl>());
			}
		} else if (teleportPad.heldObject.CompareTag("MoveableBlock")) {
			Push(teleportPad);
		}
	}
	
	void MoveToPad (Pad pad) {
		curPad = pad;
		if (carriedBy) {
			carriedBy.carrying = null;
			carriedBy = null;
		}
	}
	
	void Mount(PlayerControl player) {
		player.carrying = this;
		carriedBy = player;
		curPad = player.head;
	}
	
	void Push (Pad pad) {
		MoveableBlock block = pad.heldObject.GetComponent<MoveableBlock>();
		if (block.Push(lastMoveDir)) {
			print ("pushed");
			MoveToPad(pad);
		} else {
			print ("could not push");
		}
	}

	//minor bug with this: if player keeps holding a direction while transporting back if the player moves over a pad
	//	on the way back they might move as if they were on this pad
	//killed should freeze game, show big text saying "YOU DIED", and press R to restart
	public void Killed() {
		//Application.LoadLevel(Application.loadedLevel);
		
		GameRunner.killedByEnemy();
	}
	
	//change this to lastpad instead of lastmovedir
	public void moveBack() {
		float teleMod = 1f;
		if (lastMoveDir == Direction.DOWN) {
			lastMove = Time.time;
			lastMoveDir = Direction.UP;
			if (curPad.teleportOnUp) {
				TeleportMovement(curPad.teleportOnUp, -teleMod, 0f);
			} else {
				StandardMovement(up);
			}
		}
		if (lastMoveDir == Direction.UP) {
			lastMove = Time.time;
			lastMoveDir = Direction.DOWN;
			if (curPad.teleportOnDown) {
				TeleportMovement(curPad.teleportOnDown, teleMod, 0f);
			} else {
				StandardMovement(down);
			}
		}
		if (lastMoveDir == Direction.RIGHT) {
			lastMove = Time.time;
			lastMoveDir = Direction.LEFT;
			if (curPad.teleportOnLeft) {
				TeleportMovement(curPad.teleportOnLeft, 0f, -teleMod);
			} else {
				StandardMovement(left);
			}
		}
		if (lastMoveDir == Direction.LEFT) {
			lastMove = Time.time;
			lastMoveDir = Direction.RIGHT;
			if (curPad.teleportOnRight) {
				TeleportMovement(curPad.teleportOnRight, 0f, teleMod);
			} else {
				StandardMovement(right);
			}
		}
	}
}
