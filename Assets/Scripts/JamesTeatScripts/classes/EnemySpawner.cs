using UnityEngine;
using System.Collections;

public class EnemySpawner
{
	private int roomDifficulty;
	private ArrayList spawns;
	private ArrayList enemies;
	private GameObject[] enemiesGO; 
	private GameObject room;
		// Use this for initialization
	public EnemySpawner(int roomDifficulty, ArrayList spawns, ref ArrayList enemies) {
		this.roomDifficulty = roomDifficulty;
		this.spawns = spawns;
		this.enemies = enemies;
		enemiesGO = new GameObject[15];
		GenerateEnemies ();

	}
	
	void GenerateEnemies() {
		foreach(GameObject spawn in spawns) {
			int i =0;
			if(roomDifficulty <= 0)
				break;
			GameObject temp = (GameObject) Object.Instantiate(
				Prefab.enemy, 
				new Vector3(spawn.transform.position.x, spawn.transform.position.y, -1), 
				Quaternion.identity
			);
			temp.layer = 1;
			temp.transform.parent = spawn.transform;
			enemies.Add(temp.GetComponent<EnemyPlaceholderController>());
			enemiesGO[i] = temp;
			if(enemiesGO[i]== null){
				Debug.Log("sfsdfiosefioesfioejfiojefiohisohihihoiiodhifhihihi");
			}

			roomDifficulty -= 5;
			i++;
		}
	}

	public GameObject[] GetEnemies(){return enemiesGO;}
}

