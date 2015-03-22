﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	LineRenderer	line;
	Vector3			shotDir = Vector3.forward;

	Vector3			lineStart;

	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer>();
		lineStart = transform.position;
		lineStart.y += 1f;
		line.SetPosition(0, lineStart);

		if (transform.eulerAngles.y >= 80f && transform.eulerAngles.y <= 100f) {
			shotDir = Vector3.right;
		} else if (transform.eulerAngles.y >= 170f && transform.eulerAngles.y <= 190f) {
			shotDir = Vector3.back;
		} else if (transform.eulerAngles.y >= 260f && transform.eulerAngles.y <= 280f) {
			shotDir = Vector3.left;
		} else {
			shotDir = Vector3.forward;
		}
	}
	
	// Update is called once per frame
	void Update () {
		int playerLayerMask = 1 << LayerMask.NameToLayer("PlayerLayer");
		int groundLayerMask = 1 << LayerMask.NameToLayer("GroundLayer");

		RaycastHit hit;
		if (Physics.Raycast(lineStart, shotDir, out hit, Mathf.Infinity, playerLayerMask)) {
			line.SetPosition(1, lineStart + (shotDir * (Vector3.Distance(hit.transform.position, lineStart))));
			if (hit.collider.CompareTag("Player")) {
				hit.collider.GetComponent<PlayerControl>().Killed();
			}
		} else if(Physics.Raycast(lineStart, shotDir, out hit, Mathf.Infinity, groundLayerMask)) {
			line.SetPosition(1, lineStart + (shotDir * (Vector3.Distance(hit.transform.position, lineStart))));
		} else {
			line.SetPosition(1, lineStart + (shotDir * 1000f));
		}
	}
}