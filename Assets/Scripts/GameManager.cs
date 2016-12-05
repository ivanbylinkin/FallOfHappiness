using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public Dictionary<string,Vector3> playerPos = new Dictionary<string, Vector3>();
    public Dictionary<string,bool> entered = new Dictionary<string, bool>();
    public string currentRoom;
    public string currentFloor = "first";
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

        // preload play position and entered
        playerPos[currentFloor] = Vector3.zero;
        entered[currentFloor] = false;
	}

    void Start()
    {
        InvokeRepeating("UpdateEverySecond", 0, 1.0f);
    }

	void UpdateEverySecond(){
		List<string> keys = new List<string> (toolboxes.Keys);
		foreach (string key in keys)
		{
			if (toolboxes [key] != toolboxWaiting && toolboxes [key] > 0) { 
				toolboxes [key]--;
                print(toolboxes[key]);
                toolboxes [key] = Mathf.Max (toolboxes [key], 0);
			}
		}
	}

	public bool GenerateToolbox(string room){
		// randomly decide if a toolbox should appear
		bool display = DisplayOrNot ();
        print(display);
        if (!toolboxes.ContainsKey (room + currentRoom) && display) {
			toolboxes.Add (room + currentRoom, toolboxWaiting);
		} else if (toolboxes [room + currentRoom] > 0) {
			// timer hasn't expired yet
			return false;
		} else if (toolboxes [room + currentRoom] == toolboxWaiting) {
			// toolbox hasn't been opened yet
			return true;
		} else if (toolboxes [room + currentRoom] == 0 && display) {
            toolboxes[room + currentRoom] = toolboxWaiting;
        }
		return display;
	}

	public void OpenToolbox(string room){
		// don't regenerate for at least 60s
		toolboxes [room+currentRoom] = 60;
	}
	private bool DisplayOrNot(){
		if (Random.value >= 0.25)
		{
			return true;
		}
		return false;
	}
}
