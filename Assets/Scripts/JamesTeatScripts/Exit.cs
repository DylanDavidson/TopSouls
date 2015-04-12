using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Player") {
			Debug.Log("You win");
		}
	}

	void OnTriggerExit2D(Collider2D target){
		Debug.Log("You left the exit");
	}

}
