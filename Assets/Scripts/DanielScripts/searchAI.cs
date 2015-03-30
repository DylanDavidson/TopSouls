using UnityEngine;
using System.Collections;

public class searchAI : Pathfinding2D {


	private Vector2 moveDirection;
	
	public float moveSpeed;
	public int turnSpeed = 5;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("moveToPlayer",2.0f,1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPos = transform.position;


		// initiate A*
		if (Input.GetButton ("Fire2")) {
			//buttonPress = true;
			Vector2 moveToward = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//Debug.Log ("x: " + moveToward.x + "y: " + moveToward.y);


			FindPath (currentPos, moveToward);

			int d = 0;
			foreach (Vector3 x in Path) {

				Debug.Log ("Node " + d + " at x: " + Path [d].x + " y: " + Path [d].y);
				d++;
			}

		// joystick movement 

		//} else if (Input.GetButton ("Fire1")) {
			//Vector2 moveToward = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//transform.position = Vector3.MoveTowards (transform.position, moveToward, 2);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			// calculate new target destination and lerp
			Vector2 ntarget = ( (Vector2)(-transform.up*2f) + currentPos);
			transform.position = Vector2.Lerp ( currentPos, ntarget, Time.deltaTime);
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			// calculate new target destination and lerp
			Vector2 ntarget = ( (Vector2)(transform.up*2f) + currentPos);
			transform.position = Vector2.Lerp ( currentPos, ntarget, Time.deltaTime);
		}
		else if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector2 ntarget = ( (Vector2)(-transform.right*2f) + currentPos);
			transform.position = Vector2.Lerp ( currentPos, ntarget, Time.deltaTime);
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			Vector2 ntarget = ( (Vector2)(transform.right*2f) + currentPos);
			transform.position = Vector2.Lerp ( currentPos, ntarget, Time.deltaTime);
		}

		int i = 0;




		//Debug.DrawLine (Path[0], Path[1]);
		//Debug.DrawLine (Path [1], Path [2]);
		//Debug.DrawLine (Path [2], Path [3]);

		
		
		if (Path.Count > 0) {

			// draw line along path
			foreach (Vector3 x in Path)
			{
				//Debug.Log ("Node " + i + " at x: " + Path[i].x + "y: " + Path[i].y);
				
				if (i != 0)
					Debug.DrawLine (Path[i-1], Path[i]);
				else
					Debug.DrawLine (currentPos, Path[0]);
				i++;
			}

			// move according to path
			Move ();
		}
	}

	void moveToPlayer()
	{
		Vector2 move = (Vector2) GameObject.FindWithTag ("wandar").transform.position;
		
		FindPath (transform.position, move);
	}
}
