using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {
	private GameObject target;
	private NavMeshAgent nav_agent;
	public float ratation_speed = 5;
	public float min_nav_distance = 0.5f;

	public AnimationClip default_animation;
	public float min_speed = 1;
	public AnimationClip run_animation;

	private bool runing = true;
	private Vector3 last_position;
	private float _animate = 0;

	private Animation animation_obj;

	void Start () {
		nav_agent = GetComponent<NavMeshAgent>();
		nav_agent.SetDestination (transform.position);
		last_position = transform.position;
		animation_obj = transform.FindChild ("Mesh").GetComponent <Animation> ();
	}

	void Update () {
		if (runing && transform.position == last_position) {
			runing = false;
			StopAnimation (run_animation);
			PlayAnimation (default_animation);
			if (isMoving()) {
				Stop_Moving ();
			}
		} else if (!runing && transform.position != last_position) {
			runing = true;
			StopAnimation (default_animation);
			PlayAnimation (run_animation);
		}
		if (runing) {
			last_position = transform.position;
		}
	}

	public CombatController GetCombatController() {
		return GetComponent<CombatController> ();
	}

	/****** Moving ******/
	public void Warp(Vector3 pos) {
		nav_agent.Stop ();
		nav_agent.Warp (pos);
	}

	public void SetDestination(Vector3 dist) {
		nav_agent.SetDestination (dist);
		Resume_Moving ();
	}

	public Vector3 GetDestination() {
		return nav_agent.destination;
	}

	public void RotateToPosition(Vector3 val) {
		var rotation = Quaternion.LookRotation (val - transform.position);
		rotation.x = 0f;
		rotation.z = 0f;
		Rotate(Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * ratation_speed));
	}

	public void Rotate(Quaternion dir) {
		transform.rotation = dir;
	}

	public void Stop_Moving() {
		nav_agent.Stop ();
	}

	public void Resume_Moving() {
		nav_agent.Resume ();
	}

	public bool isMoving() {
		return (nav_agent.speed == 0) ? true : false;
	}

	/****** Animation ******/
	public void PushAnimation(AnimationClip val) {
		animation_obj [val.name].wrapMode = WrapMode.Once;
		animation_obj [val.name].layer = 1;
		animation_obj.CrossFade (val.name);
	}

	public void MixAnimation(AnimationClip val) {
		animation_obj [val.name].wrapMode = WrapMode.Once;
		animation_obj.CrossFade (val.name);
	}

	public void AddAnimation(AnimationClip val) {
		animation_obj.AddClip (val, val.name);
	}

	public void PlayAnimation(AnimationClip val) {
		animation_obj [val.name].wrapMode = WrapMode.Loop;
		animation_obj.Play (val.name);
	}

	public void StopAnimation(AnimationClip val) {
		animation_obj.Stop (val.name);
	}

	/****** Target ******/
	public void SetTarget(GameObject val) {
		this.target = val;
	}

	public void SetTargetTarget(GameObject val) {
		GetTargetController ().SetTarget (val);
	}

	public GameObject GetTarget() {
		return target;
	}

	public ObjectController GetTargetController() {
		return target.GetComponent <ObjectController> ();
	}

	public GameObject GetTargetTarget() {
		return GetTargetController ().GetTarget ();
	}

	public Vector3 GetTargetPosition() {
		return target.transform.position;
	}

	public float GetTargetDistance() {
		return Vector3.Distance (transform.position, target.transform.position);
	}
}