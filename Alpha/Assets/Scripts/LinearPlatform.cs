using UnityEngine;
using System.Collections;

public enum Axis {x, y, z}

public class LinearPlatform : MonoBehaviour {

	public bool posOrNeg = false;

	public bool	infiniteSpin = false;
	public Axis rotationAxis;
	public float rotspeed = 50f;
	float angle = 0f;

	public bool infiniteMove = false;
	public Axis moveAxis;
	Vector3 startPos;
	public Vector3 finalPos;
	public float movspeed = 10f;
	
	Quaternion toRot = Quaternion.identity;
	Vector3 orig_rot;
	
	// Gizmos
	void OnDrawGizmos() {
//		Gizmos.color = Color.green;
//		Gizmos.DrawLine (transform.position, trigger.transform.position);
	}


	// Use this for initialization
	void Awake () {
		toRot = transform.rotation;
		orig_rot = transform.eulerAngles;
		if (rotationAxis == Axis.x) {
			angle = transform.eulerAngles.x;
		} else if (rotationAxis == Axis.y) {
			angle = transform.eulerAngles.y;
		} else if (rotationAxis == Axis.z) {
			angle = transform.eulerAngles.z;
		}
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (infiniteMove) {
			Vector3 dest = posOrNeg ? finalPos : startPos;
			if (transform.position == dest) {
				Move ();
			}
		}

		if (infiniteSpin) {
			if (toRot == transform.rotation) {
				Rotate ();
			}
		}

		transform.rotation = DoRotation ();
		transform.position = DoMove ();
	}

	Quaternion DoRotation () {
		if (rotationAxis == Axis.x) {
			toRot = Quaternion.Euler (angle, orig_rot.y, orig_rot.z);
		} else if (rotationAxis == Axis.y) {
			toRot = Quaternion.Euler (orig_rot.x, angle, orig_rot.z);
		} else if (rotationAxis == Axis.z) {
			toRot = Quaternion.Euler (orig_rot.x, orig_rot.y, angle);
		}
		return Quaternion.RotateTowards(transform.rotation, toRot, Time.deltaTime * rotspeed);
	}

	public void Rotate() {
		angle += 90f;
//		angle = 90f * num_rotations;
//		num_rotations++;
//		if (num_rotations == 5) {
//			transform.eulerAngles = orig_rot;
//			num_rotations = 1;
//		}
	}

	Vector3 DoMove () {

		Vector3 dest = posOrNeg ? finalPos : startPos;
		if (Vector3.Distance (transform.position, dest) > 0.2f) {
			if (moveAxis == Axis.z && posOrNeg) {
				return transform.position + Vector3.forward * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.z && !posOrNeg) {
				return transform.position + Vector3.back * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.y && posOrNeg) {
				return transform.position + Vector3.up * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.y && !posOrNeg) {
				return transform.position + Vector3.down * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.x && posOrNeg) {
				return transform.position + Vector3.left * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.x && !posOrNeg) {
				return transform.position + Vector3.right * Time.deltaTime * movspeed;
			}
		}
		return dest;
	}

	public void Move() {
		posOrNeg = !posOrNeg;
	}

	public void MoveOnce(Vector3 input) {
		startPos = transform.position;
		finalPos = input;
		posOrNeg = true;
	}
}