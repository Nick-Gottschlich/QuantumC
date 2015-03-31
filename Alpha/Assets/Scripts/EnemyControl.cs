using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	// Generic Variables from PlayerControl
	// TODO: Consider creating a Generic Player object
	GameObject				up;
	GameObject				down;
	GameObject				left;
	GameObject				right;
	
	public float 			smooth = 10f;
	public Pad				startPad;
	Pad						curPad;
	Direction				lastMoveDir;
	float 					lastMove = 0f;

	// Enemy Specific variables
	// 0 is right, 1 is left, 2 is down, 3 is up
	public int				enemyDir = 0;
	bool					enemyStopped = false;
	int						swap = 0;
	Vector3 				curPos;

	// Use this for initialization
	void Start () {
		up = transform.FindChild("Front").gameObject;
		down = transform.FindChild("Back").gameObject;
		left = transform.FindChild("Left").gameObject;
		right = transform.FindChild("Right").gameObject;
		
		curPad = startPad;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastMove > 0.1f) {
			curPos = transform.position;
			
			if (enemyStopped) {
				if (enemyDir == 0) {
					enemyDir += 1;
					
					if (swap >= 20) {
						enemyDir = 2;
						swap = 0;
					}
				}
				else if (enemyDir == 1) {
					//enemy was going left, swap to right
					enemyDir -= 1;
					
					if (swap >= 20) {
						enemyDir = 3;
						swap = 0;
					}
				}
				
				if (enemyDir == 2) {
					//enemy was going down, swap to up
					enemyDir += 1;
					
					if (swap >= 20) {
						enemyDir = 0;
						swap = 0;
					}
				}
				else if (enemyDir == 3) {
					//enemy was going up, swap to down
					enemyDir -= 1;
					
					if (swap >= 20) {
						enemyDir = 1;
						swap = 0;
					}
				}
				//enemyStopped = false;
			}

			float teleMod = 1f;
			// Handle Up
			if (enemyDir == 3) {
				lastMove = Time.time;
				lastMoveDir = Direction.UP;
				if (curPad.teleportOnUp) {
					TeleportMovement(curPad.teleportOnUp, -teleMod, 0f);
				} else {
					StandardMovement(up);
				}
			}
			// Handle Down
			if (enemyDir == 2) {
				lastMove = Time.time;
				lastMoveDir = Direction.DOWN;
				if (curPad.teleportOnDown) {
					TeleportMovement(curPad.teleportOnDown, teleMod, 0f);
				} else {
					StandardMovement(down);
				}
			}
			// Handle Left
			if (enemyDir == 1) {
				lastMove = Time.time;
				lastMoveDir = Direction.LEFT;
				if (curPad.teleportOnLeft) {
					TeleportMovement(curPad.teleportOnLeft, 0f, -teleMod);
				} else {
					StandardMovement(left);
				}
			}
			if (enemyDir == 0) {
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

		if (curPos == transform.position) {
			//print ("enemy stopped");
			enemyStopped = true;
			swap += 1;
		} else {
			enemyStopped = false;
			swap = 0;
		}

		HandlePlayerProximity();
	}

	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player") {
			c.GetComponent<PlayerControl>().Killed();
		}
	}

	void HandlePlayerProximity() {
		//Enemy proximity to P1 and P2 detecton
		GameObject p1 = GameObject.Find ("Player1");
		GameObject p2 = GameObject.Find ("Player2");
		float p1Distance = Vector3.Distance (transform.position, p1.transform.position);
		float p2Distance = Vector3.Distance (transform.position, p2.transform.position);
		
		//		print ("Distance 1: " + p1Distance);
		//		print ("Distance 2: " + p2Distance);
		
		if (p1Distance <= 7f || p2Distance <= 7f) {
			CameraShake.shake = .25f;
			CameraShake.shakeAmount = .1f;
		}
		else if (p1Distance <= 6f || p2Distance <= 6f) {
			CameraShake.shake = .25f;
			CameraShake.shakeAmount = .2f;
		}
		else if (p1Distance <= 5f || p2Distance <= 5f) {
			CameraShake.shake = .25f;
			CameraShake.shakeAmount = .3f;
		}
		else if (p1Distance <= 4f || p2Distance <= 4f) {
			CameraShake.shake = .25f;
			CameraShake.shakeAmount = .4f;
		}
		else if (p1Distance <= 3f || p2Distance <= 3f) {
			CameraShake.shake = .25f;
			CameraShake.shakeAmount = .5f;
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
				if (hitPad.collider.GetComponentInParent<Pad>().isSlope) {
					pad = hitPad.collider.GetComponentInParent<Pad>();
				}
			} else if (Physics.Raycast(searchPos.transform.position, Vector3.down, out hitPad, 1f)) {
				if (hitPad.collider.GetComponentInParent<Pad>()) {
					pad = hitPad.collider.GetComponentInParent<Pad>();
				}
			}
		}
		
		if (pad) {
			if(pad.IsEmpty()){
				MoveToPad(pad);
			} else if (pad.heldObject.CompareTag("Player")) {
				MoveToPad(pad);
			}
		}
	}
	
	void TeleportMovement(Pad teleportPad, float xMod, float zMod) {
		curPad = teleportPad;
		Vector3 newPos = curPad.transform.position;
		newPos.x += xMod;
		newPos.z += zMod;
		newPos.y += 0.5f;
		transform.position = newPos;
	}
	
	void MoveToPad (Pad pad) {
		curPad = pad;
	}
}