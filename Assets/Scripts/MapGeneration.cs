using UnityEngine;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {
	int x2;
	int y2;
	public GameObject prefab;
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
				if(grid[i,j] !=0)
				{
					if(grid[i,j]==3){
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
						SpriteRenderer sr = map[i,j].GetComponent<SpriteRenderer>();
						sr.color = Color.black;
					}
					if(grid[i,j]==2){
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
						SpriteRenderer sr = map[i,j].GetComponent<SpriteRenderer>();
						sr.color = Color.red;
					}
					if(grid[i,j]==5){
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
						SpriteRenderer sr = map[i,j].GetComponent<SpriteRenderer>();
						sr.color = Color.blue;
					}
					if(grid[i,j]==4){
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
						SpriteRenderer sr = map[i,j].GetComponent<SpriteRenderer>();
						sr.color = Color.gray;
					}
					if(grid[i,j]==-1){
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
						SpriteRenderer sr = map[i,j].GetComponent<SpriteRenderer>();
						sr.color = Color.cyan;
					}
					else{
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
					}
				}
				if(grid[i,j]==0){
					map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
					SpriteRenderer sr = map[i,j].GetComponent<SpriteRenderer>();
					sr.color = Color.magenta;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

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
	

	// A dice rool that gives a number between 1-5.
	int DicRoll(){
		return (int)Random.Range (1, 5);
	}


	/*void CreatMap2(int x, int y){
		bool finished = false;
		while(!finished){
			int [] choise = {1,2,3,1,2,1,2};
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
					test = x-1;
					if(test<0){
						finished = true;
						grid[x,y]=5;
						return;
					}
					else if(grid[test,y]!=0){
						continue;
					}
					x=x-1;
					grid[x,y]=3;
					break;
				}
			}
		}
		
	}*/
}
