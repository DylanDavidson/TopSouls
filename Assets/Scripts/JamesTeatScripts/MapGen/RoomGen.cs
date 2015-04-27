using UnityEngine;
using System.Collections;

public class RoomGen : MonoBehaviour
{
	public int room_num;
	public int roomDifficulty;
	
	public bool activated = false;
	public bool player_inRoom = false;
	public bool enemies_dead = false;

	public ArrayList enemies = new ArrayList ();
	public ArrayList spawns = new ArrayList ();
	public PlayerController playerController;

	private int row;
	private int col;
	
	public float tileSize = 1;
	
	public int[,] grid;
	private GameObject [] doors = new GameObject[11];

	public GameObject [] num_enemies;

	// Use this for initialization
	void Awake ()
	{
		activated = true;
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		
		//map = new GameObject[row, col];
		//ob_map = new GameObject[row, col];
		
		Room r1 = new Room (room_num);
		Obstical obstical;
		if (room_num == 5) {
			obstical = new Obstical (GameVars.num_exit, row, col);
		} 
		else {
			obstical = new Obstical (GameVars.num_Room_Random, row, col);
		}

		Create (row, col, obstical.grid);
		Create (row, col, r1.grid);
		if (transform.position.x == 0 && gameObject.name == "Room4(Clone)") {
			AddPlayerSpawn ();
		}
		BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
		collider.size = new Vector2 ((row * tileSize)-tileSize*2, (col * tileSize)-tileSize*2);
		collider.isTrigger = true;
		Vector2 v = new Vector2 (((row * tileSize) / 2) -tileSize/2, ((col * tileSize) / 2)-tileSize/2);
		collider.center = v;
	}

	void Start() {
		EnemySpawner s = new EnemySpawner (roomDifficulty, spawns, ref enemies);
		num_enemies = s.GetEnemies ();


		DeactivateEnemies ();
		activated = false;
	}

	void Update(){
		if (player_inRoom) {
			if (num_enemies [0] == null
				&& num_enemies [1] == null
				&& num_enemies [2] == null
				&& num_enemies [3] == null
				&& num_enemies [4] == null
			    && num_enemies [5] == null
			    && num_enemies [6] == null
			    && num_enemies [7] == null
			    && num_enemies [8] == null 
			    && num_enemies [9] == null
			    && num_enemies [10] == null
				&& enemies_dead == false) {

				for (int i =0; i<doors.Length; i++) {
					if (doors [i] != null) {
						doors [i].GetComponent<Animator> ().SetInteger ("AniState", 2);
					}
				}
				enemies_dead = true;
			}
		}
	}


	void AddPlayerSpawn() {
		float middleX = transform.position.x + 1;
		float middleY = transform.position.y + (col/2);
//		while(Physics2D.OverlapPoint(new Vector2(middleX, middleY)).CompareTag("Wall")) {
//			middleX -= 1;
//		}
		Instantiate(
			Prefab.player_spawn, 
			new Vector3(middleX, middleY, 1), 
			Quaternion.identity
		);
	}

	void Create( int row, int col, int [,] gridf){
		int num_door = 0;
		int num_enemies = 0;
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				float position_x = transform.position.x + i * tileSize;
				float position_y = transform.position.y + j * tileSize;
				
				if (gridf [i, j] == GameVars.num_wall) {
					GameObject bob = (GameObject)Instantiate (Prefab.wall, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}
				else if (gridf [i, j] == GameVars.num_door) {
				
					GameObject bob  = (GameObject)Instantiate (Prefab.door, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
					if(num_door < 4){
						doors[num_door] = bob;
						num_door++;
					}
				}	
				else if (gridf [i, j] == GameVars.num_floor) {
					GameObject bob = (GameObject)Instantiate (Prefab.floor, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}	
				else if (gridf [i, j] == GameVars.num_enemySpawn) {
					GameObject bob = (GameObject)Instantiate (Prefab.enemy_spawn, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
					spawns.Add(bob);
				}				
				else if(gridf[i,j] == GameVars.num_exit){
					GameObject bob = (GameObject)Instantiate (Prefab.exit, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}
				else if(gridf[i,j] == GameVars.num_powerup){
					GameObject bob = (GameObject)Instantiate (Prefab.floor, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
					GameObject powerup;
					if(Random.value < 0.5f)
						powerup = Prefab.speed_powerup;
					else
						powerup = Prefab.health_powerup;
					bob = (GameObject)Instantiate (powerup, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;				
				}
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			player_inRoom = true;
			if (!activated) {
				activated = true;
				ActivateEnemies ();
			}

			if(enemies.Count != 0){
				for(int i =0; i<doors.Length; i++){
					if(doors[i] != null){
						doors[i].GetComponent<Animator>().SetInteger("AniState",1);
					}
				}
			}

		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			player_inRoom =false;
			if(activated) {
				activated = false;
				DeactivateEnemies();
			}
		}}
	
	void ActivateEnemies() {
		for(int i = enemies.Count - 1; i >= 0; i--) {
			EnemyPlaceholderController c = (EnemyPlaceholderController) enemies[i];
			if(c != null && c.gameObject != null) {
				c.active = true;
				c.transform.position = c.transform.parent.position;
			}
			else
				enemies.Remove(c);
		}
	}
	
	void DeactivateEnemies() {
		for(int i = enemies.Count - 1; i >= 0; i--)  {
			EnemyPlaceholderController c = (EnemyPlaceholderController) enemies[i];
			if(c != null && c.gameObject != null)
				c.active = false;
			else{
				enemies.Remove(c);
				}
		}
	}

	public void CloseDoors(bool closeTop, bool closeRight, bool closeDown, bool closeLeft) {
		GameObject temp = null;
		if(closeTop) {
			temp = (GameObject) Instantiate(
				Prefab.wall,
				new Vector3(transform.position.x + (row/2), transform.position.y + (col-1), 0), 
				Quaternion.identity
			);
			temp.transform.parent = transform;
		}
		if(closeRight) {
			temp = (GameObject) Instantiate(
				Prefab.wall,
				new Vector3(transform.position.x + (row-1), transform.position.y + (col/2), 0), 
				Quaternion.identity
				);
			temp.transform.parent = transform;
		}
		if(closeDown) {
			temp = (GameObject) Instantiate(
				Prefab.wall,
				new Vector3(transform.position.x + (row/2), transform.position.y, 0), 
				Quaternion.identity
				);
			temp.transform.parent = transform;
		}
		if(closeLeft) {
			temp = (GameObject) Instantiate(
				Prefab.wall,
				new Vector3(transform.position.x, transform.position.y + (col/2), 0), 
				Quaternion.identity
				);
			temp.transform.parent = transform;
		}
	}
}

