using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	public Vector3 offset;

	// Use this for initialization
	void Start () {
        // get the initial offset of the camera from the player (so the camera stays in 1 place)
        //offset = transform.position - player.transform.position;
        offset = new Vector3(0, 0, -10);
    }

	// LateUpdate is called once per frame, after all other calculations
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
