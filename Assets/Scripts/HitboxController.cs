using UnityEngine;
using System.Collections;

public class HitboxController : MonoBehaviour {

	public int damage;
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

		if((other.CompareTag("Player") && !this.CompareTag("Player") || 
		   (other.CompareTag("Enemy") && !this.CompareTag("Enemy"))))
			other.GetComponent<Rigidbody2D>().AddForce (dir*force, ForceMode2D.Impulse);
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
