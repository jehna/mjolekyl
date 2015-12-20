using UnityEngine;
using System.Collections;

public class TimeBar : MonoBehaviour {
	
	public void SetTimeLeft(float timeLeftInSeconds, float maxTimeInSeconds){
		Vector3 newScale =  transform.localScale;
		newScale.x = timeLeftInSeconds/maxTimeInSeconds;
		transform.localScale = newScale;
	}
	
	void Update() {
		SetTimeLeft(Game.time.time, Game.time.maxTime);
	}
}
