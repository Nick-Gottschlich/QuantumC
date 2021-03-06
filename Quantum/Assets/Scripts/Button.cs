﻿using UnityEngine;
using System.Collections;

public enum MoveOrRotate {Move, Rotate, Once, All};

public class Button : MonoBehaviour {

	public LinearPlatform controlObject;
	public MoveOrRotate moveOrRotate = MoveOrRotate.Rotate;
	public Vector3 moveOnceLoc;
	
	Vector3 holdMov;
	Vector3 holdRot;
	bool pressed = false;
	bool standingOn = false;
	bool curMoving = false;
	bool curRotating = false;
	Transform butt;
	
	private AudioSource source;
	public AudioClip buttonOn;
	public AudioClip buttonOff;

	// For Debugging purposes
	void OnGizmosDraw() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, transform.position + .3f * Vector3.up);

		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, controlObject.transform.position);
	}

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	
		holdMov = controlObject.transform.position;
		holdRot = controlObject.transform.eulerAngles;

		butt = transform.FindChild("Butt");
	}
	
	// Update is called once per frame
	void Update () {
		ButtonMod();

		//print (hold + " " + controlObject.transform.position.x);
		//the object is not in the same position as the last frame, so it's currently moving
		if (controlObject.transform.position != holdMov) {
			curMoving = true;
		}
		else {
			curMoving = false;
		}
		//the object is not in the same rotation as the last frame, so it's currently rotating
		if (controlObject.transform.eulerAngles != holdRot) {
			curRotating = true;
		}
		else {
			curRotating = false;
		}
		// the instant the button is pressed
		int layerMask = 1 << LayerMask.NameToLayer ("PlayerLayer");
		Vector3 rayCenter = transform.position;
		rayCenter.y -= 0.5f;
		if (Physics.Raycast (rayCenter, Vector3.up, .8f, layerMask)) {
			standingOn = true;
			if (!pressed && !curMoving && !curRotating) {
				if (pressed == false){
					source.PlayOneShot(buttonOn);
				}
				pressed = true;
				if (moveOrRotate == MoveOrRotate.Move)
					controlObject.Move ();
				else if (moveOrRotate == MoveOrRotate.Rotate)
					controlObject.Rotate();
				else if (moveOrRotate == MoveOrRotate.Once) {
					controlObject.MoveOnce(moveOnceLoc);
					Destroy (transform.FindChild("Butt").gameObject);
					Destroy (gameObject.GetComponent<Button>());
				}
				else { //All
					controlObject.Move ();
					controlObject.Rotate ();
				}
			}
		} else if(!Physics.Raycast (rayCenter, Vector3.up, .8f, layerMask)) {
			standingOn = false;
			pressed = false;
			if (pressed == true){
				source.PlayOneShot(buttonOff);
			}
		}
		
		holdMov = controlObject.transform.position;
		holdRot = controlObject.transform.eulerAngles;

	}

	void ButtonMod() {
		Vector3 newPos = butt.localPosition;
		if (standingOn) {
			newPos.y = -0.1f;
		} else {
			newPos.y = 0.1f;
		}
		butt.localPosition = newPos;
	}
}
