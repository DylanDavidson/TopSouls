using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour {
	public Text playerHealthDebug;
	public Text playerStaminaDebug;
	public Slider playerHealth;
	public Slider playerStamina;
	public PlayerController playerController;

	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		playerHealthDebug = GameObject.Find ("PlayerHealthDebug").GetComponent<Text> ();
		playerStaminaDebug = GameObject.Find ("PlayerStaminaDebug").GetComponent<Text> ();
		playerHealth = GameObject.Find ("PlayerHealth").GetComponent<Slider> ();
		playerStamina = GameObject.Find ("PlayerStamina").GetComponent<Slider> ();
		playerHealthDebug.text = "HEALTH: " + playerController.health;
		playerStaminaDebug.text = "STAMINA: " + playerController.stamina;
		playerHealth.value = playerController.health;
		playerStamina.value = playerController.stamina;
	}
	
	// Update is called once per frame
	void Update () {
		playerHealthDebug.text = "HEALTH: " + playerController.health;
		playerStaminaDebug.text = "STAMINA: " + playerController.stamina;
		playerHealth.value = playerController.health;
		playerStamina.value = playerController.stamina;
	}
}
