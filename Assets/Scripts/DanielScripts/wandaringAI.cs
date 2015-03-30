using UnityEngine;
using System.Collections;

public class wandaringAI : MonoBehaviour {

	bool change;
 	int noMove = 0;
	bool buttonPress = false;
	Vector2 ntarget;

	RaycastHit2D leftRay;
	RaycastHit2D rightRay;
	float distanceLeft;
	float distanceRight;

	Vector2 leftVector;
	Vector2 rightVector;

	//GameObject outwall = GameObject.Find("outerwall");




	private Vector2 moveDirection;

	public float moveSpeed;
	public int turnSpeed = 5;
	void Start () {

		// Get initial direction
		GetTarget();
		// Get a new target direction every 2 seconds
		InvokeRepeating ("NewTarget",2.0f,2.0f);
	}
	void Update () {

		Vector2 currentPos = transform.position;

		//if(change)
			//moveDirection = GetTarget ();
		//if( Mathf.Abs (Vector3.Distance(transform.position,target)) > range) {

		rightVector = new Vector2((transform.up.x * Mathf.Cos((-45 - 90) * Mathf.Deg2Rad)) -
		                          (transform.up.y * Mathf.Sin((-45 - 90) * Mathf.Deg2Rad)),
		                          (transform.up.x * Mathf.Sin((-45 - 90) * Mathf.Deg2Rad)) +
		                          (transform.up.y * Mathf.Cos((-45 - 90) * Mathf.Deg2Rad)));
		leftVector = new Vector2((transform.up.x * Mathf.Cos((-45) * Mathf.Deg2Rad)) -
		                         (transform.up.y * Mathf.Sin((-45) * Mathf.Deg2Rad)),
		                         (transform.up.x * Mathf.Sin((-45) * Mathf.Deg2Rad)) +
		                         (transform.up.y * Mathf.Cos((-45) * Mathf.Deg2Rad)));


		// Mouseclick1
		if (Input.GetButton("Fire1"))
		 {
			buttonPress = true;
			Vector2 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			moveDirection = moveToward - currentPos;
			//moveDirection.z = 0;
			moveDirection.Normalize();
		}

		leftRay = Physics2D.Raycast(currentPos, leftVector, 5);
		rightRay = Physics2D.Raycast(currentPos, rightVector, 5);

		distanceRight = Vector2.Distance (rightRay.point, currentPos); 
		distanceRight = Vector2.Distance (leftRay.point, currentPos); 
		//Debug.DrawLine (currentPos, transform.up *2, Color.red);
		Debug.DrawRay (transform.position, 2 * rightVector, Color.red, 0f, false);

		Debug.DrawRay (transform.position, 2 * leftVector, Color.red, 0f, false);

		//Debug.DrawRay (transform.position, transform.up * 5, Color.red);

		/*
		if (leftRay.collider != null)
			Debug.Log ("leftray hit!");

		if (rightRay.collider != null)
			Debug.Log ("rightray hit!");

*/

		// calculate new target destination and lerp
		ntarget = moveDirection * moveSpeed + currentPos;
		transform.position = Vector2.Lerp (currentPos, ntarget, Time.deltaTime);


		// set new angle based on direction of travel
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

		if (moveDirection.magnitude != 0)
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
			                 Quaternion.Euler( 0, 0, targetAngle ), 
			                 turnSpeed * Time.deltaTime );
			
	}

	void GetTarget(){

		moveDirection = Random.insideUnitCircle;
		moveDirection.Normalize ();
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
			change = false;
			if (moveDirection.magnitude == 0)
				noMove++;
			break;
		case 2:
			moveDirection.Set (0f,0f);
			noMove++;
			break;
		}
	}



	void OnCollisionEnter2D(Collision2D col)
	{

		// TODO: add feelers to check for walls. uncomment math below when ready


		// if enemy collides with wall, rotate 45 degrees
		if (col.gameObject.tag  == ("outer")) {

			// move counter-clockwise
			//if ( (moveDirection.x > 0 && moveDirection.y < 0) || (moveDirection.x < 0 && moveDirection.y > 0) )
			moveDirection.Set (moveDirection.x*Mathf.Cos (45*Mathf.Deg2Rad) - moveDirection.y*Mathf.Sin (45*Mathf.Deg2Rad),
			                   moveDirection.x*Mathf.Sin (45*Mathf.Deg2Rad) + moveDirection.y*Mathf.Cos (45*Mathf.Deg2Rad) );
			// move clockwise
			//else if ( (moveDirection.x > 0 && moveDirection.y > 0) || (moveDirection.x < 0 && moveDirection.y < 0) )
			//	moveDirection.Set (moveDirection.x*Mathf.Cos (-45*Mathf.Deg2Rad) - moveDirection.y*Mathf.Sin (-45*Mathf.Deg2Rad),
			//	                   moveDirection.x*Mathf.Sin (-45*Mathf.Deg2Rad) + moveDirection.y*Mathf.Cos (-45*Mathf.Deg2Rad) );
			// reverse direction
			//else
			//	moveDirection.Set(-moveDirection.x, -moveDirection.y);
		}
	}


	

}
