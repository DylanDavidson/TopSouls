using UnityEngine;
using System.Collections;

public class arrowController : WeaponController {

	public int projSpeed = 3;
	public Vector2 direction;

	private Vector2 pos;
	private float initializationTime;


	// Use this for initialization
	void Start () {
		pos = direction;
		initializationTime = Time.timeSinceLevelLoad;



	}
	
	// Update is called once per frame
	void Update () {
		float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;

		transform.position += new Vector3(direction.x, direction.y, 0f) * Time.deltaTime;
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
			Debug.Log ("we did it");
			Destroy (gameObject);
		}
		//if (Time.realtimeSinceStartup > 


	}
}