using UnityEngine;
using System.Collections;


public class Pad : MonoBehaviour {

	public bool 		isSlope = false;

	public Pad			teleportOnUp;
	public Pad			teleportOnDown;
	public Pad			teleportOnLeft;
	public Pad			teleportOnRight;

	public GameObject	heldObject;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int layerMask = 1 << LayerMask.NameToLayer ("PlayerLayer");
		RaycastHit hitPad;
		Vector3 rayCenter = transform.position;
		rayCenter.y -= 0.5f;
		if (Physics.Raycast(rayCenter, Vector3.up, out hitPad, 2f, layerMask)) {
			heldObject = hitPad.collider.gameObject;
		} else {
			heldObject = null;
		}
	}
	
	public bool IsEmpty() {
		return (heldObject == null);
	}
}
