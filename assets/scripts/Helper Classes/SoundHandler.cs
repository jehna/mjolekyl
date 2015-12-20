using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {
	
	public CustomSound[] sounds;
	
	private AudioSource player = null;
	
	public void Start() {
		player = gameObject.AddComponent<AudioSource>();
	}
	
	public void Play(string soundName, float volume) {
		AudioClip clip = null;
		foreach(CustomSound sound in sounds) {
			if(sound.name == soundName) {
				clip = sound.sound;
				break; //dance
			}
		}
		
		if(clip == null) return;
		
		player.volume = volume;
		player.PlayOneShot(clip);
	}
}

[System.Serializable]
public class CustomSound {
	public string name;
	public AudioClip sound;
}