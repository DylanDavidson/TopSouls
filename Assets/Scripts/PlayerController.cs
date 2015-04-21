using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public const float DEFAULT_SPEED = 3;
	public const float SPEED_BOOST = 3;
	public const int HEALTH_BOOST = 20;

	public Rigidbody2D rigidBody;
	public CircleCollider2D collider;
	public GameObject currentRoom;
	public AudioClip hurtSound;
	public AudioClip dodgeSound;
	public AudioClip deathSound;
	public AudioClip blockSound;

	public int health;
	public int stamina;
	public int dodgeStamina;
	public int attackStamina;
	public float speed = DEFAULT_SPEED;
	public float staminaCooldown;
	public float attackCooldown;
	public float dodgeCooldown;
	public float dodgeForce;
	public float dodgeWindow;

	private Animator animator;
	private SpriteRenderer renderer;
	private AudioSource source;
	
	private bool spriteOn;
	private int blinkCount = 0;
	private float lastDodge;
	private float lastBlock;
	private float lastAtk;
	private float lastTick;
	private float powerupTicks;


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
			source.PlayOneShot (blockSound);

			if (stamina - damage > 0)
			{
				lastBlock = Time.time;
				stamina -= damage;
				InvokeRepeating("BlinkSprite", 0f, .05f);
			}

			else if(health - Mathf.Abs(stamina-damage) > 0)
			{
				source.PlayOneShot (hurtSound);

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
			source.PlayOneShot (hurtSound);

			if (health - damage > 0)
			{
				health -= damage;
				InvokeRepeating("BlinkSprite", 0f, .1f);
			}

			else
				Die();
		}
	}


	private void BlinkSprite()
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


	private void Die()
	{
		source.PlayOneShot (deathSound);

		for(int i=0; i<1000000000; i++);

		foreach(GameObject o in GameObject.FindObjectsOfType<GameObject>())
			Destroy(o);
		Application.LoadLevel ("death_scene");
	}


	private void MovementListener()
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


	private void Move(string dir)
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


	private void DodgeListener()
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


	private void Dodge(string dir)
	{
		InvokeRepeating ("DodgeWindowActive", 0f, .1f);
		//InvokeRepeating ("BlinkSprite", 0f, .1f);

		source.PlayOneShot (dodgeSound);

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


	private void DodgeWindowActive()
	{
		if (Time.time - lastDodge >= dodgeWindow)
		{
			gameObject.layer = LayerMask.NameToLayer("Player");
			animator.enabled = true;
			CancelInvoke("DodgeWindowActive");
		}
		
		else
		{
			gameObject.layer = LayerMask.NameToLayer("Dodging");
			animator.enabled = false;
		}
	}


	private void AttackListener ()
	{
		int atk = animator.GetInteger ("attacking");
		animator.SetBool ("moving", false);
		
		if (atk > 0)
			animator.SetInteger ("attacking", atk-1);
		else
		{
			if(animator.GetInteger ("attacking") == 0 && 
			   stamina >= attackStamina && 
			   Time.time-lastAtk >= attackCooldown) 
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


	private void Attack(int direction)
	{
		animator.SetInteger ("attacking", 15);
		animator.SetInteger("direction", direction);
		lastAtk = Time.time;
		stamina -= attackStamina;
	}


	private void ShieldListener()
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


	private void Block(bool blocking)
	{
		animator.SetBool("blocking", blocking);
	}


	private void StaminaManager()
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


	private void RoomTransistioner()
	{
		Camera.main.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
	}


	public void SpeedUp() {
		speed += SPEED_BOOST;
		Invoke ("ResetSpeed", 5);
	}


	private void ResetSpeed() {
		speed = DEFAULT_SPEED;
	}


	public void HealthUp() {
		health += HEALTH_BOOST;
		if(health > 100)
			health = 100;
	}


	void Start() {
		animator = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<CircleCollider2D>();
		source = GetComponent<AudioSource> ();
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
