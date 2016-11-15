using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public Vector3 playerPos;
	public bool entered = false;
	// keep track of classrooms that have had toolboxes
	private Dictionary<string,int> toolboxes = new Dictionary<string,int>();

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
		foreach (KeyValuePair<string, int> kvp in toolboxes)
		{
			toolboxes [kvp.Key] -= (int)Time.deltaTime;
		}
	}

	public bool GenerateToolbox(string room){
		// randomly decide if a toolbox should appear
		bool display = DisplayOrNot ();
		if (!toolboxes.ContainsKey (room)) {
			// don't generate a new toolbox in this room for at least 60s
			toolboxes.Add (room, 60);
		} else if (toolboxes [room] > 0) {
			// timer hasn't expired yet
			return false;
		} else {
			// reset the timer once it expires
			toolboxes [room] = 60;
		}
		return display;
	}

	bool DisplayOrNot(){
		if (Random.value >= 0.5)
		{
			return true;
		}
		return false;
	}
}
