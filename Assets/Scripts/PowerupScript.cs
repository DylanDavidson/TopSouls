using UnityEngine;
using System.Collections;
using System.Reflection;

public class PowerupScript : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}

	void SpeedPowerup() {
		player.SendMessage("SpeedUp");
	}

	void HealthPowerup() {
		player.SendMessage("HealthUp");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name != "Player")
			return;
		Debug.Log (other.gameObject.name);
		// Calls Powerup method based on object hit
		SendMessage(name.Replace("(Clone)", "") + "Powerup");
		Destroy(gameObject);
	}
}
