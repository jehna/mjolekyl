using UnityEngine;
using System.Collections;

public enum CameraMenuPosition{
	MainMenu,
	HighScore,
	EnterScore,
	LoadingScreen,
	Tutorial
}

public class CameraMoverScript : MonoBehaviour {
	
	public Vector3 mainMenuPosition;
	public Vector3 highScorePosition;
	public Vector3 enterScorePosition;
	public Vector3 loadingScreenPosition;
	public Vector3 tutorialPosition;
	
	private bool needToMove;
	private Vector3 toPoint;
	
	private Callback callBack;
		
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("CameraMoveToPos") == "EnterScore"){
			MoveToPosition(CameraMenuPosition.HighScore, false, null);
		}else{
			MoveToPosition(CameraMenuPosition.MainMenu, false, null);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(needToMove){
			transform.position = Vector3.Lerp (transform.position, toPoint, Time.deltaTime*6);
			if(Vector3.Distance(toPoint,transform.position) < 0.01){
				transform.position = toPoint;
				needToMove = false;
				if(callBack != null){
					callBack();
					callBack = null;
				}
			}
		}
	}
	
	public void MoveToPosition(CameraMenuPosition cameraMenuPosition, bool animateMove, Callback newCallBack){
		switch(cameraMenuPosition){
		case CameraMenuPosition.MainMenu:
			if(animateMove){
				toPoint = mainMenuPosition;
				needToMove = true;
				callBack = newCallBack;
			}else{
				transform.position = mainMenuPosition;
			}
			break;
			
		case CameraMenuPosition.HighScore:
			if(animateMove){
				toPoint = highScorePosition;
				needToMove = true;
				callBack = newCallBack;
			}else{
				transform.position = highScorePosition;
			}
			break;
			
		case CameraMenuPosition.EnterScore:
			if(animateMove){
				toPoint = enterScorePosition;
				needToMove = true;
				callBack = newCallBack;
			}else{
				transform.position = enterScorePosition;
			}
			break;
		case CameraMenuPosition.LoadingScreen:
			if(animateMove){
				toPoint = loadingScreenPosition;
				needToMove = true;
				callBack = newCallBack;
			}else{
				transform.position = loadingScreenPosition;
			}
			break;
		case CameraMenuPosition.Tutorial:
			if(animateMove){
				toPoint = tutorialPosition;
				needToMove = true;
				callBack = newCallBack;
			}else{
				transform.position = tutorialPosition;
			}
			break;
		}
	}
}




