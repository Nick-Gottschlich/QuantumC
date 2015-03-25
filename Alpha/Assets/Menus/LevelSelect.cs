using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

	int current_x = 0;
	int current_y = 0;

	List<Vector3> grid = new List<Vector3>();
	List<string> levels = new List<string>();

	int grid_max_x = 4;
	int grid_max_y = 5;

	float increment_x = 6f;
	float increment_y = 3.9f;

	float smooth = 10f;
	float journeyDistance = 0f;

	Vector3 newPos;
	Vector3 start_pos;
	
	float camera_diff_y;

	float shake_start = 2.5f;

	// Use this for initialization
	void Start () {

		newPos = transform.position;
		start_pos = transform.position;

		//Initialize grid system
		for(int y = 0; y < grid_max_y; y++)
			for(int x = 0; x < grid_max_x; x++)
				grid.Add (new Vector3 (-27f + x * increment_x, 31.2f - y*increment_y, -26.3f));

		//Initialize the level loading system
		levels.Add ("_noah_enemy_proximity_demo"); //level (0,0)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (1,0)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (2,0)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (3,0)
//
//		levels.Add ("_noah_enemy_proximity_demo"); //level (0,1)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (1,1)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (2,1)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (3,1)
//
//		levels.Add ("_noah_enemy_proximity_demo"); //level (0,2)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (1,2)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (2,2)
//		levels.Add ("_noah_enemy_proximity_demo"); //level (3,2)
		

//		foreach (Vector3 pos in grid) {
//			print ("X: " + pos.x + " Y: " + pos.y + " Z: " + pos.z);
//			print ("Y: " + pos.y);
//			print ("Z: " + pos.z);
//		}

		GameObject camera = GameObject.Find ("Camera_GUI");
		camera_diff_y = Mathf.Abs (transform.position.y - camera.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp(KeyCode.UpArrow)){
			if(current_y > 0){
				current_y--;
				newPos = grid[current_x + current_y*4];
				journeyDistance = Vector3.Distance(transform.position, newPos);				
			}
			else{
				if(transform.position.y >= start_pos.y - shake_start)
					ShakeCursor();
			}
		}
		else if(Input.GetKeyUp(KeyCode.DownArrow)){
			if(current_y < grid_max_y - 1){
				current_y++;
				newPos = grid[current_x + current_y*4];
				journeyDistance = Vector3.Distance(transform.position, newPos);				
			}
			else{
				if(transform.position.y <= start_pos.y - increment_y  * (grid_max_y - 1) + shake_start)
					ShakeCursor();
			}
		}

		else if(Input.GetKeyUp(KeyCode.LeftArrow)){
			if(current_x > 0){
				current_x--;
				newPos = grid[current_x + current_y*4];
				journeyDistance = Vector3.Distance(transform.position, newPos);				
			}
			else{
				if(transform.position.x <= start_pos.x + shake_start)
					ShakeCursor();
			}
		}
		else if(Input.GetKeyUp(KeyCode.RightArrow)){
			if(current_x < grid_max_x - 1){
				current_x++;
				newPos = grid[current_x + current_y*4];
				journeyDistance = Vector3.Distance(transform.position, newPos);				
			}
			else{
				if(transform.position.x >= start_pos.x + increment_x  * (grid_max_x - 1) - shake_start)
					ShakeCursor();
			}
		}

		float fracJourney = Time.deltaTime * smooth / journeyDistance;
		transform.position = Vector3.Lerp (transform.position, newPos, fracJourney);

		//Have camera follow the cursor
		MoveScreen ();

		//Load the level the cursor is currently highlighting
		if (Input.GetKeyUp (KeyCode.Return))
			LoadLevel (current_x, current_y);
	}

	void ShakeCursor(){
//		print ("Shake if off");
		CameraShake.shake = .25f;
		CameraShake.shakeAmount = .1f;
	}

	void LoadLevel(int x, int y){
		print ("load!");
//		Application.LoadLevel (levels [x + y * 4]);
	}

	void MoveScreen(){
		GameObject camera = GameObject.Find ("Camera_GUI");

		float new_cam_y = transform.position.y - camera_diff_y;
		camera.transform.position = new Vector3(camera.transform.position.x, new_cam_y, camera.transform.position.z);
	}
}
