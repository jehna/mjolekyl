using UnityEngine;
using System.Collections;

public class AdditionalText : MonoBehaviour {

	// Use this for initialization
	public void SetText (string text) {
		foreach(TextMesh t in gameObject.GetComponentsInChildren<TextMesh>()) {
			t.text = text;
		}
	}
}
