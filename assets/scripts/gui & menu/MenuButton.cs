using UnityEngine;
using System.Collections;

public enum ButtonType{
	None,
	StartButton,
	HighScoresButton,
	PauseButton,
	ContinueGameButton,
	BackToMenuButton,
	BackToMenuFromHighScore,
	TutorialButton
}

public class MenuButton : MonoBehaviour {
	
	public ButtonType buttonType;
	public GameObject[] myShadows;
	public bool createShadows;
	public bool justClicked;
	public GameObject myActionReactor;
	private Vector3 originalScale;
	private Vector3 overScale;
	private Vector3 clickedScale;

	void Start () {
		originalScale = transform.localScale;
		overScale = transform.localScale*1.1f;
		clickedScale = transform.localScale*1.2f;
		if(createShadows){
			for(int i = 0; i < myShadows.Length; i++){
				myShadows[i].GetComponent<TextMesh>().text = gameObject.GetComponent<TextMesh>().text;
				myShadows[i].GetComponent<TextMesh>().fontSize = gameObject.GetComponent<TextMesh>().fontSize;
				myShadows[i].SetActive(true);
			}
		}
	}

	void OnMouseDown(){
		if(!justClicked && buttonType != ButtonType.None){
			Game.sound.Play("click", 0.6f);
			StartCoroutine(ButtonClicked());
			switch(buttonType){
			case ButtonType.StartButton:
				if(PlayerPrefs.GetInt("FirstPlay", 0) == 0) {
					TutorialButtonPressed();
					PlayerPrefs.SetInt("FirstPlay", 1);
				} else {
					StartGameButtonPressed();
				}
				break;
			case ButtonType.TutorialButton:
				TutorialButtonPressed();
				break;
			
			case ButtonType.HighScoresButton:
				HighScoresButtonPressed();
				break;
			case ButtonType.PauseButton:
				PauseButtonPressed();
				break;
				
			case ButtonType.ContinueGameButton:
				myActionReactor.SetActive(false);
				Time.timeScale = 1.0f;
				break;
				
			case ButtonType.BackToMenuButton:
				// TO DO: Add here player prefs value so menu knows where to go! And maybe nice animation.
				Time.timeScale = 1;
				Game.loader.Load ("Menu");
				break;
				
			case ButtonType.BackToMenuFromHighScore:
				myActionReactor = Camera.main.gameObject;
				myActionReactor.GetComponent<CameraMoverScript>().MoveToPosition(CameraMenuPosition.MainMenu, true, null);
				HighScoreListHandler highScoreList = GameObject.FindObjectOfType(typeof (HighScoreListHandler)) as HighScoreListHandler;
				if(highScoreList != null){
					highScoreList.StartCoroutine(highScoreList.UpdateBestHighScore());
				}
				break;
			}
		}
	}
	
	void OnMouseEnter(){
		if(buttonType != ButtonType.None){
			transform.localScale = overScale;	
		}
	}
	
	void OnMouseExit(){
		if(buttonType != ButtonType.None){
			transform.localScale = originalScale;
		}
	}
	
	void StartGameButtonPressed(){
		Game.loader.Load("Game");	
	}
	
	void PauseButtonPressed(){
		myActionReactor.SetActive(true); 
		Time.timeScale = 0;
	}
	
	public static void HighScoresButtonPressed(){
		Camera.main.GetComponent<CameraMoverScript>().MoveToPosition(CameraMenuPosition.HighScore, true, null);
		HighScoreListHandler highScoreList = GameObject.FindObjectOfType(typeof (HighScoreListHandler)) as HighScoreListHandler;
		if(highScoreList != null){
			highScoreList.StartCoroutine(highScoreList.UpdateHighScoreList());
		}
	}
	
	public static void TutorialButtonPressed(){
		Camera.main.GetComponent<CameraMoverScript>().MoveToPosition(CameraMenuPosition.Tutorial, true, null);
	}
	
	IEnumerator ButtonClicked(){
		justClicked = true;
		clickedScale = transform.localScale*1.1f;
		transform.localScale = clickedScale;
		yield return new WaitForSeconds(0.15f);
		transform.localScale = originalScale;
		justClicked = false;
	}
	
	void OnBecameVisible(){
		transform.localScale = originalScale;
		justClicked = false;
	}
}