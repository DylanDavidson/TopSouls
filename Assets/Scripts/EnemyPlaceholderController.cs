using UnityEngine;
using System.Collections;

public class EnemyPlaceholderController : Pathfinding2D {
	public string playerObject_Name;
	public int health;
	public Rigidbody2D rigidBody;
	public Transform playerTransform;
	public float speed = 2.5f;
	public bool active = false;


	int noMove = 0;
	Vector2 moveDirection;

	public int GetHealth()
	{
		return health;
	}
	
	public void GetPushed(Vector2 forceVector, bool blocking)
	{
		if(blocking)
			rigidBody.AddForce (forceVector/2, ForceMode2D.Impulse);
		else
			rigidBody.AddForce (forceVector, ForceMode2D.Impulse);
	}

	public void TakeDamage(int damage, bool blocking)
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
		playerTransform = GameObject.Find ("Player").transform;
		InvokeRepeating("updatePath", 0f, 1f);

	}
	
	// Update is called once per frame
	void Update () {
		//rotate to look at the player

		//if (active) {
			transform.LookAt (playerTransform.position);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);//correcting the original rotation
			//move towards the player
			//if (Vector3.Distance (transform.position, playerTransform.position) > 1f) {//move if distance from target is greater than 1
					//transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			//}
		//}

		if (Path.Count > 0) {
			Move();
		}
		else{
			Vector2 ntarget = moveDirection * speed + ((Vector2) transform.position);
			transform.position = Vector2.Lerp (transform.position, ntarget, Time.deltaTime);
		}
	}


	void updatePath()
	{
		if (Vector3.Distance (transform.position, playerTransform.position) <= 5f)
			FindPath (transform.position, playerTransform.position);
		else
			NewTarget ();
		
	}
	
	void NewTarget(){
		int choice = Random.Range (0,3);
		
		
		// if enemy remains stationary for too long, force a new movedirection
		if (noMove >= 2) {
			choice = 0;
			noMove = 0;
		}
		
		// randomly choose new direction, stay the same direction, or stand still
		switch(choice){
		case 0:
			moveDirection = Random.insideUnitCircle;
			moveDirection.Normalize ();
			noMove = 0;
			break;
		case 1:
			//change = false;
			if (moveDirection.magnitude == 0)
				noMove++;
			break;
		case 2:
			moveDirection.Set (0f,0f);
			noMove++;
			break;
		}
	}
}
