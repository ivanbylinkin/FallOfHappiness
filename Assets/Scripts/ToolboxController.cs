using UnityEngine;
using System.Collections;

public class ToolboxController : MonoBehaviour {
	// TODO: have an array of goods to be found and randomly select what this toolbox contains
	public float sliethCoins = 0;
	public string room;

	// Use this for initialization
	void Awake () {
		bool keep = GameManager.instance.GenerateToolbox (room);
		if (!keep) {
			Destroy (gameObject);
		} else {
			// generate random goods/cash
			if (sliethCoins == 0){ sliethCoins = RandomCoins (); }
		}
	}

	float RandomCoins(){
		float value = Random.value * 5;
		if (value >= 3.5 && HighValue ()) {
			// sometimes you get lucky and hit a high value toolbox
			return 10f;
		}
		return value;
	}

	bool HighValue(){
		if (Random.value >= 0.5)
		{
			return true;
		}
		return false;
	}
}
