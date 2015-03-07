﻿using UnityEngine;
using System.Collections;

public class Room0_Gen : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	public GameObject door;
	public int row;
	public int col;
	public int[,] grid;
	public GameObject [,] map;
	
	// Use this for initialization
	void Start () {
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
				if(grid[i,j]==0){
					map[i,j] = (GameObject)Instantiate(wall,new Vector3(transform.position.x+i*.32f,transform.position.y+j*.32f,0),Quaternion.identity);
					map[i,j].transform.parent = transform;
				}
			}
		}
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}