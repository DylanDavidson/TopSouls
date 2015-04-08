using UnityEngine;
using System.Collections;

public class Room1_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	private int row;
	private int col;
	public float tileSize;

	private int num_floor = 0;
	private int num_door = 2;
	private int num_wall = 1;
	public int[,] grid;
	public GameObject [,] map;
	//public GameObject NG;

	// Use this for initializations
	void Start () {
		Dice d = Dice.getInatance ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		//Debug.Log ("rows = " + ng.getX () + " cols = " + ng.getY ());
		map = new GameObject[row, col];
		Room r1 = new Room (1);
		grid = r1.grid;
		/*for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				if((i == (int)row/2)&&(j==0||j==col-1)){
					grid[i,j] =num_door;
				}
				// the bonds of the room COL,
				else if( (i==0 || i == row-1) ){
					grid[i,j]=num_wall;
				}
				// the bonds of the room COL,
				else if( (j==0 || j == col-1) ){
					grid[i,j]=num_wall;
				}
				else{
					if(row > 5 && col > 5 && i>1 && i<row-2 
					   && j>1 && j<col-2){

						d.roll();
						if(d.getVal() < d.getMaxVal()-1){
							grid[i,j] = num_floor;
						}
						else{
							grid[i,j] = num_wall;
						}
					}
					else{
						grid[i,j]=num_floor;
					}
				}
			}
		}*/

		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				float position_x = transform.position.x+i*tileSize;
				float position_y = transform.position.y+j*tileSize;
				if(r1.grid[i,j] !=0)
				{
					if(grid[i,j]==num_wall){
						map[i,j] = (GameObject)Instantiate(wall,new Vector3(position_x,position_y,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					if(grid[i,j]==num_door){
						map[i,j] = (GameObject)Instantiate(door,new Vector3(position_x,position_y,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
				}
				if(grid[i,j]==num_floor){
					map[i,j] = (GameObject)Instantiate(floor,new Vector3(position_x,position_y,0),Quaternion.identity);
					map[i,j].transform.parent = transform;
				}
			}
		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
