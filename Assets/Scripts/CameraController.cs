using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float leftx = -8;
	private float rightx = 8;
	private float upy = 4;
	private float downy = -4;
	private Transform playerTransform;

	void Start() {
		playerTransform = GameObject.Find("Player2").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTransform.position.x < leftx) {
			leftx -= 16;
			rightx -= 16;
			Vector3 cam = transform.position;
			cam.x -= 17;
			transform.position = cam;
		}
		else if(playerTransform.position.x > rightx) {
			leftx += 16;
			rightx += 16;
			Vector3 cam = transform.position;
			cam.x += 17;
			transform.position = cam;
		}	
		else if(playerTransform.position.y > upy) {
			upy += 8;
			downy += 8;
			Vector3 cam = transform.position;
			cam.y += 9;
			transform.position = cam;
		}
		else if(playerTransform.position.y < downy) {
			upy -= 8;
			downy -= 8;
			Vector3 cam = transform.position;
			cam.y -= 9;
			transform.position = cam;
		}
		 


	}
}
