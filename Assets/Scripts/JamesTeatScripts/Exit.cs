using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Player") {
			Application.LoadLevel ("boss_scene");
		}
	}

	void OnTriggerExit2D(Collider2D target){
		Debug.Log("You left the exit");
	}

}
