using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;

public class BoardManager : MonoBehaviour {

	/*
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
*/
	public int columns = 8;
	public int rows = 8;

	//public Count wallCount = new Count (5,9);
	//public Count foodCount = new Count (1,5);
	public GameObject exit;
	public GameObject floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemytiles;
	public GameObject outerWallTiles;

	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();

	void InitializeList()
	{
		gridPositions.Clear ();

		for (int x = 1; x < columns - 1; x++)
		{
			for (int y = 1; y < rows - 1; y++)
			{
				gridPositions.Add (new Vector3(x,y,0f));
			}
		}
	}

	void BoardSetup()
	{

		//boardHolder = new GameObject ("Board").transform;

/*
		try {

			StreamReader inFile = new StreamReader ("infile.txt");
			int [,] matrix = new int[20,20];
			GameObject Instance; 
			Debug.Log ("we got to here");
			for (int y = 0; y < 9; y++) {
				for (int x = 0; x < 9; x++)
				{

					String mychar = inFile.Read ().ToString();
					Debug.Log ("got string");
					if (mychar.Equals("x"))
					{
						Instance = outerWallTiles;
					}
					else
						Instance = floorTiles;

					GameObject myinstance = Instantiate (Instance, new Vector3(x,y,0f), Quaternion.identity) as GameObject;

				}
			}
		}
		catch (Exception e)
		{
			Debug.Log("oops");
		}

*/

		for (int x = -6; x < 16; x++)
		{
			for (int y = -2; y < 12; y++)
			{
				GameObject toInstantiate = floorTiles;
				if (x == -6 || x == 15 || y == -2 || y == 11)
					toInstantiate = outerWallTiles;
				GameObject instance = Instantiate (toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;

				//instance.transform.SetParent (boardHolder);
			}

}
	}


	/*
	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions (randomIndex);
		gridPositions.RemoveAt (randomIndex);

		return randomPosition;

	}

	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);

		for (int i = 0; i < objectCount; i++) 
		{
			Vector3 randomPosition = RandomPosition ();
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}
*/
	public void setupScene(int level)
	{
		BoardSetup ();
		InitializeList();
		//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		//int enemyCount = (int)Mathf.Log (level, 2f);
		//LayoutObjectAtRandom (enemytiles, enemyCount, enemyCount);
		//Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);

	}


}
