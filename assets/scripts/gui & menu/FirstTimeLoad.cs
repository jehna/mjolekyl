using UnityEngine;
using System.Collections;

public class FirstTimeLoad : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(0.3f);
		Game.loader.Load("Menu");
	}
}
