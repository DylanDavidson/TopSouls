using UnityEngine;
using System.Collections;

public class EnemyPlaceholderController : MonoBehaviour {
	public int health;
	public Rigidbody2D rigidbody;

	public int GetHealth()
	{
		return health;
	}
		
	public void GetPushed(Vector2 forceVector)
	{
		this.transform.position = forceVector;
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
