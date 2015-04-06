using UnityEngine;
using System.Collections;

public class Dice {
	public static Dice instance = new Dice();
	private static int maxVal = 15;
	private int val = Random.Range(0,maxVal+1);
	
	
	public static Dice getInatance(){return instance;}
	public void roll(){val = Random.Range (0, maxVal+1);}
	public int getVal(){ return val;}
	public int getMaxVal(){ return maxVal;}
}
