using UnityEngine;
using System.Collections;

public class EnemyPlaceholderController : MonoBehaviour {
	public string playerObject_Name;
	public int health;
	public Rigidbody2D rigidBody;
	public Transform playerTransform;
	public float speed = 2.5f;
	public bool active = false;

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

	}
	
	// Update is called once per frame
	void Update () {
		//rotate to look at the player

		if (active) {
			transform.LookAt (playerTransform.position);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);//correcting the original rotation
			//move towards the player
			//if (Vector3.Distance (transform.position, playerTransform.position) > 1f) {//move if distance from target is greater than 1
					transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			//}
		}
	}
		
}
