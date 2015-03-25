using UnityEngine;
using System.Collections;

public class HitboxController : MonoBehaviour {

	public int damage;
	public int knockbackVel;
	public int force;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		DoDamage (other);
		Push (other);
	}

	void Push(Collider2D other)
	{
		Vector2 dir = (other.transform.position - transform.parent.position).normalized;
	
		Debug.DrawRay (other.transform.position, dir);

		if(other.CompareTag("Enemy") && !this.CompareTag("Enemy"))
			other.GetComponent<EnemyPlaceholderController>().GetPushed(dir*force);
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().GetPushed(dir*force);
	}
	
	void DoDamage(Collider2D other)
	{
		if(other.CompareTag("Enemy"))
			other.GetComponent<EnemyPlaceholderController>().TakeDamage(damage);
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().TakeDamage(damage);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
