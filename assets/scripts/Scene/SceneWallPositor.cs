using UnityEngine;
using System.Collections;

public class SceneWallPositor : MonoBehaviour {
	
	void Start () {
		
		float startScale = 4.0f / 3.0f;
		float currentScale = (float)Screen.width / (float)Screen.height;
		
		transform.position = transform.position - transform.position.x * -Vector3.left + (transform.position.x) * (currentScale / startScale) * -Vector3.left;
		
	}
}