using UnityEngine;
using System.Collections;

public class runnerController : Pathfinding2D {
	public string playerObject_Name;
	public int health;
	public Rigidbody2D rigidBody;
	public Transform playerTransform;
	public float speed;
	public bool active = false;
	
	private Animator animator; 
	bool atCorner;
	Vector3 playerDir;

	int noMove = 0;
	Vector2 moveDirection;
	float defaultSpeed;

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
		defaultSpeed = speed;
		playerTransform = GameObject.Find ("Player").transform;
		animator = GetComponent<Animator> ();
		animator.SetBool ("walking", false);

		InvokeRepeating("actionListener", 0f, .1f);
	}

	void Update () 
	{
		// If actionListener calls A*, move according to function in PathFinding2D script
		// *note* the enemy's speed will be specified in the inherited Pathfinding2D move function
		if (Path.Count > 0) {
			Move(speed);

			// Set direction animation based on first node location
			Vector3 directionVec = Path[0] - transform.position;
			directionVec.Normalize();
			
			//Debug.Log ("x: " + directionVec.x + " y: " + directionVec.y);
			animator.SetBool ("walking", true);
			animator.SetBool ("attack", false);


			if (directionVec.x >= .1f && directionVec.y <= .1f && directionVec.y >= -.1f)
				animator.SetInteger ("direction", 2);

			if (directionVec.x <= -.1f && directionVec.y <= .1f && directionVec.y >= -.1f)
				animator.SetInteger ("direction", 4);

			if (directionVec.y >= .1f && directionVec.x <= .1f && directionVec.x >= -.1f)
				animator.SetInteger ("direction", 3);
			if (directionVec.y <= -.1f && directionVec.x <= .1f && directionVec.x >= -.1f)
				animator.SetInteger ("direction", 1);
		}
	}

	void FixedUpdate()
	{
		 playerDir = (playerTransform.position - transform.position).normalized;

		// Check whether the player exists at the enemy's corner
		if ((playerDir.x >= .5f && playerDir.y >= .5f) || (playerDir.x >= .5f && playerDir.y <= -.5f) || (playerDir.x <= -.5f && playerDir.y >= .5f) || 
		    (playerDir.x <= -.5f && playerDir.y <= -.5f))
			atCorner = true;
		else
			atCorner = false;
	}

	void actionListener()
	{
		float distanceToPlayer = Vector3.Distance (transform.position, playerTransform.position);
		
		// if the player is within a set range, find a path to the player
		if ((distanceToPlayer <= 7f)) 
		{
			FindPath (transform.position, playerTransform.position);
			speed = defaultSpeed;
		} 

		else if ((distanceToPlayer <= 4f && distanceToPlayer >= 3f)) 
		{
			FindPath (transform.position, playerTransform.position);
			speed = defaultSpeed + 2;
		} 

		else if ((distanceToPlayer <= 2f && distanceToPlayer >= 1f)) 
		{
			FindPath (transform.position, playerTransform.position);
			speed = defaultSpeed + 4;
		} 

		else
			NewTarget ();	
	}


	
	void NewTarget()
	{
		int choice = Random.Range (0,20);
		
		switch (choice) 
		{
		case 0:
			FindPath (transform.position, new Vector3 (transform.position.x + 1, transform.position.y, 0f));
			break;
		case 1:
			FindPath (transform.position, new Vector3 (transform.position.x - 1, transform.position.y, 0f));
			break;
		case 2:
			FindPath (transform.position, new Vector3 (transform.position.x, transform.position.y + 1, 0f));
			break;
		case 3:
			FindPath (transform.position, new Vector3 (transform.position.x + 1, transform.position.y - 1, 0f));
			break;
		}
	}
}
