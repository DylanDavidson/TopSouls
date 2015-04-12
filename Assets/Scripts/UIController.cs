using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour {
	public Text health;
	public Text stamina;
	public Slider healthSlider;
	public Slider staminaSlider;
	public PlayerController playerController;

	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		health = GameObject.Find ("Health").GetComponent<Text> ();
		stamina = GameObject.Find ("Stamina").GetComponent<Text> ();
		healthSlider = GameObject.Find ("HealthSlider").GetComponent<Slider> ();
		staminaSlider = GameObject.Find ("StaminaSlider").GetComponent<Slider> ();
		health.text = "HEALTH: " + playerController.health;
		stamina.text = "STAMINA: " + playerController.stamina;
		healthSlider.value = playerController.health;
		staminaSlider.value = playerController.stamina;
	}
	
	// Update is called once per frame
	void Update () {
		health.text = "HEALTH: " + playerController.health;
		stamina.text = "STAMINA: " + playerController.stamina;
		healthSlider.value = playerController.health;
		staminaSlider.value = playerController.stamina;
	}
}
