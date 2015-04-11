using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {
	private ObjectController obj_controller;

	void Start () {
		obj_controller = GetComponent<ObjectController> ();
		obj_controller.SetTarget (GameObject.FindGameObjectWithTag("Player"));
	}

	void Update () {
	}

	void onMouseOver() {
		obj_controller.SetTargetTarget (this.gameObject);
	}
}