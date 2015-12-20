using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class HighScoreListHandler : MonoBehaviour {
	
	public GUILabel highScoreLabel;
	public GUILabel bestScoreLabel;
	
	private string privateKey = "f89ads8f97ad9s";
	
	void Start(){
		StartCoroutine(UpdateBestHighScore());
		StartCoroutine(UpdateHighScoreList());
	}

	public IEnumerator UpdateHighScoreList(){
		WWW url = new WWW("http://www.duckhood.com/mjolekyl/highscores/load-highscore.php?limit=10");
		yield return url;
		
		if (!string.IsNullOrEmpty(url.error)){
			highScoreLabel.setMyText("I can haz error\nloading highscores :(\n\nTrying again...");
			StartCoroutine(UpdateHighScoreList());
		}else{
			highScoreLabel.setMyText(url.text);
		}
	}
	
	public IEnumerator UpdateBestHighScore(){
		WWW url = new WWW("http://www.duckhood.com/mjolekyl/highscores/load-highscore.php?limit=1");
		yield return url;
		
		if (!string.IsNullOrEmpty(url.error)){
			bestScoreLabel.setMyText("");
			StartCoroutine(UpdateHighScoreList());
		}else{
			bestScoreLabel.setMyText("Best mjÖlekyler: " +url.text);
		}
	}
	
	public void SendHighScore(string nick, int score, Callback cb) {
		StartCoroutine(_SendHighScore(nick, score, cb));
	}
	
	public IEnumerator _SendHighScore(string nick, int score, Callback cb) {
		string checksum = CalculateCheksum(nick, score);
		WWW url = new WWW(
			"http://www.duckhood.com/mjolekyl/highscores/save-highscore.php?"+
			"name="+nick+"&"+
			"score="+score+"&"+
			"checkSum="+checksum+"&"
		);
		yield return url;
		if (string.IsNullOrEmpty(url.error)){
			Debug.Log("http://www.duckhood.com/mjolekyl/highscores/save-highscore.php?"+
				"name="+nick+"&"+
				"score="+score+"&"+
				"checkSum="+checksum+"&");
			
			
			cb();
		} else {
			(FindObjectOfType(typeof(InputField)) as InputField).additionalText.SetText("(Failed to send)");
			yield return new WaitForSeconds(1.5f);
			cb();
		}
	}
	
	public string CalculateCheksum(string nick, int score) {
		MD5 md5 = MD5.Create();
		byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(nick + score + privateKey);
	    byte[] hash = md5.ComputeHash(inputBytes);
	 	
		var strHash = "";
		foreach(byte b in hash) {
			strHash += b.ToString("X2").ToLower();
		}
	    Debug.Log(strHash);
		return strHash;
	}
}
