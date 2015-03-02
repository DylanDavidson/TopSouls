using UnityEngine;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

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
		int y = Random.Range (0,4);
		grid [0, 0] = 1;
		CreatMap ();
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

					else{
						map[i,j] = (GameObject)Instantiate(prefab,new Vector3(i*.32f,j*.32f,0),Quaternion.identity);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CreatMap(){
		bool finished = false;
		int x=0;
		int y=0;
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


}
