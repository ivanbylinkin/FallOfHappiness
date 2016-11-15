using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public Vector3 playerPos;
	public bool entered = false;
	// keep track of classrooms that have had toolboxes
	private Dictionary<string,int> toolboxes = new Dictionary<string,int>();
	private int toolboxWaiting = -1;

	// Use this for initialization
	void Awake(){
		// only have 1 game manager ever
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		List<string> keys = new List<string> (toolboxes.Keys);
		foreach (string key in keys)
		{
			if (toolboxes [key] != toolboxWaiting && toolboxes [key] > 0) { 
				toolboxes [key] -= (int)Time.deltaTime;
				toolboxes [key] = Mathf.Max (toolboxes [key], 0);
			}
			print (key + " " + toolboxes [key]);
		}
	}

	public bool GenerateToolbox(string room){
		// randomly decide if a toolbox should appear
		bool display = DisplayOrNot ();
		if (!toolboxes.ContainsKey (room) && display) {
			toolboxes.Add (room, toolboxWaiting);
		} else if (toolboxes [room] > 0) {
			// timer hasn't expired yet
			return false;
		} else if (toolboxes [room] == toolboxWaiting) {
			// toolbox hasn't been opened yet
			return true;
		}
		return display;
	}

	public void OpenToolbox(string room){
		// don't regenerate for at least 60s
		toolboxes [room] = 60;
	}

	bool DisplayOrNot(){
		if (Random.value >= 0.5)
		{
			return true;
		}
		return false;
	}
}
