using UnityEngine;
using System.Collections;

public class NumGen{
	public static NumGen instance = new NumGen();
	public static int x = Random.Range(3,32);
	public static int y = Random.Range(3,32);
	

	public static NumGen getInatance(){return instance;}
	public int getX(){ return x;}
	public int getY(){return y;}
}
