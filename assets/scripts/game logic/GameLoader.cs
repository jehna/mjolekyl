using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {
	
	private CameraMenuPosition returnToPosition = CameraMenuPosition.MainMenu;
	
	public void LoadAndPanTo(string level, CameraMenuPosition position) {
		returnToPosition = position;
		Load(level);
	}
	
	public void Load(string level) {
		Camera.main.GetComponent<CameraMoverScript>().MoveToPosition(CameraMenuPosition.LoadingScreen, true, new Callback(delegate() {
			Application.LoadLevel(level);
		}));
	}
	
	void OnLevelWasLoaded() {
		Camera.main.GetComponent<CameraMoverScript>().MoveToPosition(returnToPosition, true, null);
		returnToPosition = CameraMenuPosition.MainMenu;
	}
	
}
