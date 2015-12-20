using UnityEngine;
using System.Collections;

public class IndestructableMenu : MonoBehaviour {
	
	[HideInInspector]
	public string myName;
	
	// Use this for initialization
	void Awake () {
		myName = gameObject.name;
		
		bool isInstantiated = false;
		foreach(IndestructableMenu other in FindObjectsOfType(typeof(IndestructableMenu)) as IndestructableMenu[]) {
			if(other != this && other.myName == this.myName) {
				isInstantiated = true;
				break;
			}
		}
		
		if(isInstantiated) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
		}
	}
}
