using UnityEngine;
using System.Collections;

public class Room3_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	private int row;
	private int col;

	private int num_floor = 0;
	private int num_door = 2;
	private int num_wall = 1;
	public int[,] grid;
	public GameObject [,] map;
	public GameObject numberGen;
	//public GameObject NG;
	
	// Use this for initializations
	void Start () {
		Dice d = Dice.getInatance ();
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		map = new GameObject[row, col];
		grid = new int[row, col];
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				if((i ==(int)row/2)&&(j==0||j==col-1)){
					grid[i,j] =num_door;
				}
				else if((j ==(int)col/2)&&(i==0)){
					grid[i,j] =num_door;
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
		}
		
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				if(grid[i,j] !=0)
				{
					if(grid[i,j]==num_wall){
						map[i,j] = (GameObject)Instantiate(wall,new Vector3(transform.position.x+i*.32f,transform.position.y+j*.32f,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					if(grid[i,j]==num_door){
						map[i,j] = (GameObject)Instantiate(door,new Vector3(transform.position.x+i*.32f,transform.position.y+j*.32f,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
				}
				if(grid[i,j]==num_floor){
					map[i,j] = (GameObject)Instantiate(floor,new Vector3(transform.position.x+i*.32f,transform.position.y+j*.32f,0),Quaternion.identity);
					map[i,j].transform.parent = transform;
				}
			}
		}
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
