using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatController : MonoBehaviour {
	private ObjectController obj_controller;
	public AnimationClip hit_animation;
	private Skill[] skills;
	private Skill active_skill;
	private bool status = false;

	void Start () {
		obj_controller = GetComponent<ObjectController> ();
		skills = transform.FindChild("Combat").GetComponents<Skill> ();
	}

	void Update () {
		if (!status) {
			foreach (var skill in skills) {
				status = skill.toStart ();
				if (status) {
					active_skill = skill;
					break;
				}
			}
		}
		if (status) {
			status = active_skill.toUpdate ();
		}
	}

	public bool inCombat() {
		return status;
	}

	public void GetHit() {
		if (hit_animation) {
			obj_controller.PushAnimation (hit_animation);
		}
	}
}