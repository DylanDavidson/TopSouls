using UnityEngine;
using System.Collections;

public class VictoryController : MonoBehaviour {

	IEnumerator Restart()
	{
		yield return new WaitForSeconds (8);
		if(Input.anyKey)
			Application.LoadLevel ("final_scene");
	}

	// Update is called once per frame
	void Update () 
	{
		StartCoroutine(Restart ());
	}
}

