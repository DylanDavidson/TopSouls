using UnityEngine;
using System.Collections;

public class EnemySpawner
{
	private int roomDifficulty;
	private ArrayList spawns;
	private GameObject room;
		// Use this for initialization
	public EnemySpawner(int roomDifficulty, ArrayList spawns) {
		this.roomDifficulty = roomDifficulty;
		this.spawns = spawns;
		GenerateEnemies ();
	}
	
	void GenerateEnemies() {
		foreach(GameObject spawn in spawns) {
			if(roomDifficulty <= 0)
				break;
			GameObject temp = (GameObject) Object.Instantiate(
				Prefab.enemy, 
				new Vector3(spawn.transform.position.x, spawn.transform.position.y, -1), 
				Quaternion.identity
			);
			temp.transform.parent = spawn.transform;
			//enemies.Add(temp.GetComponent<EnemyPlaceholderController>());
			roomDifficulty -= 5;
		}
	}
}

