using UnityEngine;
using System.Collections;

public class Room0_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;

	private int num_floor = 0;
	private int num_door = 2;
	private int num_wall = 1;

	private int row;
	private int col;

	public float tileSize;

	public int[,] grid;
	public GameObject [,] map;
	public GameObject [,] ob_map;
	//public GameObject NG;
	
	// Use this for initialization
	void Start () {
		Dice d = Dice.getInatance ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		Debug.Log ("rows = " + ng.getX () + " cols = " + ng.getY ());
		map = new GameObject[row, col];
		ob_map = new GameObject[row, col];

		Room r1 = new Room (0);
		Obstical obstical = new Obstical (0, row, col);
		grid = r1.grid;

		/*for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				if((i ==(int)row/2)&&(j==0||j==col-1)){
					d.roll();
					if(d.getVal() < d.getMaxVal()){
						grid[i,j] =num_door;
					}
					else{grid[i,j] =num_wall;}
				}
				else if((j ==(int)col/2)&&(i==row-1 || i ==0)){
					d.roll();
					if(d.getVal() < d.getMaxVal()/4){
						grid[i,j] =num_door;
					}
					else{grid[i,j] =num_wall;}
				}
				else if( (i==0 || i == row-1) ){
					grid[i,j]=num_wall;
				}
				else if( (j==0 || j == col-1) ){
					grid[i,j]=num_wall;
				}
				else{
					if(row > 5 && col > 5 && i>1 && i<row-2 
					   && j>1 && j<col-2){

						d.roll();
						if(d.getVal() < d.getMaxVal()){
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





				//grid[i,j] = 0;
			}
		}*/
		int ob_row = obstical.getRow ();
		int ob_col = obstical.getCol ();
		
		int[,] ob_grid = obstical.grid;
		
		
		for (int i=0; i<ob_row; i++) {
			for(int j=0; j<ob_col;j++)
			{
				float position_x = transform.position.x+(i)*tileSize;
				float position_y = transform.position.y+(j)*tileSize;
				if(ob_grid[i,j] !=0)
				{
					if(ob_grid[i,j]==1){
						GameObject bob =(GameObject)Instantiate(wall,new Vector3(position_x,position_y,0),Quaternion.identity);
						bob.transform.parent = transform;
					}
				}
			}
		}

		for (int i=0; i<row; i++) {
			for (int j=0; j<col; j++) {
				float position_x = transform.position.x + i * tileSize;
				float position_y = transform.position.y + j * tileSize;
				if (grid [i, j] != 0) {
					if (grid [i, j] == num_wall) {
						map [i, j] = (GameObject)Instantiate (wall, new Vector3 (position_x, position_y, 0), Quaternion.identity);
						map [i, j].transform.parent = transform;
					}
					if (grid [i, j] == num_door) {
						map [i, j] = (GameObject)Instantiate (door, new Vector3 (position_x, position_y, 0), Quaternion.identity);
						map [i, j].transform.parent = transform;
					}
				}
				if (grid [i, j] == num_floor) {
					map [i, j] = (GameObject)Instantiate (floor, new Vector3 (position_x, position_y, 0), Quaternion.identity);
					map [i, j].transform.parent = transform;
				}
			}
		}



		//GameObject bob = (GameObject)Instantiate (wall, new Vector3 (10, 10, 0), Quaternion.identity);
		

		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
