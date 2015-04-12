using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {
	private ObjectController obj_controller;
	private CombatController combat_controller;
	public AnimationClip skill_animation;
	public float skill_time = -1;
	public float skill_range = 2.5f;
	public float max_range = -1;
	public bool can_move = false;
	private float time = 0;
	private bool status = false;

	void Start () {
		obj_controller = transform.parent.GetComponent<ObjectController> ();
		if (skill_animation && skill_time == -1) {
			skill_time = skill_animation.length;
		}
		if (max_range == -1) {
			max_range = skill_range;
		}
	}
	
	public bool toUpdate () {
		if (status && time < skill_time && obj_controller.GetTargetDistance () < max_range) {
			obj_controller.RotateToPosition(obj_controller.GetTargetPosition());
			time += Time.deltaTime;
		} else {
			toStop ();
		}
		return status;
	}

	public bool toStart() {
		if (!status && obj_controller.GetTargetDistance() < skill_range) {
			if (!can_move) {
				obj_controller.Stop_Moving();
			}
			obj_controller.PushAnimation(skill_animation);
			obj_controller.GetTargetController().GetCombatController().GetHit();
			status = true;
			time = -1 * Time.deltaTime;
		}
		return status;
	}

	public void toStop() {
		if (!can_move) {
			obj_controller.Resume_Moving ();
		}
		status = false;
	}
}