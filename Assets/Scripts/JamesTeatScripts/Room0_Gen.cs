using UnityEngine;
using System.Collections;

public class Room0_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	private int row;
	private int col;

	public float tileSize;

	public int[,] grid;
	public GameObject [,] map;
	//public GameObject NG;
	
	// Use this for initialization
	void Start () {
		NumGen ng = NumGen.getInatance (); 
		row = ng.getX ();
		col = ng.getY ();
		Debug.Log ("rows = " + ng.getX () + " cols = " + ng.getY ());
		map = new GameObject[row, col];
		grid = new int[row, col];
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				grid[i,j] = 0;
			}
		}
		
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				float position_x = transform.position.x+i*tileSize;
				float position_y = transform.position.y+j*tileSize;
				if(grid[i,j]==0){
					map[i,j] = (GameObject)Instantiate(wall,new Vector3(position_x,position_y,0),Quaternion.identity);
					map[i,j].transform.parent = transform;
				}
			}
		}
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
