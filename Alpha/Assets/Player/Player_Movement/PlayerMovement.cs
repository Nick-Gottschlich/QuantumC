using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public bool play = false;
	Vector3 starting_location;
	float speed = 3f;

	Vector3 move_direction;

	Vector3 left_increment;
	Vector3 right_increment;
	Vector3 up_increment;
	Vector3 down_increment;

	Vector3 gravity;

	GameObject path_end1;

	// Use this for initialization
	void Start () {
		starting_location = transform.position;

		ChangePlane (0);
		path_end1 = GameObject.Find ("Path_End");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P)){
			play = true;
		}
		else if(Input.GetKeyUp(KeyCode.R)){ // reset the position of the player
			play = false;
			transform.position = starting_location;
			rigidbody.velocity = Vector3.zero;
		}
	}

	float CalcDistance(float x1, float x2, float y1, float y2){
		return Mathf.Sqrt (Mathf.Pow((x1 - x2),2) + Mathf.Pow((y1 - y2),2));
	}

	void FixedUpdate(){
		if(play){

			if(CalcDistance(transform.position.x, path_end1.transform.position.x, transform.position.y, path_end1.transform.position.y) <= .2f){
				print("Distance: " + CalcDistance(transform.position.x, path_end1.transform.position.x, transform.position.y, path_end1.transform.position.y));
				play = false;
				rigidbody.velocity = Vector3.zero;
				return;
			}

//			rigidbody.MovePosition(rigidbody.position + move_direction * speed * Time.fixedDeltaTime);


			
			rigidbody.MovePosition(rigidbody.position + move_direction);
		}

		//Apply gravity
		rigidbody.AddForce (gravity);
	}

	void OnCollisionEnter(Collision coll){
//		print ("Collision with: " + coll.collider.tag);

		if(coll.collider.tag == "Move_Left"){
			print ("Collision with: " + coll.collider.tag);
//			move_direction = -1*transform.right;
			move_direction = left_increment;
		}
		else if(coll.collider.tag == "Move_Right"){
			print ("Collision with: " + coll.collider.tag);
//			move_direction = transform.right;
			move_direction = right_increment;
		}
		else if(coll.collider.tag == "Move_Down"){
			print ("Collision with: " + coll.collider.tag);
//			move_direction = -1*transform.up;
			move_direction = down_increment;
		}
		else if(coll.collider.tag == "Move_Up"){
			print ("Collision with: " + coll.collider.tag);
//			move_direction = transform.up;
			move_direction = up_increment;
		}
		else if(coll.collider.tag == "Change_Plane_Y+"){
			print ("Collision with: " + coll.collider.tag);
			ChangePlane(1);
		}
		else if(coll.collider.tag == "Change_Plane_Y-"){
			print ("Collision with: " + coll.collider.tag);
			ChangePlane(0);
		}
		else if(coll.collider.tag == "Change_Plane_X+"){
			print ("Collision with: " + coll.collider.tag);
			ChangePlane(2);
		}
		else if(coll.collider.tag == "Change_Plane_X-"){
			print ("Collision with: " + coll.collider.tag);
			ChangePlane(3);
		}
		else if(coll.collider.tag == "Change_Plane_Z+"){
			print ("Collision with: " + coll.collider.tag);
			ChangePlane(4);
		}
		else if(coll.collider.tag == "Change_Plane_Z-"){
			print ("Collision with: " + coll.collider.tag);
			ChangePlane(5);
		}

	}

	void ChangePlane(int plane){
		rigidbody.velocity = Vector3.zero;
		move_direction = Vector3.zero;

		if(plane == 0){ // "normal" plane of movement, facing -y
			left_increment = new Vector3(-speed * Time.fixedDeltaTime, 0, 0);
			down_increment = new Vector3(0, 0, -speed * Time.fixedDeltaTime);
			right_increment = -1*left_increment;
			up_increment = -1*down_increment;
			gravity = Physics.gravity;
		}
		else if(plane == 1){ // up side down plane of movement, facing +y
			left_increment = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
			down_increment = new Vector3(0, 0, speed * Time.fixedDeltaTime);
			right_increment = -1*left_increment;
			up_increment = -1*down_increment;
			gravity = -1*Physics.gravity;
		}
		else if(plane == 2){ // vertical plane facing +x direction
			left_increment = new Vector3(0,0, speed * Time.fixedDeltaTime);
			down_increment = new Vector3(0, -speed * Time.fixedDeltaTime, 0);
			right_increment = -1*left_increment;
			up_increment = -1*down_increment;
			gravity = new Vector3(-1*Physics.gravity.y, 0, 0);
		}
		else if(plane == 3){ // vertical plane facing -x direction
			left_increment = new Vector3(0,0, -speed * Time.fixedDeltaTime);
			down_increment = new Vector3(0, -speed * Time.fixedDeltaTime, 0);
			right_increment = -1*left_increment;
			up_increment = -1*down_increment;
			gravity = new Vector3(Physics.gravity.y, 0, 0);
		}
		else if(plane == 4){ // vertical plane facing +z direction
			left_increment = new Vector3(-speed * Time.fixedDeltaTime, 0, 0);
			down_increment = new Vector3(0, -speed * Time.fixedDeltaTime, 0);
			right_increment = -1*left_increment;
			up_increment = -1*down_increment;
			gravity = new Vector3(0, 0, -1*Physics.gravity.y);
		}
		else if(plane == 5){ // vertical plane facing -z direction
			left_increment = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
			down_increment = new Vector3(0, -speed * Time.fixedDeltaTime, 0);
			right_increment = -1*left_increment;
			up_increment = -1*down_increment;
			gravity = new Vector3(0, 0, Physics.gravity.y);
		}
	}
}
