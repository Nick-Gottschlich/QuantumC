﻿using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

	bool through_text = false;
	int extra_lines_of_text = 0;
	int lines_read = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "_Transition_1") {			
			if (Input.GetKeyUp (KeyCode.Return)) {
				GameRunner.loadingMessage();
				Application.LoadLevel ("Nick_Level_2_basictutorial");
			}
		}
		else if (Application.loadedLevelName == "_Transition_1_2") {			
			if (Input.GetKeyUp (KeyCode.Return)) {
				GameRunner.loadingMessage();
				Application.LoadLevel ("Nick_Level_6");
			}
		}
		else if (Application.loadedLevelName == "_Transition_2") {
			extra_lines_of_text = 2;
			
			if (Input.GetKeyUp (KeyCode.Return)){
				if(lines_read == 0){ //Disable current text, enable next text
					GameObject p1_t1 = GameObject.Find ("p1_Bubble_1");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t1.GetComponent<GUITexture>().enabled = false;
					p1_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == 1){
					GameObject p0_t1 = GameObject.Find ("p0_Bubble_1");
					GameObject p0_t2 = GameObject.Find ("p0_Bubble_2");
					p0_t1.GetComponent<GUITexture>().enabled = false;
					p0_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == extra_lines_of_text){
					lines_read = 0;
					GameRunner.loadingMessage();
					Application.LoadLevel ("Nick_Level_4_tutorialforboxandlasers");
				}
			}
		}
		else if (Application.loadedLevelName == "_Transition_3") {
			extra_lines_of_text = 2;

			if (Input.GetKeyUp (KeyCode.Return)){
				if(lines_read == 0){ //Disable current text, enable next text
					GameObject p0_t1 = GameObject.Find ("p0_Bubble_1");
					GameObject p0_t2 = GameObject.Find ("p0_Bubble_2");
					p0_t1.GetComponent<GUITexture>().enabled = false;
					p0_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == 1){
					GameObject p1_t1 = GameObject.Find ("p1_Bubble_1");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t1.GetComponent<GUITexture>().enabled = false;
					p1_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == extra_lines_of_text){
					lines_read = 0;
					GameRunner.loadingMessage();
					Application.LoadLevel ("Nick_Level_1_devils_tuning_fork_RESKIN");
				}
			}
		}
		else if (Application.loadedLevelName == "_Transition_4") {
			extra_lines_of_text = 1;
			
			if (Input.GetKeyUp (KeyCode.Return)){
				if(lines_read == 0){ //Disable current text, enable next text
					GameObject p1_t1 = GameObject.Find ("p1_Bubble_1");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t1.GetComponent<GUITexture>().enabled = false;
					p1_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if (Input.GetKeyUp (KeyCode.Return)) {
					GameRunner.loadingMessage();
					Application.LoadLevel ("Nick_Level_3_box_and_laser_level");
				}
			}
		}
		else if (Application.loadedLevelName == "_Transition_5") {
			extra_lines_of_text = 3;
			
			if (Input.GetKeyUp (KeyCode.Return)){
				if(lines_read == 0){
					GameObject p1_t1 = GameObject.Find ("p1_Bubble_1");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t1.GetComponent<GUITexture>().enabled = false;
					p1_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == 1){ //Disable current text, enable next text
					GameObject p0_t1 = GameObject.Find ("p0_Bubble_1");
					GameObject p0_t2 = GameObject.Find ("p0_Bubble_2");
					p0_t1.GetComponent<GUITexture>().enabled = false;
					p0_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == 2){
					GameObject p1_t3 = GameObject.Find ("p1_Bubble_3");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t2.GetComponent<GUITexture>().enabled = false;
					p1_t3.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == extra_lines_of_text){
					lines_read = 0;
					GameRunner.loadingMessage();
					Application.LoadLevel ("_game_play_custom_level_Jay_2");
				}
			}
		}
		else if (Application.loadedLevelName == "_Transition_6") {			
			if (Input.GetKeyUp (KeyCode.Return)) {
				GameRunner.loadingMessage();
				Application.LoadLevel ("Nick_Level_5_reflexes");
				}
		}
		else if (Application.loadedLevelName == "_Transition_7") {
			extra_lines_of_text = 2;
			
			if (Input.GetKeyUp (KeyCode.Return)){
				if(lines_read == 0){ //Disable current text, enable next text
					GameObject p0_t1 = GameObject.Find ("p0_Bubble_1");
					GameObject p0_t2 = GameObject.Find ("p0_Bubble_2");
					p0_t1.GetComponent<GUITexture>().enabled = false;
					p0_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == 1){
					GameObject p1_t1 = GameObject.Find ("p1_Bubble_1");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t1.GetComponent<GUITexture>().enabled = false;
					p1_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == extra_lines_of_text){
					lines_read = 0;
					GameRunner.loadingMessage();
					Application.LoadLevel ("_game_play_custom_level_Jay_4");
				}
			}
		}
		else if (Application.loadedLevelName == "_Transition_8") {
			extra_lines_of_text = 2;
			
			if (Input.GetKeyUp (KeyCode.Return)){
				if(lines_read == 0){
					GameObject p1_t1 = GameObject.Find ("p1_Bubble_1");
					GameObject p1_t2 = GameObject.Find ("p1_Bubble_2");
					p1_t1.GetComponent<GUITexture>().enabled = false;
					p1_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == 1){ //Disable current text, enable next text
					GameObject p0_t1 = GameObject.Find ("p0_Bubble_1");
					GameObject p0_t2 = GameObject.Find ("p0_Bubble_2");
					p0_t1.GetComponent<GUITexture>().enabled = false;
					p0_t2.GetComponent<GUITexture>().enabled = true;
					lines_read++;
				}
				else if(lines_read == extra_lines_of_text){
					lines_read = 0;
					GameRunner.loadingMessage();
					Application.LoadLevel ("Pratik_Level_1");
				}
			}
		}
	}
}
