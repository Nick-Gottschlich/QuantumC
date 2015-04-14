using UnityEngine;
using System.Collections;

public class MoveableBlock : MonoBehaviour {

	public Pad		curPad;
	public float	smooth = 10f;

	float			journeyDistance;
	bool 			distanceSet = false;

	GameObject		up;
	GameObject		down;
	GameObject		left;
	GameObject		right;


	// Use this for initialization
	void Start () {
		up = transform.FindChild("Up").gameObject;
		down = transform.FindChild("Down").gameObject;
		left = transform.FindChild("Left").gameObject;
		right = transform.FindChild("Right").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos;
		newPos = new Vector3 (curPad.transform.position.x, curPad.transform.position.y + 0.5f, curPad.transform.position.z);
		
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
		}
		
		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
	}

	public bool Push(Direction dir) {
		if (GameEngine.isHeavyMode) return false;
		float teleMod = 1f;
		if (dir == Direction.UP) {
			if (curPad.teleportOnUp) {
				TeleportMovement(curPad.teleportOnUp, -teleMod, 0f);
				return true;
			} else {
				return StandardMovement(up);
			}
		} else if (dir == Direction.DOWN) {
			if (curPad.teleportOnDown) {
				TeleportMovement(curPad.teleportOnDown, teleMod, 0f);
				return true;
			} else {
				return StandardMovement(down);
			}
		} else if (dir == Direction.LEFT) {
			if (curPad.teleportOnLeft) {
				TeleportMovement(curPad.teleportOnLeft, 0f, -teleMod);
				return true;
			} else {
				print ("trying to move left");
				return StandardMovement(left);
			}
		} else {
			if (curPad.teleportOnRight) {
				TeleportMovement(curPad.teleportOnRight, 0f, teleMod);
				return true;
			} else {
				return StandardMovement(right);
			}
		}
	}

	bool StandardMovement(GameObject searchPos) {
		Vector3 startPos = searchPos.transform.position;
		Collider[] hits = Physics.OverlapSphere(startPos, 0.4f);
		Pad pad = null;
		if (hits.Length > 0) {
			foreach (Collider c in hits) {
				if (c.CompareTag("Pad")) {
					pad = c.GetComponentInParent<Pad>();
				}
			}
		} else {
			RaycastHit hitPad;
			if (Physics.Raycast(searchPos.transform.position, Vector3.up, out hitPad, 3f)) {
				if (hitPad.collider.GetComponentInParent<Pad>().isSlope) {
					pad = hitPad.collider.GetComponentInParent<Pad>();
				}

			} else if (Physics.Raycast(searchPos.transform.position, Vector3.down, out hitPad, 3f)) {
				if (hitPad.collider.GetComponentInParent<Pad>().isSlope) {
					pad = hitPad.collider.GetComponentInParent<Pad>();
				}
			}
		}
		if(pad && pad.IsEmpty() && pad.boxAllowed == true){
			MoveToPad(pad);
			return true;
		}
		print ("didn't find jack");
		return false;
	}
	
	void TeleportMovement(Pad teleportPad, float xMod, float zMod) {
		Vector3 newPos = teleportPad.transform.position;
		newPos.x += xMod;
		newPos.z += zMod;
		transform.position = newPos;
		
		if (teleportPad && teleportPad.IsEmpty() && teleportPad.boxAllowed == true){
			MoveToPad(teleportPad);
		}
	}

	void MoveToPad(Pad pad) {
		curPad = pad;
		distanceSet = false;
	}
}
