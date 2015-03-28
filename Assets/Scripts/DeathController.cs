using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour {

	IEnumerator Restart()
	{
		yield return new WaitForSeconds (3);
		if(Input.anyKey)
			Application.LoadLevel ("final_scene1");
	}

	// Update is called once per frame
	void Update () 
	{
		StartCoroutine(Restart ());
	}
}
