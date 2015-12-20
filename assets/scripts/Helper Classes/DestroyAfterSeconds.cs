using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {

	public float dieTime = 0.8f;
	
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(dieTime);
		Destroy(gameObject);
	}
}
