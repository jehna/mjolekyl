using UnityEngine;
using System.Collections;

public class MolecylHaloRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate((Vector3.up * 40)*Time.deltaTime, Space.Self);
	}
}
