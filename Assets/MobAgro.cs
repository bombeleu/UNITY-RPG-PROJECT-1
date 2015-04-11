using UnityEngine;
using System.Collections;

public class MobAgro : MonoBehaviour {
	public float ratation_speed = 5;
	public float min_distance = 1;
	public float max_distance = 3;
	public float agro_range = 7;
	public float un_agro_range = 10;
	public float run_speed = 4;
	public AnimationClip run_animation;

	private ObjectController obj_controller;
	private CharacterController controller;
	private Vector3 last_position;
	private Vector3 start_position;
	private bool target = false;
	private bool agro = false;
	private bool move = false;
	
	void Start () {
		obj_controller = GetComponent<ObjectController> ();
		controller = GetComponent<CharacterController> ();
		start_position = transform.position;
	}
	void Update () {
		target = inRange ();
		if (target) {
			rotateToPosition (obj_controller.GetTargetPosition ());
			var distance = Vector3.Distance (transform.position, obj_controller.GetTargetPosition ());
			if (distance > min_distance) {
				controller.SimpleMove (transform.forward * run_speed);
				if (move && (distance < max_distance || transform.position == last_position)) {
					move = false;
					obj_controller.ResetAnimation (true);
				} else if (!move && distance > max_distance && transform.position != last_position) {
					move = true;
					obj_controller.SetAnimation (run_animation, true);
				}
			}
		} else if (agro) {
			obj_controller.ResetAnimation (true);
			agro = false;
		}
		last_position = transform.position;
	}

	public bool Agro() {
		return agro;
	}

	private bool inRange() {
		if (Vector3.Distance (transform.position, obj_controller.GetTargetPosition()) > un_agro_range) {
			if (target) Debug.Log("Mob: leave Player!");
			move = false;
			return false;
		} else {
			if (!target) Debug.Log("Mob: atack Player!");
			agro = true;
			return true;
		}
	}
		
	private void rotateToPosition(Vector3 val) {
		var rotation = Quaternion.LookRotation (val - transform.position);
		rotation.x = 0f;
		rotation.z = 0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * ratation_speed);
	}
}