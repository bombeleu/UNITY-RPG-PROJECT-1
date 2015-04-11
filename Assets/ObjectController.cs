using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {
	private GameObject target;

	private bool _animation = true;
	private Animation animation_obj;
	public AnimationClip default_animation;
	private AnimationClip base_animation;
	private AnimationClip action_animation;

	void Start () {
		animation_obj = transform.FindChild ("Mesh").GetComponent <Animation> ();
	}

	void Update () {
		if (!animation_obj.isPlaying && base_animation && _animation) {
			animation_obj.CrossFade (base_animation.name);
		}
	}

	/****** Animation ******/
	public void PushAnimation(AnimationClip val) {
		animation_obj [val.name].wrapMode = WrapMode.Once;
		animation_obj.CrossFade (val.name);
	}

	public void MixAnimation(AnimationClip val) {
		animation_obj [val.name].wrapMode = WrapMode.Once;
		animation_obj.Play (val.name, AnimationPlayMode.Mix);
	}

	public void SetAnimation(AnimationClip val) {
		base_animation = val;
	}

	public void SetAnimation(AnimationClip val, bool run) {
		SetAnimation(val);
		if (run) {
			RestartAnimation ();
		}
	}

	public void ResetAnimation() {
		base_animation = default_animation;
	}

	public void ResetAnimation(bool run) {
		ResetAnimation ();
		if (run) {
			RestartAnimation ();
		}
	}

	public void RestartAnimation() {
		if (base_animation) {
			_animation = true;
			animation_obj.CrossFade (base_animation.name);
		}
	}

	public void StopAnimation() {
		animation_obj.Stop ();
		_animation = false;
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
}