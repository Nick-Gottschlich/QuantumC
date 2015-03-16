using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	// Variable to determine which player
	//public bool 		player1or2;
	
	enum Direction {updown, leftright};

	public GameObject	up;
	public GameObject	down;
	public GameObject	left;
	public GameObject	right;

	public float 		smooth = 10f;
	public Pad			curPad;
	
	float 				journeyDistance = 0f;
	float 				xdirection1;
	float 				zdirection1;
	bool 				distanceSet = false;
	GameObject			searchPos;
	Direction 			dir = Direction.updown;
	int					i = 0;

	//Pad					curPadStore;

	float lastMove = 0f;

	// Use this for initialization
	void Start () {
		xdirection1 = 0;
		zdirection1 = -1;
		searchPos = down;
	}
	
	// Update is called once per frame
	void Update () {
		xdirection1 = 0;
		zdirection1 = -1;
		searchPos = down;
		if (Time.time - lastMove > 0.1f) {
			/*print(i);
			if (i >= 4) {
				i = 0;
			}*/
			if (searchPos) {
				lastMove = Time.time;
				Collider[] hits = Physics.OverlapSphere(searchPos.transform.position, 0.5f);
				if (hits.Length > 0) {
					Pad pad = hits[0].GetComponentInParent<Pad>();
					curPad = pad;
					distanceSet = false;
					//break;
				} 
				/*else {
					print ("asdf");
					if (i < 2) {
						if (dir == Direction.updown) {
							if (searchPos == down) {
								xdirection1 = 0;
								zdirection1 = 1;
								searchPos = up;
							}
							if (searchPos == up) {
								xdirection1 = 0;
								zdirection1 = -1;
								searchPos = down;
							}
						}
						else {
							if (searchPos == left) {
								xdirection1 = 1;
								zdirection1 = 0;
								searchPos = right;
							}
							if (searchPos == right) {
								xdirection1 = -1;
								zdirection1 = 0;
								searchPos = left;
							}
						}
					}
					else {
						if (dir == Direction.updown){
							dir = Direction.leftright;
						}
						else {
							dir = Direction.updown;
						}
						i = 0;
					}
				}*/
			}
			i++;				
		}

		Vector3 newPos = new Vector3(curPad.transform.position.x, gameObject.transform.position.y, curPad.transform.position.z);
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
		}

		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		//Gizmos.DrawSphere(transform.position, 1.5f);
	}
}
