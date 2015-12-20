using UnityEngine;
using System.Collections;

public class ScoreSetter : MonoBehaviour {
	
	private string defaultText = "mjölekyls connected: ";
	private GUILabel scoreLabel;
	
	void Start () {
		Game.score.OnScoreChange += new Callback(UpdateScore);
		scoreLabel = gameObject.GetComponent<GUILabel>();
		UpdateScore();
	}
	
	void UpdateScore () {
		scoreLabel.setMyText(defaultText + Game.score.value);
	}
}
