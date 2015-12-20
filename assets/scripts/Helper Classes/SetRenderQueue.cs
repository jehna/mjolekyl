using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour {
	
	public int rendererQueue = 1000;
	
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.renderQueue = rendererQueue;
	}
}
