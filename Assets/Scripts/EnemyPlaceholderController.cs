using UnityEngine;
using System.Collections;

public class EnemyPlaceholderController : MonoBehaviour {
	public int health;

	public int GetHealth()
	{
		return health;
	}

	public void TakeDamage(int damage)
	{
		if (health - damage > 0)
			health -= damage;
		else
			Die();
	}

	public void Die()
	{
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
