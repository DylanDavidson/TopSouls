using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	void DoorOpen(){

		gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	void DoorClose(){
		gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;

	}
}
