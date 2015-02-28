using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	Animator animator;
	public float speed = 5f;
	bool moved;
	public int health;

	void Attack()
	{

	}

	void Move(int dir)
	{
		animator.SetBool ("moving", true);
		Vector2 position = this.transform.position;

		// 1: down, 2: right, 3: up, 4: left
		switch(dir)
		{
		case 1: 
			position.y -= speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", dir);
			break;
		case 2:
			position.x += speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", dir);
			break;
		case 3:
			position.y += speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", dir);
			break;
		case 4:
			position.x -= speed * Time.deltaTime;
			this.transform.position = position;
			animator.SetInteger ("direction", dir);
			break;
		}

		moved = true;
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
			if (Input.GetKey (KeyCode.Space)) 
			{
				if (animator.GetInteger ("attacking") == 0)
							animator.SetInteger ("attacking", 15);
			}
			if (Input.GetKey (KeyCode.DownArrow)) 
				Move (1);
			if (Input.GetKey (KeyCode.RightArrow)) 
				Move (2);
			if (Input.GetKey (KeyCode.UpArrow))
				Move (3);
			if (Input.GetKey (KeyCode.LeftArrow)) 
				Move (4);
		}
	}					
}
