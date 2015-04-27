using UnityEngine;
using System.Collections;

public class bossController : Pathfinding2D {
	public string playerObject_Name;
	public int health;
	public Rigidbody2D rigidBody;
	public Transform playerTransform;
	public float speed = 2.5f;
	public bool active = false;

	public AudioClip hurtSound;

	private AudioSource source;

	private Animator animator; 
	bool atCorner;
	Vector3 playerDir;

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
		source.PlayOneShot (hurtSound);

		if (health - damage > 0)
			health -= damage;
		else
			Die();
	}

	public void Die()
	{
		foreach(GameObject o in GameObject.FindObjectsOfType<GameObject>())
			Destroy(o);
		Application.LoadLevel ("victory_scene");
	}

	// Use this for initialization
	void Start () 
	{
		source = GetComponent<AudioSource> ();
		playerTransform = GameObject.Find ("Player").transform;
		animator = GetComponent<Animator> ();
		animator.SetBool ("walking", false);

		InvokeRepeating("actionListener", 0f, .1f);

	}

	void Update () 
	{
		if (Path.Count > 0) 
		{
			Move(speed);

			// Set direction animation based on first node location
			Vector3 directionVec = Path[0] - transform.position;
			directionVec.Normalize();
			
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
		if ( (distanceToPlayer <= 14f && distanceToPlayer >= 4.5f)  || (atCorner && distanceToPlayer <= 14f) ) {
			FindPath (transform.position, playerTransform.position);

		// if the player is within attacking range and is not in a corner: set attack based on direction
		} 
		else if (distanceToPlayer < 4.5f && !atCorner) {

			Path.Clear ();
			// right
			if (playerDir.x >= .1f)
			{
			//animator.SetBool ("walking", false);
				animator.SetInteger ("direction", 2);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			// up
			else if (playerDir.y >= .1f)
			{
				animator.SetInteger ("direction", 3);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			// left
			else if (playerDir.x <= -.1f)
			{
				animator.SetInteger ("direction", 4);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			// down
			else if (playerDir.y <= -.1f)
			{
				animator.SetInteger ("direction", 1);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
		}

		// if the player is not within range, have the enemy wander
		else {
			NewTarget ();
		}
		
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
