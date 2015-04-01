using UnityEngine;
using System.Collections;

public class Room2_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	private int row;
	private int col;
	public int[,] grid;
	public GameObject [,] map;
	public GameObject NG;
	
	// Use this for initializations
	void Start () {
		NG = GameObject.Find ("NumberGenerator");
		NumberGenerator ng = NG.GetComponent<NumberGenerator> ();
		row = ng.getX ();
		col = ng.getY ();
		map = new GameObject[row, col];
		grid = new int[row, col];
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				if((i ==(int)row/2)&&(j==0||j==col-1)){
					grid[i,j] =1;
				}
				else if((j ==(int)col/2)&&(i==row-1 || i ==0)){
					grid[i,j] =1;
				}
				else if( (i==0 || i == row-1) ){
					grid[i,j]=2;
				}
				else if( (j==0 || j == col-1) ){
					grid[i,j]=2;
				}
				else{
					grid[i,j]=0;
				}
			}
		}
		
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				if(grid[i,j] !=0)
				{
					if(grid[i,j]==2){
						map[i,j] = (GameObject)Instantiate(wall,new Vector3(transform.position.x+i*.32f,transform.position.y+j*.32f,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					if(grid[i,j]==1){
						map[i,j] = (GameObject)Instantiate(door,new Vector3(transform.position.x+i*.32f,transform.position.y+j*.32f,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
				}
				if(grid[i,j]==0){
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
