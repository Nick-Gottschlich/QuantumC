using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	// Variable to determine which player
	//public bool 			player1or2;
	//public int				playerNum = 1;
	
	public GameObject		up;
	public GameObject		down;
	public GameObject		left;
	public GameObject		right;
	
	public float 			smooth = 10f;
	public Pad				curPad;
	//0 is right, 1 is left, 2 is down, 3 is up
	public int				enemyDir = 0;
	float 					journeyDistance = 0f;
	float					xdirection;
	float					zdirection;
	bool 					distanceSet = false;
	bool					enemyStopped = false;
	GameObject 				searchPos;
	int						swap = 0;
	
	Vector3 				curPos;
	
	//public PlayerControl	carrying;
	//public PlayerControl	carriedBy;
	
	//public Pad 				head;
	
	float lastMove = 0f;

	// Use this for initialization
	void Start () {
		if (enemyDir == 0) {
			xdirection = 1;
			zdirection = 0;
		
			searchPos = right;
		}
		if (enemyDir == 1) {
			xdirection = -1;
			zdirection = 0;
			
			searchPos = left;
		}
		if (enemyDir == 2) {
			xdirection = 0;
			zdirection = -1;
			
			searchPos = down;
		}
		if (enemyDir == 3) {
			xdirection = 0;
			zdirection = 1;
			
			searchPos = up;
		}
	}
	
	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player") {
			c.GetComponent<PlayerControl>().Killed();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print (swap);
		if (Time.time - lastMove > 0.1f) {
			curPos = transform.position;
			
			if (enemyStopped) {
				if (enemyDir == 0) {
					//enemy was going right, swap to left
					xdirection = -1;
					zdirection = 0;
					
					searchPos = left;
					enemyDir += 1;
					
					if (swap >= 20) {
						enemyDir = 2;
						swap = 0;
					}
				}
				else if (enemyDir == 1) {
					//enemy was going left, swap to right
					xdirection = 1;
					zdirection = 0;
					
					searchPos = right;
					enemyDir -= 1;
					
					if (swap >= 20) {
						enemyDir = 3;
						swap = 0;
					}
				}
				
				if (enemyDir == 2) {
					//enemy was going down, swap to up
					xdirection = 0;
					zdirection = 1;
					
					searchPos = up;
					enemyDir += 1;
					
					if (swap >= 20) {
						enemyDir = 0;
						swap = 0;
					}
				}
				else if (enemyDir == 3) {
					//enemy was going up, swap to down
					xdirection = 0;
					zdirection = -1;
					
					searchPos = down;
					enemyDir -= 1;
					
					if (swap >= 20) {
						enemyDir = 1;
						swap = 0;
					}
				}
				//enemyStopped = false;
			}
			
			if (searchPos) {
				lastMove = Time.time;
				Vector3 startPos = searchPos.transform.position;
				Collider[] hits = Physics.OverlapSphere(startPos, 0.5f);
				if (hits.Length > 0) {
					Pad pad = null;
					bool hasPlayer = false;
					foreach (Collider c in hits) {
						if (c.CompareTag("Pad")) {
							pad = c.GetComponentInParent<Pad>();
						}
						//this is checking if the pad is going to hit the player, we need to check the spheres colliding somehow
						/*else if (c.CompareTag("Player")) {
							c.GetComponent<PlayerControl>().Killed();
						}*/
					}
					if (!hasPlayer && pad) {
						curPad = pad;
						distanceSet = false;
					}
				}
			}
		}
		
		Vector3 newPos = new Vector3(curPad.transform.position.x, curPad.transform.position.y + 0.6f, curPad.transform.position.z);
		if (!distanceSet) {
			distanceSet = true;
			journeyDistance = Vector3.Distance(transform.position, newPos);
		}
		
		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);
		
		if (curPos == transform.position) {
			//print ("enemy stopped");
			enemyStopped = true;
			swap += 1;
		}
		else {
			enemyStopped = false;
			swap = 0;
		}

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
}