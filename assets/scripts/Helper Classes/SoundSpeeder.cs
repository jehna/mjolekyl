using UnityEngine;
using System.Collections;

public class SoundSpeeder : MonoBehaviour {
	
	private AudioSource source = null;
	private bool inRightScene = false;
	
	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(inRightScene && Game.time.time < Game.time.maxTime/2) {
			source.pitch = 1.0f + (1-(Game.time.time / (Game.time.maxTime/2))) * 0.25f;
		}
	}
	
	void OnLevelWasLoaded(int level) {
		if(Application.loadedLevelName == "Game") {
			inRightScene = true;
			Game.OnGameOver += OnGameOver;
		} else {
			inRightScene = false;
		}
	}
	
	void OnGameOver() {
		inRightScene = false;
		Game.sound.Play("explosion", 7.0f);
		if(source != null) source.pitch = 1.0f;
	}
	
}
