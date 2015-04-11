using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {
	public float speed = 4;
	public float ratation_speed = 10;
	public float min_move_distance = 1.5f;
	public float max_move_distance = 40;
	public AnimationClip run_animation;

	private ObjectController obj_controller;
	private CharacterController controller;
	private Vector3 target;
	private bool move;

	void Start () {
		move = false;
		obj_controller = GetComponent<ObjectController> ();
		controller = GetComponent <CharacterController> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			move = locateTarget();
			if (getDistance(transform.position, target) >= min_move_distance) {
				Debug.Log("Player: move to " + target);
			} else {
				move = false;
				obj_controller.ResetAnimation(true);
			}
		}
		if (move) {
			rotateToPosition ();
			if (getDistance(transform.position, target) >= min_move_distance) {
				controller.SimpleMove (transform.forward * speed);
				obj_controller.SetAnimation(run_animation, true);
			} else {
				move = false;
				obj_controller.ResetAnimation(true);
			}
		}
	}

	bool locateTarget() {
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, max_move_distance)) {
			target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
			return true;
		}
		return false;
	}

	void rotateToPosition() {
		var rotation = Quaternion.LookRotation (target - transform.position);
		rotation.x = 0f;
		rotation.z = 0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * ratation_speed);
	}

	float getDistance (Vector3 pos1, Vector3 pos2) {
		return Vector3.Distance (new Vector3 (pos1.x, 0, pos1.z),
		                         new Vector3 (pos2.x, 0, pos2.z));
	}
}
