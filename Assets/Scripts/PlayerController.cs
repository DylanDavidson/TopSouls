using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	Animator animator;
	public float speed;
	private bool moved;
	public int health;
	public Rigidbody2D rigidBody;
	public float force = 2f;

	public int GetHealth()
	{
		return health;
	}

	public void GetPushed(Vector2 forceVector)
	{
		rigidBody.AddForce (forceVector, ForceMode2D.Impulse);
		//this.transform.position = forceVector;
	}

	public void TakeDamage(int damage)
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

	void Attack()
	{

	}

	void Move(string dir)
	{
		animator.SetBool ("moving", true);
		Vector2 position = this.transform.position;

		switch(dir)
		{
		case "DOWN": 
			position.y -= speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", 1);
			break;
		case "RIGHT":
			position.x += speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", 2);
			break;
		case "UP":
			position.y += speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", 3);
			break;
		case "LEFT":
			position.x -= speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", 4);
			break;
		}

		moved = true;
	}

	void Dodge(string dir)
	{
		switch(dir)
		{
		case "DOWN":
			rigidBody.AddForce (new Vector2(0, -1).normalized * force, ForceMode2D.Impulse);
			break;
		case "RIGHT":
			rigidBody.AddForce (new Vector2(1, 0).normalized * force, ForceMode2D.Impulse);
			break;
		case "UP":
			rigidBody.AddForce (new Vector2(0, 1).normalized * force, ForceMode2D.Impulse);
			break;
		case "LEFT":
			rigidBody.AddForce (new Vector2(-1, 0).normalized * force, ForceMode2D.Impulse);
			break;
		// TODO: Save last direction pressed and base backward dodge direction off that
		//case "BACK":
			//rigidBody.AddForce (new Vector2(1, 0).normalized * 10, ForceMode2D.Impulse);
			//break;
		}
	}

	void Start() {
		animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		int att = animator.GetInteger ("attacking");
		animator.SetBool ("moving", false);
		animator.SetInteger("direction", 0);
		if (att > 0)
			animator.SetInteger ("attacking", att - 1);
		else
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				if (animator.GetInteger ("attacking") == 0) 
							animator.SetInteger ("attacking", 15);
			}
			//if (Input.GetKeyDown (KeyCode.Space))
			//	Dodge("BACK");
			if (Input.GetKeyDown (KeyCode.Space) && Input.GetKey (KeyCode.S))
				Dodge("DOWN");
			if (Input.GetKeyDown (KeyCode.Space) && Input.GetKey (KeyCode.D))
				Dodge("RIGHT");
			if (Input.GetKeyDown (KeyCode.Space) && Input.GetKey (KeyCode.W))
				Dodge("UP");
			if (Input.GetKeyDown (KeyCode.Space) && Input.GetKey (KeyCode.A))
				Dodge("LEFT");
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ) 
				Move ("DOWN");
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) ) 
				Move ("RIGHT");
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) )
				Move ("UP");
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) ) 
				Move ("LEFT");
		}

		Vector2 v = new Vector2(transform.position.x - 1, transform.position.y);
		Collider2D floor = Physics2D.OverlapPoint(v);
		if(floor) {
			Vector3 roomPos = floor.transform.parent.transform.position;
			roomPos.z = -10f;
			roomPos.x += 7.5f;
			roomPos.y -= 7f;
			Camera.main.transform.position = roomPos;
		}
	}					
}
