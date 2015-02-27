using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private int leftx = -8;
	private int rightx = 8;
	private int upy = 4;
	private int downy = -4;
	private Transform playerTransform;

	void Start() {
		playerTransform = GameObject.Find("Player").transform;
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

	}
}
