using UnityEngine;
using System.Collections;

public class BackdropsController : MonoBehaviour {
	
	public GameObject cameraToFollow;
	
	private Vector3 cameraStartPos;
	
	void Start(){
		if(cameraToFollow){
			cameraStartPos = cameraToFollow.transform.position;
		}
		
	}
	
	void Update () {
		if(cameraToFollow){
			transform.position = cameraToFollow.transform.position - cameraStartPos;
		}
	}
}
