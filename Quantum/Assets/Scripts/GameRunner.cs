﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour {
	static public GameRunner S;
	
	static public bool P1colGoal = false;
	static public bool P2colGoal = false;
	
	static bool dead = false;
	static bool winSoundBool = false;
	static bool deathSoundBool = false;
	
	public Text winText;
	public Text pauseText;
	public static Text deathText;
	public static Text loadingText;
	public AudioClip winSound;
	public AudioClip deathSound;
	public AudioClip alarm;
	public AudioClip deathSound2;
	
	private AudioSource source;
	
	//static GameObject loadingScreen = GameObject.Find("LoadingObject");
	
	//GameObject P1 = GameObject.Find("Player1");
	//GameObject P2 = GameObject.Find("Player2");
	
	static SortedDictionary<string,bool> levels_completed = new SortedDictionary<string, bool>();
	static bool levels_initialized = false;
	
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		winSoundBool = false;
		deathSoundBool = false;
		
		GameObject winDisp = GameObject.Find("WinDisp");
		winText = winDisp.GetComponent<Text>();
		
		GameObject PauseDisp = GameObject.Find("Pause");
		pauseText = PauseDisp.GetComponent<Text>();
		
		GameObject DeathDisp = GameObject.Find("Death");
		deathText = DeathDisp.GetComponent<Text>();
		
		GameObject LoadDisp = GameObject.Find ("Loading");
		loadingText = LoadDisp.GetComponent<Text>();
		
		winText.text = "";
		pauseText.text = "";
		deathText.text = "";
		loadingText.text = "";
		
		Time.timeScale = 1;
		
		//loadingScreen = GameObject.Find("LoadingObject");
	}
	
	void Awake(){
		S = this;
		
		//winSoundBool = false;
		//deathSoundBool = false;
		
		//Initilaize level completion tracking variable
		LevelSelect.initLevels ();
		
		if(!levels_initialized){
			//print("count: " + LevelSelect.levels.Count);
			for(int x = 0; x < LevelSelect.levels.Count; x++)
				levels_completed.Add(LevelSelect.levels[x], false);
			
			levels_initialized = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//get rid of or change this in the future, maybe have it call a cutscene, and then go to next level?
		if (P1colGoal && P2colGoal) {
			if (winSoundBool == false) {
				source.PlayOneShot(winSound);
			}
			winSoundBool = true;
			winText.text = "ACCESS GRANTED \n PRESS ENTER";
			Time.timeScale = 0;
			
			levels_completed[Application.loadedLevelName] = true;  //Update levels completed
			
			if(Input.GetKeyUp(KeyCode.Return)) {
			   if (!AllLevelsDone()) {
					//Load the next level in the sequence
					LevelSelect.current_level_id++;
					
					loadingText.text = "LOADING";
					Application.LoadLevel(LevelSelect.levels[LevelSelect.current_level_id]);
				} else {
					loadingText.text = "LOADING";
					Application.LoadLevel("_Success");
				}
			}
		}
		
		//deathText.text = "";
		
		if(Input.GetKeyDown(KeyCode.Escape)) {
			loadingText.text = "LOADING";
			Application.LoadLevel("_Start_Screen");
		}
		
		//reset level on press of R key
		if (Input.GetKeyDown (KeyCode.R)) {
			Time.timeScale = 1;
			//loadingText.text = "LOADING";
			Application.LoadLevel(Application.loadedLevel);
			dead = false;
		}
		//reset level on press of enter if dead
		if (Input.GetKeyDown (KeyCode.Return) && dead) {
			Time.timeScale = 1;
			//loadingText.text = "LOADING";
			Application.LoadLevel(Application.loadedLevel);
			dead = false;
		}
		
		//press P to pause and unpause level
		if (Input.GetKeyDown (KeyCode.P) && dead == false) {
			if (Time.timeScale == 0) {
				pauseText.text = "";
				Time.timeScale = 1;
			}
			else if (Time.timeScale == 1) {
				Time.timeScale = 0;
				pauseText.text = "Pause";
			}	
		}
	}
	
	bool AllLevelsDone(){
//		foreach (bool completed in levels_completed.Values) {
//			//			print("Count: " + levels_completed.Values.Count);
//			
//			if (!completed)
//				return false;
//		}
//		
//		//		print ("blah");
//		return true;
		return LevelSelect.current_level_id + 1 >= LevelSelect.num_levels;
	}
	
	
	public static void killedByEnemy() {
		Time.timeScale = 0;
		dead = true;
		
		if (deathSoundBool == false) {
			S.source.PlayOneShot(S.deathSound);
			S.source.PlayOneShot(S.alarm);
		}
		deathSoundBool = true;
		
		deathText.text = "ALL \n SYSTEMS \n CORRUPTED";
	}
	
	public static void killedByLava() {
		Time.timeScale = 0;
		dead = true;
		
		if (deathSoundBool == false) {
			S.source.PlayOneShot(S.deathSound2);
			S.source.PlayOneShot(S.alarm);
		}
		deathSoundBool = true;
		
		deathText.text = "ALERT: \n FIREWALL \n ENGAGED";
	}
	
	public static void loadingMessage() {
		GameObject LoadDisp = GameObject.Find ("Loading");
		loadingText = LoadDisp.GetComponent<Text>();	

		loadingText.text = "LOADING";
	}
		
}
