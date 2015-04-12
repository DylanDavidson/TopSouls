using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exit : MonoBehaviour {


	public Image fade;  
	public float fadeSpeed;
	public Color fadeColour = Color.clear;
	private bool in_Exit = false;


	void Start(){
		fade = Image.FindObjectOfType<Image> ();
		fade.color = fadeColour;
	}


	void Update(){
	}


	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Player") {
			Application.LoadLevel ("boss_scene");
			in_Exit =true;
		}
	}

	void OnTriggerExit2D(Collider2D target){
		Debug.Log("You left the exit");
	}

}
