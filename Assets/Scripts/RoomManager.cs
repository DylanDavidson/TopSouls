using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public GameObject blankFloor;
	public GameObject wall;
	private float x;
	private float y;
	// Use this for initialization
	void Floor1() {
		TextAsset floor1 = (TextAsset) Resources.Load ("Floor1");
		y = 4f;
		x = -8f;
		float baseX = -8f;
		float currY = 4f;
		for(int i = 0; i < 4; i++) {
			for(int j = 0; j < 4; j++) {
				RoomGenerator(floor1.text[(5 * i) + j], x);
				y = currY;
				x += 17;
			}
			x = baseX;
			currY -= 9;
			y = currY;
		}
	}

	void RoomGenerator(char roomNum, float startX) {
		float endX = startX + 17;
		TextAsset room = (TextAsset) Resources.Load ("Room" + roomNum);
		for(int i = 0; i < room.text.Length; i++) {
			RoomSquareGenerator(room.text[i]);
			if(x >= endX) {
				x = startX;
				y -= 1;
			}
		}
	}

	void RoomSquareGenerator(char tile) {
		if(tile == 'X') {
			Instantiate(wall, new Vector3(x, y, 1), Quaternion.identity);
			x += 1;
		}
		else if(tile == 'O') {
			Instantiate(blankFloor, new Vector3(x, y, 1), Quaternion.identity);
			x += 1;
		}
		else if(tile == 'F') {
			if(Random.value < .5)
				Instantiate(wall, new Vector3(x, y, 1), Quaternion.identity);
			else
				Instantiate(blankFloor, new Vector3(x, y, 1), Quaternion.identity);
			x += 1;
		}
	}

	void Awake () {
		Floor1 ();
	}
}
