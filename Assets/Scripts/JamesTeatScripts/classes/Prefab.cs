using UnityEngine;
using System.Collections;

public class Prefab : MonoBehaviour
{
	public static Prefab instance;

	public GameObject enemy_obj;
	public GameObject player_spawn_obj;
	public GameObject enemy_spawn_obj;
	public GameObject floor_obj;
	public GameObject wall_obj;
	public GameObject door_obj;
	public GameObject exit_obj;
	public GameObject speed_powerup_obj;
	public GameObject health_powerup_obj;
	public GameObject archer_obj;

	public static GameObject floor {
		get {
			return instance.floor_obj;
		}
	}

	public static GameObject speed_powerup {
		get {
			return instance.speed_powerup_obj;
		}
	}

	public static GameObject health_powerup {
		get {
			return instance.health_powerup_obj;
		}
	}

	public static GameObject wall {
		get {
			return instance.wall_obj;
		}
	}

	public static GameObject door {
		get {
			return instance.door_obj;
		}
	}
	
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

	public static GameObject archer {
		get {
			return instance.archer_obj;
		}
	}

	public static GameObject player_spawn {
		get {
			return instance.player_spawn_obj;
		}
	}

	public static GameObject exit {
		get {
			return instance.exit_obj;
		}
	}

	void Awake() {
		instance = this;
	}
}

