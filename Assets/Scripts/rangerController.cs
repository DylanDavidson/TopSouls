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

	//RaycastHit2D cast = new RaycastHit2D();

	
	
	
	int noMove = 0;
	Vector2 moveDirection;

	public void create(Vector2 myDirection)
	{

		Vector3 vectorToTarget = playerTransform.position - transform.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x)) * Mathf.Rad2Deg + 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);



		toInstanciate = Instantiate (Projectile, transform.position
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
			transform.LookAt (playerTransform.position);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);//correcting the original rotation

			//cast = Physics2D.Raycast (transform.position, -Vector2.right);
			Debug.DrawRay(new Vector2(transform.position.x -.6f,transform.position.y) , -Vector2.right);


			if (Path.Count > 0) {
				Move ();
			} else {
				//Vector2 ntarget = moveDirection * speed + ((Vector2)transform.position);
				//transform.position = Vector2.Lerp (transform.position, ntarget, Time.deltaTime);
			}

		//}
	}

	void FixedUpdate()
	{
		cast = Physics2D.Raycast (new Vector2(transform.position.x,transform.position.y) , 
		                          new Vector2 (playerTransform.position.x - transform.position.x, 
		                             		   playerTransform.position.y - transform.position.y), Mathf.Infinity, 
		                          				LayerMask.NameToLayer("Enemies"));
		
		//if (cast.collider.CompareTag ("Player"))
		//	Debug.Log ("hello");
		//if (animator.GetBool ("attack") )
		//	animator.SetBool ("attack", false);
	}
	

	void updatePath()
	{
		//cast.collider.CompareTag ("outer")
		float distanceToPlayer = Vector3.Distance (transform.position, playerTransform.position);
		if (distanceToPlayer <= 10f && distanceToPlayer >= 5f || 
		    (distanceToPlayer <= 10f && cast.collider.CompareTag ("outer"))) {

			FindPath (transform.position, playerTransform.position);
			//animator.SetBool ("attack", false);
		} else if (cast.collider.CompareTag ("Player") && distanceToPlayer <= 5f) {
			//Debug.Log ("hello world");
			Path.Clear ();
			create (new Vector2 (playerTransform.position.x, 
			                     playerTransform.position.y ));
		}
			//animator.SetBool ("attack", true);
		else {
		//	NewTarget ();
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

