﻿using UnityEngine;
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

	public 
	// Use this for initialization
	void Awake () {
		Generator ();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	void Update() {
		if (!activated && playerController.currentRoom == gameObject) {
			ActivateEnemies ();
			activated = true;
		}
		else if(activated) {
			activated = false;
			DeactivateEnemies();
		}
	}

	void ActivateEnemies() {
		foreach (EnemyPlaceholderController c in enemies) {
			c.active = true;
		}
	}

	void DeactivateEnemies() {
		foreach (EnemyPlaceholderController c in enemies) {
			c.active = false;
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
			if(Random.value < .4) {
				temp = (GameObject) Instantiate(enemy, new Vector3(spawn.transform.position.x, spawn.transform.position.y, 0), Quaternion.identity);
				temp.transform.parent = transform;
				enemies.Add(temp.GetComponent<EnemyPlaceholderController>());
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