using UnityEngine;
using System.Collections;

public class EnemyPlaceholderController : Pathfinding2D {
	public string playerObject_Name;
	public int health;
	public Rigidbody2D rigidBody;
	public Transform playerTransform;
	public float speed = 2.5f;
	public bool active = false;

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
		animator = GetComponent<Animator> ();
		animator.SetBool ("walking", false);

		InvokeRepeating("actionListener", 0f, .1f);

	}

	void Update () {
		//rotate to look at the player

		//if (active) {
			//transform.LookAt (playerTransform.position);
			//transform.Rotate (new Vector3 (0, -90, 0), Space.Self);//correcting the original rotation
			//move towards the player
			//if (Vector3.Distance (transform.position, playerTransform.position) > 1f) {//move if distance from target is greater than 1
					//transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			//}
		//}

		/*
		Vector3 vectorToTarget = playerTransform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
*/

		//animator.SetBool ("walking", false);

		// If actionListener calls A*, move according to function in PathFinding2D script
		// *note* the enemy's speed will be specified in the inherited Pathfinding2D move function
		if (Path.Count > 0) {



			Move();

			// Set direction animation based on first node location
			Vector3 directionVec = Path[0] - transform.position;
			directionVec.Normalize();
			
			Debug.Log ("x: " + directionVec.x + " y: " + directionVec.y);
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
				
			/*

			if ((directionVec.x > 0f && directionVec.y <= .1f ))
				animator.SetInteger ("direction", 2);
			
			if (directionVec.x < 0f && directionVec.y <= .1f )
				animator.SetInteger ("direction", 4);
			
			if (directionVec.y > 0f && directionVec.x <= .1f)
				animator.SetInteger ("direction", 3);
			if (directionVec.y < 0f && directionVec.x <= .1f)
				animator.SetInteger ("direction", 1);
				*/
		}
		// else
		//else{
		//	Vector2 ntarget = moveDirection * speed + ((Vector2) transform.position);
		//	transform.position = Vector2.Lerp (transform.position, ntarget, Time.deltaTime);
		//}
	}

	void FixedUpdate()
	{
		//if (animator.GetBool ("attack"))
		//	animator.SetBool ("attack", false);
		
		 playerDir = (playerTransform.position - transform.position).normalized;

		// Check whether the player exists at the enemy's corner
		if ((playerDir.x >= .5f && playerDir.y >= .5f) || (playerDir.x >= .5f && playerDir.y <= -.5f) || (playerDir.x <= -.5f && playerDir.y >= .5f) || 
		    (playerDir.x <= -.5f && playerDir.y <= -.5f))
			atCorner = true;
		else
			atCorner = false;
		
		//animator.SetInteger ("direction", 1);
	}

	void actionListener()
	{
		float distanceToPlayer = Vector3.Distance (transform.position, playerTransform.position);


		// if the player is within a set range, find a path to the player
		if ( (distanceToPlayer <= 7f && distanceToPlayer >= 1.5f) || (atCorner && distanceToPlayer <= 7f) ) {
			FindPath (transform.position, playerTransform.position);
			//animator.SetBool ("attack", false);

		// if the player is within attacking range and is not in a corner: set attack based on direction
		} else if (distanceToPlayer < 1.5f && !atCorner) {

			Path.Clear ();
			// right
			if (playerDir.x >= .5f)
			{
			//animator.SetBool ("walking", false);
				animator.SetInteger ("direction", 2);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			// up
			else if (playerDir.y >= .5f)
			{
				animator.SetInteger ("direction", 3);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			// left
			else if (playerDir.x <= -.5f)
			{
				animator.SetInteger ("direction", 4);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			// down
			else if (playerDir.y <= -.5f)
			{
				animator.SetInteger ("direction", 1);
				animator.SetBool ("attack", true);
				animator.SetBool ("walking", false);
			}
			//animator.SetInteger ("direction", 4);
		}

		// if the player is not within range, have the enemy wander
		else {
			NewTarget ();
			//animator.SetBool ("attack", false);
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
