using UnityEngine;
using System.Collections;

public  class NumberGenerator : MonoBehaviour {

	private int x;
	private int y;
	// Use this for initialization
	void Start () {
		x = Random.Range(3,31);
		y = Random.Range(3,31);

		Debug.Log (x + " " + y);
	}

	public int getX(){return x;}
	public int getY(){return y;}

}
