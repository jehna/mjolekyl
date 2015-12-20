using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	
	public float maxTime = 120.0f;
	float timeLeft = 120.0f;
	float timeScale = 1.0f;
	bool gameIsOver = false;
	
	void FixedUpdate () {
		if(gameIsOver) return;
		this.timeLeft -= Time.fixedDeltaTime * timeScale;
		this.timeScale += Time.fixedDeltaTime * 0.01f;
		
		//Debug.Log(this.timeLeft);
		
		if(this.time <= 0) {
			Game.Over();
			gameIsOver = true;
		}
	}
	
	public void AddTime(float value) {
		timeLeft += value;
		timeLeft = Mathf.Min(maxTime, timeLeft);
	}
	
	public float time {
		get {
			return Mathf.Max(0, timeLeft);
		}
		set {
		}
	}
}
