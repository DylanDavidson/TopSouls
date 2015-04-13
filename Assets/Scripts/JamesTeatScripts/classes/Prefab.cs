using UnityEngine;
using System.Collections;

public class Prefab : MonoBehaviour
{
	public static Prefab instance;

	public GameObject enemy_obj;
	public GameObject player_spawn_obj;
	public GameObject enemy_spawn_obj;

	
	public static GameObject enemy_spawn {
		get {
			return instance.enemy_spawn_obj;
		}
	}

	public static GameObject enemy {
		get {
			return instance.enemy_obj;
		}
	}

	public static GameObject player_spawn {
		get {
			return instance.player_spawn_obj;
		}
	}

	void Awake() {
		instance = this;
	}
}

