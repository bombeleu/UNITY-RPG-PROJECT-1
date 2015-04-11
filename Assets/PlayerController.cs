using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private ObjectController obj_controller;

	void Start () {
		obj_controller = GetComponent<ObjectController> ();
	}

	void Update () {
	
	}
}