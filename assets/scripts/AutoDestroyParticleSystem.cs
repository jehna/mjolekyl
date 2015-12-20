using UnityEngine;
using System.Collections;

public class AutoDestroyParticleSystem : MonoBehaviour {

	void Update () {
		if(!GetComponent<ParticleSystem>().IsAlive()){
			Destroy(gameObject);
		}
	}
}
