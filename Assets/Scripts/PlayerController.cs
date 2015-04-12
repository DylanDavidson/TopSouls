using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Rigidbody2D rigidBody;
	public CircleCollider2D collider;
	public GameObject currentRoom;

	public int health;
	public int stamina;
	public int dodgeStamina;
	public int attackStamina;
	public float speed;
	public float staminaCooldown;
	public float dodgeCooldown;
	public float dodgeForce;
	public float dodgeWindow;

	private Animator animator;
	private SpriteRenderer renderer;
	
	private bool spriteOn;
	private int blinkCount = 0;
	private float lastDodge;
	private float lastBlock;
	private float lastAtk;
	private float lastTick;

	public int GetHealth()
	{
		return health;
	}

	public void GetPushed(Vector2 forceVector, bool blocking)
	{
		if(blocking)
			rigidBody.AddForce (forceVector, ForceMode2D.Impulse);
		else
			rigidBody.AddForce (forceVector, ForceMode2D.Impulse);
	}

	public void TakeDamage(int damage, bool blocking)
	{
		if(blocking)
		{
			if (stamina - damage > 0)
			{
				lastBlock = Time.time;
				stamina -= damage;
				InvokeRepeating("BlinkSprite", 0f, .05f);
			}

			else if(health - Mathf.Abs(stamina-damage) > 0)
			{
				lastBlock = Time.time+2f;
				stamina = 0;
				animator.SetBool("blocking", false);
				health -= Mathf.Abs(stamina-damage);
				InvokeRepeating("BlinkSprite", 0f, .1f);
			}

			else
				Die();

		}

		else
		{
			if (health - damage > 0)
			{
				health -= damage;
				InvokeRepeating("BlinkSprite", 0f, .1f);
			}

			else
				Die();
		}
	}

	public void BlinkSprite()
	{
		blinkCount++;

		if(spriteOn)
		{
			renderer.enabled = false;
			spriteOn = false;
		}

		else if(!spriteOn)
		{
			renderer.enabled = true;
			spriteOn = true;
		}

		if(blinkCount >=20)
		{
			renderer.enabled = true;
			spriteOn = true;
			blinkCount = 0;
			CancelInvoke("BlinkSprite");
		}
	}

	public void Die()
	{
		foreach(GameObject o in GameObject.FindObjectsOfType<GameObject>())
			Destroy(o);
		Application.LoadLevel ("death_scene");
	}

	public void Invincible()
	{
		if (Time.time - lastDodge >= dodgeWindow)
		{
			collider.enabled = true;
			CancelInvoke("Invincible");
		}

		else
			collider.enabled = false;
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
	}

	void Dodge(string dir)
	{
		InvokeRepeating ("Invincible", 0f, .1f);
		//InvokeRepeating ("BlinkSprite", 0f, .1f);

		switch(dir)
		{
		case "DOWN":
			rigidBody.AddForce (new Vector2(0, -1).normalized * dodgeForce, ForceMode2D.Impulse);
			break;
		case "RIGHT":
			rigidBody.AddForce (new Vector2(1, 0).normalized * dodgeForce, ForceMode2D.Impulse);
			break;
		case "UP":
			rigidBody.AddForce (new Vector2(0, 1).normalized * dodgeForce, ForceMode2D.Impulse);
			break;
		case "LEFT":
			rigidBody.AddForce (new Vector2(-1, 0).normalized * dodgeForce, ForceMode2D.Impulse);
			break;
		// TODO: Save last direction pressed and base backward dodge direction off that
		//case "BACK":
			//rigidBody.AddForce (new Vector2(1, 0).normalized * 10, ForceMode2D.Impulse);
			//break;
		}

		stamina -= dodgeStamina;
		lastDodge = Time.time;
	}

	void Block(bool blocking)
	{
		animator.SetBool("blocking", blocking);
	}

	void Attack(int direction)
	{
		animator.SetInteger ("attacking", 15);
		animator.SetInteger("direction", direction);
		lastAtk = Time.time;
		stamina -= attackStamina;
	}

	void MovementListener()
	{
		if(animator.GetInteger("attacking") == 0)
		{
			if (Input.GetKey (KeyCode.S)) 
				Move ("DOWN");
			if (Input.GetKey (KeyCode.D) ) 
				Move ("RIGHT");
			if (Input.GetKey (KeyCode.W) )
				Move ("UP");
			if (Input.GetKey (KeyCode.A) ) 
				Move ("LEFT");
		}
	}

	void AttackListener ()
	{
		int atk = animator.GetInteger ("attacking");
		animator.SetBool ("moving", false);
		
		if (atk > 0)
			animator.SetInteger ("attacking", atk-1);
		else
		{
			if(animator.GetInteger ("attacking") == 0 && stamina >= attackStamina) 
			{
				if(Input.GetKeyDown (KeyCode.DownArrow))
					Attack(1);
				else if(Input.GetKeyDown (KeyCode.RightArrow))
					Attack(2);
				else if(Input.GetKeyDown (KeyCode.UpArrow)) 
					Attack(3);
				else if(Input.GetKeyDown (KeyCode.LeftArrow))
					Attack(4);
			}
		}
	}

	void DodgeListener()
	{
		if(stamina >= dodgeStamina && Time.time-lastDodge >= dodgeCooldown)
		{
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
		}
	}

	void ShieldListener()
	{
		if(stamina > 0)
		{
			if (!animator.GetBool("blocking") &&
			    !animator.GetBool("moving") &&
			    Input.GetKey (KeyCode.LeftShift))
			{
				Block(true);
			}

			else if((animator.GetBool("blocking") && 
			    Input.GetKeyUp(KeyCode.LeftShift)) ||
			    animator.GetBool("moving"))
			{
				Block(false);
			}
		}
	}

	void StaminaManager()
	{
		if(Time.time-lastDodge >= staminaCooldown && 
		   Time.time-lastAtk >= staminaCooldown &&
		   Time.time-lastBlock >= staminaCooldown &&
		   Time.time-lastTick >= .05f &&
		   stamina < 100)
		{
			if(stamina + 1 > 100)
			{
				stamina += Mathf.Abs(100-stamina);
				lastTick = Time.time;
			}
			else
			{
				stamina += 1;
				lastTick = Time.time;
			}
		}
	}

	void RoomTransistioner()
	{
		/*Vector2 v = new Vector2(transform.position.x - 1, transform.position.y);
		Collider2D floor = Physics2D.OverlapPoint(v);


		if(floor && floor.transform.parent && !floor.transform.parent.CompareTag("Enemy")) {
			currentRoom = floor.transform.parent.gameObject;
			Vector3 roomPos = currentRoom.transform.position;
			roomPos.z = -10f;
			roomPos.x += 7.5f;
			roomPos.y -= 7f;
			Camera.main.transform.position = roomPos;
		}
		*/

		Camera.main.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
	}

	void Start() {
		animator = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<CircleCollider2D>();
	}


	void Update()
	{
		AttackListener ();
		MovementListener();
		DodgeListener ();
		ShieldListener ();
		StaminaManager ();
		RoomTransistioner ();
	}
	
}
