using UnityEngine;
using System.Collections.Generic;

public class MapGenerationDylan : MonoBehaviour {
	public float tileWidth;
	public float tileHeight;
	public int tilesPerRoom;
	public GameObject room0;
	public GameObject room1;
	public GameObject room4;
	public GameObject room3;
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
				grid[i,j] =0;
				//map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f*4,j*.32f*4,0),Quaternion.identity);
			}
		}
		int x = 0;
		int y = Random.Range (0,col);
		grid [x, y] = -1;
		CreatMap (x,y);
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++)
			{
				float thisX = transform.position.x + (i * tileWidth * tilesPerRoom);
				float thisY = transform.position.y + (j * tileHeight * tilesPerRoom);
				if(grid[i,j] !=0)
				{
					if(grid[i,j]==3){
						map[i,j] = (GameObject)Instantiate(room3,new Vector3(thisX,thisY,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					else if(grid[i,j]==2){
						map[i,j] = (GameObject)Instantiate(room1,new Vector3(thisX,thisY,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					else if(grid[i,j]==5){
						map[i,j] = (GameObject)Instantiate(room3,new Vector3(thisX,thisY,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					else if(grid[i,j]==4){
						map[i,j] = (GameObject)Instantiate(room4,new Vector3(thisX,thisY,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					else if(grid[i,j]==-1){
						map[i,j] = (GameObject)Instantiate(room1,new Vector3(thisX,thisY,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
					else{
						map[i,j] = (GameObject)Instantiate(room1,new Vector3(thisX,thisY,0),Quaternion.identity);
						map[i,j].transform.parent = transform;
					}
				}
				else if(grid[i,j]==0){
					map[i,j] = (GameObject)Instantiate(room0,new Vector3(thisX,thisY,0),Quaternion.identity);
					map[i,j].transform.parent = transform;
				}
			}
		}

		HandlePlayerSpawn ();
	}

	void HandlePlayerSpawn() {
		GameObject[] potentialSpawns = GameObject.FindGameObjectsWithTag ("Spawn");
		GameObject chosenSpawn = potentialSpawns[0];
		Vector3 spawn = chosenSpawn.transform.position;
		spawn.z = 0;
		GameObject.Find ("Player").transform.position = spawn;
	}

	void CreatMap(int x, int y){
		bool finished = false;
		while(!finished){
			int [] choise = {1,2,3};
			shuffleArray (choise);
			int test;
			for(int i=0;i<choise.Length;i++){
				switch(choise[i]){
				case 1:
					test = y+1;
					if(test>=col || grid[x,test]!=0){
						continue;
					}
					y=y+1;
					grid[x,y]=1;
					break;
				case 2:
					test = y-1;
					if(test<0 || grid[x,test]!=0){
						continue;
					}
					y=y-1;
					grid[x,y]=2;
					break;
				case 3:
					test = x+1;
					if(test>=row){
						finished = true;
						grid[x,y]=5;
						return;
					}
					else if(grid[test,y]!=0){
						continue;
					}
					grid[x,y] =4;
					x=x+1;
					grid[x,y]=3;
					break;
				}
			}
		}

	}


	void shuffleArray(int[] ar)
	{
		for (int i = ar.Length-1; i > 0; i--)
		{
			int index = Random.Range(0,i+1);
			int a = ar[index];
			ar[index] = ar[i];
			ar[i] = a;
		}
	}
	
	
}
