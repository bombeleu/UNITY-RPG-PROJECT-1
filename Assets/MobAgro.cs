using UnityEngine;
using System.Collections;

public class MobAgro : MonoBehaviour {
	public float agro_range = 7;
	public float un_agro_range = 10;

	private ObjectController obj_controller;
	private Vector3 start_position;
	private Vector3 last_target_location;
	private bool agro = false;
	private bool returning = true;
	
	void Start () {
		obj_controller = GetComponent<ObjectController> ();
		start_position = transform.position;
	}
	void Update () {
		var target = inRange ();
		if (!agro && target) {
			Debug.Log("Mob: atack Player!");
			returning = false;
			agro = true;
			obj_controller.SetDestination(obj_controller.GetTargetPosition());
		} else if (agro && !target && Vector3.Distance (transform.position, obj_controller.GetTargetPosition()) > un_agro_range) {
			Debug.Log("Mob: leave Player!");
			agro = false;
			obj_controller.SetDestination(start_position);
		}
		if (!agro && !target && !returning) {
			Debug.Log("Mob: come Back!");
			returning = true;
			obj_controller.SetDestination(start_position);
		} else if (agro && Vector3.Distance(obj_controller.GetTargetPosition(), last_target_location) > 1) {
			obj_controller.SetDestination(obj_controller.GetTargetPosition());
		}
		if (agro) {
			last_target_location = transform.position;	
		} 
	}

	public bool Agro() {
		return agro;
	}

	private bool inRange() {
		if (Vector3.Distance (transform.position, obj_controller.GetTargetPosition()) < agro_range) {
			return true;
		} else {
			return false;
		}
	}
}