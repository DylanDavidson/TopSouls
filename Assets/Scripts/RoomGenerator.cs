using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	public int roomNum;
	public GameObject blankFloor;
	public GameObject wall;
	// Use this for initialization
	void Start () {
		Generator ();
	}

	void Generator() {
		float endX = 15;
		TextAsset room = (TextAsset) Resources.Load ("Room_" + roomNum);
		float x = 0;
		float y = 0;
		for(int i = 0; i < room.text.Length; i++) {
			if(room.text[i] == '\n')
				continue;
			RoomSquareGenerator(room.text[i], x, y);
			//Debug.Log(room.text[i]);
			x += 1;
			if(x >= endX) {
				x = 0;
				y -= 1;
			}
		}
	}

	void RoomSquareGenerator(char tile, float x, float y) {
		GameObject temp = null;
		float thisX = transform.position.x + x;
		float thisY = transform.position.y + y;
		if(tile == 'X') {
			temp = (GameObject) Instantiate(wall, new Vector3(thisX, thisY, 1), Quaternion.identity);
			temp.transform.parent = transform;
		}
		else if(tile == 'O') {
			temp = (GameObject) Instantiate(blankFloor, new Vector3(thisX, thisY, 1), Quaternion.identity);
			temp.transform.parent = transform;
		}
		else if(tile == 'F') {
			if(Random.value < .5)
				temp = (GameObject) Instantiate(wall, new Vector3(thisX, thisY, 1), Quaternion.identity);
			else
				temp = (GameObject) Instantiate(blankFloor, new Vector3(thisX, thisY, 1), Quaternion.identity);
			temp.transform.parent = transform;
		}
	}
}
