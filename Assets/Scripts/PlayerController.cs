using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Animator animator;
	public float speed = 5f;
	bool moved;

	void Start() {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		animator.SetBool ("moving", false);
		int att = animator.GetInteger ("attacking");
		animator.SetInteger("direction", 0);
		if (att > 0)
						animator.SetInteger ("attacking", att - 1);
				else {
						if (Input.GetKey (KeyCode.Space)) {
								if (animator.GetInteger ("attacking") == 0)
										animator.SetInteger ("attacking", 15);
						}
						if (Input.GetKey (KeyCode.LeftArrow)) {
								animator.SetBool ("moving", true);
								animator.SetInteger ("direction", 4);
								Vector3 position = this.transform.position;
								position.x -= speed * Time.deltaTime;
								this.transform.position = position;
								moved = true;
						}
						if (Input.GetKey (KeyCode.RightArrow)) {
								animator.SetBool ("moving", true);
								animator.SetInteger ("direction", 2);
								Vector3 position = this.transform.position;
								position.x += speed * Time.deltaTime;
								this.transform.position = position;
								moved = true;
						}
						if (Input.GetKey (KeyCode.UpArrow)) {
								animator.SetBool ("moving", true);
								animator.SetInteger ("direction", 3);
								Vector3 position = this.transform.position;
								position.y += speed * Time.deltaTime;
								this.transform.position = position;
								moved = true;
						}
						if (Input.GetKey (KeyCode.DownArrow)) {
								animator.SetBool ("moving", true);
								Vector3 position = this.transform.position;
								position.y -= speed * Time.deltaTime;
								this.transform.position = position;
								animator.SetInteger ("direction", 1);
								moved = true;
						}
				}
	}
}
