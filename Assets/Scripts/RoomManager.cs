using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public GameObject blankFloor;
	public GameObject wall;
	// Use this for initialization
	void Room1() {
		TextAsset room1 = (TextAsset) Resources.Load ("Room1");
		float y = 4;
		float x = -8;
		for(int i = 0; i < room1.text.Length; i++) {
			if(room1.text[i] == 'X') {
				Instantiate(wall, new Vector3(x, y, 1), Quaternion.identity);
				x += 1;
			}
			else if(room1.text[i] == 'O') {
				Instantiate(blankFloor, new Vector3(x, y, 1), Quaternion.identity);
				x += 1;
			}
			else if(room1.text[i] == 'F') {
				if(Random.value < .5)
					Instantiate(wall, new Vector3(x, y, 1), Quaternion.identity);
				else
					Instantiate(blankFloor, new Vector3(x, y, 1), Quaternion.identity);
				x += 1;
			}
			if(x > 8) {
				x = -8;
				y -= 1;
			}
		}
	}

	void Room2() {
		TextAsset room2 = (TextAsset) Resources.Load ("Room2");
		float y = 4;
		float x = -25;
		for(int i = 0; i < room2.text.Length; i++) {
			if(room2.text[i] == 'X') {
				Instantiate(wall, new Vector3(x, y, 1), Quaternion.identity);
				x += 1;
			}
			else if(room2.text[i] == 'O') {
				Instantiate(blankFloor, new Vector3(x, y, 1), Quaternion.identity);
				x += 1;
			}
			else if(room2.text[i] == 'F') {
				if(Random.value < .5)
					Instantiate(wall, new Vector3(x, y, 1), Quaternion.identity);
				else
					Instantiate(blankFloor, new Vector3(x, y, 1), Quaternion.identity);
				x += 1;
			}
			if(x >= -8) {
				x = -25;
				y -= 1;
			}
		}
	}

	void Start () {
		Room1 ();
		Room2 ();
	}
}
