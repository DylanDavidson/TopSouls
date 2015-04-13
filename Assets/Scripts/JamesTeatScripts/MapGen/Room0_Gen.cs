using UnityEngine;
using System.Collections;

public class Room0_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	public int roomDifficulty;
	public bool activated;
	public ArrayList enemies = new ArrayList ();
	public ArrayList spawns = new ArrayList ();

	public PlayerController playerController;

	private int row;
	private int col;

	public float tileSize;

	public int[,] grid;
	public GameObject [,] map;
	public GameObject [,] ob_map;
	//public GameObject NG;
	
	// Use this for initialization
	void Start () {
		activated = true;
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		Dice d = Dice.getInatance ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();

		map = new GameObject[row, col];
		ob_map = new GameObject[row, col];

		Room r1 = new Room (GameVars.num_Room_Random);
		Obstical obstical = new Obstical (GameVars.num_Room_Random, row, col);
	
		Create (row, col, obstical.grid);
		Create (row, col, r1.grid);
		EnemySpawner spawner = new EnemySpawner (roomDifficulty, spawns, ref enemies);	
	}

	void Create( int row, int col, int [,] gridf){
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				float position_x = transform.position.x + i * tileSize;
				float position_y = transform.position.y + j * tileSize;

				if (gridf [i, j] == GameVars.num_wall) {
					GameObject bob = (GameObject)Instantiate (wall, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}
				if (gridf [i, j] == GameVars.num_door) {
					GameObject bob  = (GameObject)Instantiate (door, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}
			
				if (gridf [i, j] == GameVars.num_floor) {
					GameObject bob = (GameObject)Instantiate (floor, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}

				if (gridf [i, j] == GameVars.num_enemySpawn) {
					GameObject bob = (GameObject)Instantiate (Prefab.enemy_spawn, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
					spawns.Add(bob);
				}
			}
		}
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
}
