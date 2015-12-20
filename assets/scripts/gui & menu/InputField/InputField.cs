using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class InputField : MonoBehaviour {
	
	public int maxLength = 8;
	public string acceptedCharactersPattern = "[a-z0-9]";
	public string placeHolder = "Enter your name";
	public AdditionalText additionalText;
	
	private TextMesh[] targets;
//	private TouchScreenKeyboard mobileKeyboard;
	private bool hasFocus = false;
	private string inputText = "";
	private Regex validChars;
//	private float blinkCursorTime = 1.3f;
	
	// Use this for initialization
	void Start () {
		targets = gameObject.GetComponentsInChildren<TextMesh>();
		validChars = new Regex(acceptedCharactersPattern, RegexOptions.IgnoreCase);
		
		// If we're coming from the game
		if(PlayerPrefs.GetInt("LastGameHighscore", 0) > 0) {
			Focus();
		}
	}
	
	void Focus() {
//		mobileKeyboard = TouchScreenKeyboard.Open(inputText, TouchScreenKeyboardType.Default, false, false, false, false, placeHolder);
		hasFocus = true;
	}
	
	void UnFocus() {
		if(inputText == "") return;
		hasFocus = false;
		additionalText.SetText("(Sending...)");
		
		// Go to highscores
		(FindObjectOfType(typeof(HighScoreListHandler)) as HighScoreListHandler).SendHighScore(inputText, PlayerPrefs.GetInt("LastGameHighscore", 0), delegate {
			MenuButton.HighScoresButtonPressed();
			PlayerPrefs.SetInt("LastGameHighscore", 0);
		});
	}
	
	// Update is called once per frame
	void Update () {
		if(hasFocus) {
			//if(mobileKeyboard.active) inputText = mobileKeyboard.text;
			inputText = GetDesktopInput();
			
			// Strip extra characters
			if(inputText.Length > maxLength) {
				inputText = inputText.Substring(0, maxLength);
			}
			
			string renderText = inputText;
			
			if(renderText.Length == 0) renderText = placeHolder;
			else if(hasFocus) {
				// Blink cursor
				/*if(Time.time % blinkCursorTime > blinkCursorTime/2) {
					renderText += " l";
				}*/
			}
			
			// Get unfocus from mobile
			/*if(mobileKeyboard.active) {
				if(mobileKeyboard.done || mobileKeyboard.wasCanceled) {
					UnFocus();
				}
			}*/
			
			foreach(TextMesh target in targets) {
				target.text = renderText;
			}
		}
	}
	
	string GetDesktopInput() {
		string inputString = inputText;
		foreach (char c in Input.inputString) {
			
            if (c == "\b"[0]) {
                if (inputString.Length != 0) {
                    inputString = inputString.Substring(0, inputString.Length - 1);
				}
                
			} else { // Get unfocus from keyboard
                if (c == "\n"[0] || c == "\r"[0]) {
                    UnFocus();
				} else {
                    if(validChars.IsMatch(c.ToString())) inputString += c;
				}
			}
        }
		return inputString;
	}
}
