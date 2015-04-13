using UnityEngine;
using System.Collections;

public class Prefab : MonoBehaviour
{
	public static Prefab instance;

	public GameObject enemy;

	public static GameObject enemyPrefab {
		get {
			return instance.enemy;
		}
	}

	void Awake() {
		instance = this;
	}
}

