using UnityEngine;
using System.Collections;

public enum Axis {x, y, z}

public class LinearPlatform : MonoBehaviour {

	bool posOrNeg = false;

	public Axis rotationAxis;
	public float rotspeed = 50f;
	float angle = 0f;

	public Axis moveAxis;
	Vector3 startPos;
	public Vector3 finalPos;
	public float movspeed = 10f;
	
	Quaternion toRot = Quaternion.identity;
	
	// Gizmos
	void OnDrawGizmos() {
//		Gizmos.color = Color.green;
//		Gizmos.DrawLine (transform.position, trigger.transform.position);
	}


	// Use this for initialization
	void Start () {
		toRot = transform.rotation;
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
		transform.rotation = DoRotation ();
		transform.position = DoMove ();
	}

	Quaternion DoRotation () {
		if (rotationAxis == Axis.x) {
			toRot = Quaternion.Euler (angle, 0f, 0f);
		} else if (rotationAxis == Axis.y) {
			toRot = Quaternion.Euler (0f, angle, 0f);
		} else if (rotationAxis == Axis.z) {
			toRot = Quaternion.Euler (0f, 0f, angle);
		}
		return Quaternion.RotateTowards(transform.rotation, toRot, Time.deltaTime * rotspeed);
	}

	public void Rotate() {
		angle += 90f;
	}

	Vector3 DoMove () {
		Vector3 dest = posOrNeg ? finalPos : startPos;
		if (Vector3.Distance (transform.position, dest) > 0.2f) {
			if (moveAxis == Axis.x && posOrNeg) {
				return transform.position + Vector3.forward * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.x && !posOrNeg) {
				return transform.position + Vector3.back * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.y && posOrNeg) {
				return transform.position + Vector3.up * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.y && !posOrNeg) {
				return transform.position + Vector3.down * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.z && posOrNeg) {
				return transform.position + Vector3.left * Time.deltaTime * movspeed;
			} else if (moveAxis == Axis.z && !posOrNeg) {
				return transform.position + Vector3.right * Time.deltaTime * movspeed;
			}
		}
		return dest;
	}

	public void Move() {
		posOrNeg = !posOrNeg;
	}
}