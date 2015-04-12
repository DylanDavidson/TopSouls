using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fade : MonoBehaviour {
	public Image damageImage;  
	public float flashSpeed;
	public Color flashColour = Color.black; 

	void Start(){
		damageImage.color = flashColour;
	}
	// Update is called once per frame
	void Update () {
		damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
	}
}
