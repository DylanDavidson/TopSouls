using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exit : MonoBehaviour {
	public Image fade;  
	public float fadeSpeed;
	public Color fadeColour = Color.clear;
	public bool in_Exit;

	void Start() {
		fade = Image.FindObjectOfType<Image> ();
		fade.color = fadeColour;
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Player") {
			Application.LoadLevel ("boss_scene");
			in_Exit =true;
		}
	}
}
