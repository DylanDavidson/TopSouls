using UnityEngine;
using System.Collections;

public class rangerController : Pathfinding2D {
	public string playerObject_Name;
	public int health;
	public Rigidbody2D rigidBody;
	public Transform playerTransform;
	public float speed = 2.5f;
	public bool active ;

	private RaycastHit2D cast;
	public GameObject Projectile;
	private GameObject toInstanciate;

	private Animator animator;
	public LayerMask myLayerMask;

	public int arrowCount = 1;
	//RaycastHit2D cast = new RaycastHit2D();

	
	
	
	int noMove = 0;
	Vector2 moveDirection;

	public void create(Vector2 myDirection)
	{

		Vector3 vectorToTarget = playerTransform.position - transform.position;
		vectorToTarget.Normalize ();
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x)) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);

		Vector2 displacedVector = new Vector2 (transform.position.x + 2f, transform.position.y + 2f);

		Vector2 origin = new Vector2 (transform.position.x + Mathf.Cos (angle * Mathf.Deg2Rad),
		                             transform.position.y +  Mathf.Sin (angle * Mathf.Deg2Rad));


		toInstanciate = Instantiate (Projectile, origin
		                             , q) as GameObject;

		arrowController myArrow = toInstanciate.GetComponent<arrowController> ();

		myArrow.direction = myDirection;
	}


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
		InvokeRepeating("updatePath", 0f, .5f);
		
	}
	
	// Update is called once per frame
	void Update () {

		
		//if (active) {
			//transform.LookAt (playerTransform.position);
			//transform.Rotate (new Vector3 (0, -90, 0), Space.Self);//correcting the original rotation

			//cast = Physics2D.Raycast (transform.position, -Vector2.right);
			//Debug.DrawRay(new Vector2(transform.position.x -.6f,transform.position.y) , -Vector2.right);


			if (Path.Count > 0) {
				Move (speed);

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
		Vector2 originVector = new Vector2 (transform.position.x + 2f, transform.position.y + 2f);
		float angle = (Mathf.Atan2(playerTransform.position.y, playerTransform.position.x));

		Vector2 origin = new Vector2 (originVector.magnitude * Mathf.Cos (angle), originVector.magnitude * Mathf.Sin (angle));

		cast = Physics2D.Raycast (new Vector2(transform.position.x,transform.position.y) , 
		                          new Vector2 (playerTransform.position.x - transform.position.x, 
		                             		   playerTransform.position.y - transform.position.y), Mathf.Infinity, 
		                          				~myLayerMask);
		
		Debug.Log (cast.transform.name);
		//if (animator.GetBool ("attack") )
		//	animator.SetBool ("attack", false);
	}
	

	void updatePath()
	{
		//cast.collider.CompareTag ("outer")
		float distanceToPlayer = Vector3.Distance (transform.position, playerTransform.position);
		if (distanceToPlayer <= 10f && distanceToPlayer >= 5f || 
		    (distanceToPlayer <= 10f && !cast.collider.CompareTag ("Player"))) {

			FindPath (transform.position, playerTransform.position);
			//animator.SetBool ("attack", false);
		} else if (cast.collider.CompareTag ("Player") && distanceToPlayer <= 5f && distanceToPlayer > 2f && arrowCount == 1) {
			//Debug.Log ("hello world");
			Path.Clear ();
			create (new Vector2 (playerTransform.position.x, 
			                     playerTransform.position.y ));
			arrowCount = 0;
		}
			//animator.SetBool ("attack", true);
		else if ( (cast.collider.CompareTag ("Player") && distanceToPlayer <= 5f && arrowCount == 0) || (distanceToPlayer <= 2f) ){
			NewTarget ();
			int choice = Random.Range (0, 3);
			if (choice == 0)
				arrowCount = 1;
			//animator.SetBool ("attack", false);
		}
		
	}
	


	void NewTarget(){
		int choice = Random.Range (0,20);
		//Path.Clear();

		switch (choice) {
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
		
		
		/*
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
			Vector2 ntarget = moveDirection * speed + ((Vector2) transform.position);
			FindPath (transform.position, ntarget);
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
		}*/
	}
}

