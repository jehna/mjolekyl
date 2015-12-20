using UnityEngine;
using System.Collections;

public class GameScore : MonoBehaviour {
	int _score = 0;
	
	void Start() {
		Game.OnGameOver += new Callback(OnGameOver);
	}
	
	void OnGameOver() {
		PlayerPrefs.SetInt("LastGameHighscore", _score);
		
		if(PlayerPrefs.GetInt("AlltimeHighscore", 0) < _score) {
			PlayerPrefs.SetInt("AlltimeHighscore", _score);
		}
	}
	
	public void Add(int val) {
		_score += val;
		if(OnScoreChange != null) OnScoreChange();
	}
	
	public int value {
		get {
			return _score;
		}
		set {
		}
	}
	
	public Callback OnScoreChange;
}
