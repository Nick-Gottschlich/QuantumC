using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

	int current_x = 0;
	int current_y = 0;

	List<Vector3> grid = new List<Vector3>();
	public static List<string> levels = new List<string>();

	int grid_max_x = 3;
	int grid_max_y = 3;

	float increment_x = 8.65f;
	float increment_y = 5.5f;

	int y_location_mult = 3;

	float smooth = 25f;
	float journeyDistance = 0f;

	Vector3 newPos;
	Vector3 start_pos;
	
	float camera_diff_y;

	float shake_start = 1.5f;

	public static int current_level_id = 0;

	// Use this for initialization
	void Start () {

		newPos = transform.position;
		start_pos = transform.position;

		GameObject cursor = GameObject.Find ("Cursor");

		//Initialize grid system
		for(int y = 0; y < grid_max_y; y++)
			for(int x = 0; x < grid_max_x; x++)
				grid.Add (new Vector3 (cursor.transform.position.x + x * increment_x, 
				                       cursor.transform.position.y - y*increment_y, -26.3f));

		//Initialize the level loading system

		//Row 1
		levels.Add ("Nick_Level_2_basictutorial"); //level (0,0)
		levels.Add ("Nick_Level_4_tutorialforboxandlasers"); //level (1,0)
		levels.Add ("Nick_Level_1_devils_tuning_fork"); //level (2,0)
		levels.Add ("Nick_Level_3_box_and_laser_level"); //level (3,0)

		//Row 2
		levels.Add ("_game_play_custom_level_Jay_2"); //level (0,1)
		levels.Add ("_game_play_custom_level_Jay_3"); //level (1,1)
		levels.Add ("Nick_Level_5_reflexes"); //level (2,1)
		levels.Add ("_game_play_custom_level_Jay_4"); //level (3,1)

		//Row 3
		levels.Add ("Pratik_Level_1"); //level (0,2)
//		levels.Add ("_Level_Selection"); //level (1,2)
//		levels.Add ("_Level_Selection"); //level (2,2)
//		levels.Add ("_Level_Selection"); //level (3,2)

		//Row 4
		

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
				newPos = grid[current_x + current_y * y_location_mult];
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
				newPos = grid[current_x + current_y * y_location_mult];
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
				newPos = grid[current_x + current_y * y_location_mult];
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
				newPos = grid[current_x + current_y * y_location_mult];
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
//		print ("load!");
		current_level_id = x + y * y_location_mult;
		Application.LoadLevel (levels [current_level_id]);
	}

	void MoveScreen(){
		GameObject camera = GameObject.Find ("Camera_GUI");

		float new_cam_y = transform.position.y - camera_diff_y;
		camera.transform.position = new Vector3(camera.transform.position.x, new_cam_y, camera.transform.position.z);
	}
}
