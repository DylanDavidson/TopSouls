using UnityEngine;
using System.Collections;

public class RoomFinal_Gen : MonoBehaviour {

	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	public GameObject enemy;
	public GameObject exit;
	
	
	
	private int row;
	private int col;
	
	public float tileSize;
	
	public int[,] grid;
	public GameObject [,] map;
	public GameObject [,] ob_map;

	// Use this for initialization
	void Start () {
		Dice d = Dice.getInatance ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		
		map = new GameObject[row, col];
		ob_map = new GameObject[row, col];
		
		Room r1 = new Room (GameVars.num_Room_Final);
		Obstical obstical = new Obstical (GameVars.num_Room_Final, row, col);
		
		Create (row, col, obstical.grid);
		Create (row, col, r1.grid);
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
					Debug.Log(gridf [i, j]);
					GameObject bob = (GameObject)Instantiate (enemy, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
					bob.GetComponent<SpriteRenderer>().sortingOrder =1;
					bob.GetComponent<EnemyPlaceholderController>().active=false;
				}

				if(gridf[i,j] == GameVars.num_exit){
					GameObject bob = (GameObject)Instantiate (exit, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					bob.transform.parent = transform;
				}
			}
		}
	}
}
