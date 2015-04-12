using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {
	public float max_move_distance = 100;
	public float min_move_distance = 1;

	private ObjectController obj_controller;

	void Start () {
		obj_controller = GetComponent<ObjectController> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			var target = locateTarget ();
			var distance = getDistance (transform.position, target);
			if (distance >= min_move_distance && distance <= max_move_distance) {
				Debug.Log ("Player: move to " + target);
				obj_controller.SetDestination (target);
			}
		}
	}

	Vector3 locateTarget() {
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, max_move_distance)) {
			return new Vector3 (hit.point.x, hit.point.y, hit.point.z);
		}
		return transform.position;
	}

	float getDistance (Vector3 pos1, Vector3 pos2) {
		return Vector3.Distance (new Vector3 (pos1.x, 0, pos1.z),
		                         new Vector3 (pos2.x, 0, pos2.z));
	}
}
