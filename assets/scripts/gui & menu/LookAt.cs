using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	
	public Transform target;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform, Vector3.up);
	}
}
