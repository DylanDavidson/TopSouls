using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	public int roomNum;
	public GameObject blankFloor;
	public GameObject wall;
	public GameObject spawn_point;
	public GameObject enemy;
	public GameObject enemy_spawn;
	public bool activated = false;
	public PlayerController playerController;
	public ArrayList spawns = new ArrayList ();
	public ArrayList enemies = new ArrayList();
	public int difficultyScore = 5;

	public 
	// Use this for initialization
	void Awake () {
		if(transform.position.x == 15)
			difficultyScore += 5;
		else if(transform.position.x == 30)
			difficultyScore += 10;
		else if(transform.position.x == 45)
			difficultyScore += 15;
		Generator ();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	void Update() {
		if (!activated && playerController.currentRoom == gameObject) {
			activated = true;
			ActivateEnemies ();
		}
		else if(activated && playerController.currentRoom != gameObject) {
			activated = false;
			DeactivateEnemies();
		}
	}

	void ActivateEnemies() {
		foreach (EnemyPlaceholderController c in enemies) {
			c.active = true;
			c.transform.position = c.transform.parent.position;
		}
	}

	void DeactivateEnemies() {
		foreach (EnemyPlaceholderController c in enemies) {
			c.active = false;
		}
	}

	public void CloseDoors(bool closeTop, bool closeRight, bool closeDown, bool closeLeft) {
		GameObject temp = null;

		if(closeTop) {
			for(int i = 6; i <= 8; i++) {
				temp = (GameObject) Instantiate(wall, new Vector3(transform.position.x + i, transform.position.y, 0), Quaternion.identity);
				temp.transform.parent = transform;
			}
		}
		if(closeRight) {
			for(int i = 6; i <= 8; i++) {
				temp = (GameObject) Instantiate(wall, new Vector3(transform.position.x + 14, transform.position.y - i, 0), Quaternion.identity);
				temp.transform.parent = transform;
			}
		}
		if(closeDown) {
			for(int i = 6; i <= 8; i++) {
				temp = (GameObject) Instantiate(wall, new Vector3(transform.position.x + i, transform.position.y - 14, 0), Quaternion.identity);
				temp.transform.parent = transform;
			}
		}
		if(closeLeft) {
			for(int i = 6; i <= 8; i++) {
				temp = (GameObject) Instantiate(wall, new Vector3(transform.position.x, transform.position.y - i, 0), Quaternion.identity);
				temp.transform.parent = transform;
			}
		}
	}

	void Generator() {
		float endX = 15;
		TextAsset room = (TextAsset) Resources.Load ("Room_" + roomNum);
		float x = 0;
		float y = 0;
		for(int i = 0; i < room.text.Length; i++) {
			if(!char.IsLetterOrDigit(room.text[i]))
				continue;
			RoomSquareGenerator(room.text[i], x, y);
			//Debug.Log(room.text[i]);
			x += 1;
			if(x >= endX) {
				x = 0; 
				y -= 1;
			}
		}
		if (transform.position.x == 0 && roomNum != 0) {
			float middleX = transform.position.x + 7;
			float middleY = transform.position.y - 7;
			while(Physics2D.OverlapPoint(new Vector2(middleX, middleY)).CompareTag("Wall")) {
				middleX -= 1;
			}
			Instantiate(spawn_point, new Vector3(middleX, middleY, 1), Quaternion.identity);
		}
		GameObject temp = null;
		foreach(GameObject spawn in spawns) {
			if(difficultyScore > 0 && Random.value < 0.8) {
				temp = (GameObject) Instantiate(enemy, new Vector3(spawn.transform.position.x, spawn.transform.position.y, 0), Quaternion.identity);
				temp.transform.parent = spawn.transform;
				enemies.Add(temp.GetComponent<EnemyPlaceholderController>());
				difficultyScore -= 5;
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
		else if(tile == 'E') {
			temp = (GameObject) Instantiate(blankFloor, new Vector3(thisX, thisY, 1), Quaternion.identity);
			temp.transform.parent = transform;
			temp = (GameObject) Instantiate(enemy_spawn, new Vector3(thisX, thisY, 0), Quaternion.identity);
			temp.transform.parent = transform;
			spawns.Add (temp);
		}
	}
}
