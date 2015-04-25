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
		//	direction.Set (direction.x + newDir.x, direction.y + newDir.y);
		/*
		if (direction.x > 0 && direction.y > 0) {
			direction.x += newDir.x;
			direction.y +=newDir.y;
		}
		else if (direction.x > 0 && direction.y < 0) {
			direction.x +=newDir.x;
			direction.y -=newDir.y;
		}
		else if (direction.x < 0 && direction.y > 0) {
			direction.x -=newDir.x;
			direction.y +=newDir.y;
		}
		else if (direction.x < 0 && direction.y < 0) {
			direction.x -=newDir.x;
			direction.y -=newDir.y;
		}
*/
		//transform.position = Vector3.Lerp (transform.position, 
		//                                  new Vector3(direction.x, direction.y, 0), Time.deltaTime * projSpeed);
		//Debug.Log (direction.x + " " + direction.y + ".");




		/*
		if (pos.x < 0)
			pos.x -= Time.deltaTime * projSpeed;
		else if (pos.x > 0)
			pos.x += Time.deltaTime * projSpeed;
		if (pos.y > 0)
			pos.y += Time.deltaTime * projSpeed;
		else if (pos.y < 0)
			pos.y -= Time.deltaTime * projSpeed;
*/
		//this.transform.position = pos;

		if (timeSinceInitialization >= 1)
			Destroy (gameObject);

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//if (other.collider.CompareTag ("outer") || other.collider.CompareTag("Player"))
		   // Destroy (this);

		if (other.gameObject.tag == "Player" || other.gameObject.tag == "outer") {
			Destroy (gameObject);
		}
		//if (Time.realtimeSinceStartup > 


	}
}