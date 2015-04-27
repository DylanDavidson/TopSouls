using UnityEngine;
using System.Collections;

public class arrowController : WeaponController {

	public float projSpeed = 5f;
	public Vector2 direction;

	private Vector2 pos;
	private float initializationTime;
	Vector2 newDir;

	// Use this for initialization
	void Start () {
		pos = direction;
		initializationTime = Time.timeSinceLevelLoad;
		newDir = new Vector2 (direction.x - transform.position.x , direction.y - transform.position.y  );
		newDir.Normalize ();
	
	}


	
	// Update is called once per frame
	void Update () {
		float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;

		transform.position = Vector3.MoveTowards (transform.position, direction, Time.deltaTime * 7f);

		if ((Vector2)transform.position == direction)
			direction.Set(direction.x + newDir.x, direction.y + newDir.y);

		if (timeSinceInitialization >= 1)
			Destroy (gameObject);

	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if((other.CompareTag("Shield") && !other.CompareTag("Player")) || 
		   (!other.CompareTag("Shield") && other.CompareTag("Player")) || 
		   (other.CompareTag("Enemy") && !this.CompareTag("EnemyWeapon")) ||
		   (other.CompareTag("runner") && !this.CompareTag("EnemyWeapon"))||
		   (other.CompareTag("boss") && !this.CompareTag("EnemyWeapon"))  ||
		   (other.CompareTag("ranger") && !this.CompareTag("EnemyWeapon"))) 
		{
			if(Time.time-lastCollision >=  collisionCooldown)
			{
				lastCollision = Time.time;
				DoDamage (other);
				Push (other);
			}
		}

		if (other.CompareTag ("Wall") || other.CompareTag ("outer") || other.CompareTag ("Player"))
			Destroy (gameObject);
	}
}