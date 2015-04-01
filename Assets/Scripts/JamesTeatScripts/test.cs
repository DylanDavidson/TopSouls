using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public GameObject bob;
	// Use this for initialization
	void Start () {
		NumberGenerator ng = bob.GetComponent<NumberGenerator> ();
		Debug.Log ("test " +ng.getX());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
