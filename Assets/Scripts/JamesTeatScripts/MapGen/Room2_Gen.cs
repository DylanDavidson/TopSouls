using UnityEngine;
using System.Collections;

public class Room2_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	private int row;
	private int col;

	public float tileSize;
	
	public int[,] grid;
	public GameObject [,] map;
	//public GameObject NG;
	
	// Use this for initializations
	void Start () {
		Dice d = Dice.getInatance ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		map = new GameObject[row, col];

		Room r1 = new Room (2);
		Obstical obstical = new Obstical (0, row, col);
		
		Create (row, col, obstical.grid);
		Create (row, col, r1.grid);

		
		

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
			}
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
