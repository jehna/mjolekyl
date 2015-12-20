using UnityEngine;
using System.Collections;

public class GUILabel : MonoBehaviour {
	
	public bool createShadows;
	public GameObject[] myShadows;

	// Use this for initialization
	void Start () {
		if(createShadows){
			for(int i = 0; i < myShadows.Length; i++){
				myShadows[i].GetComponent<TextMesh>().text = gameObject.GetComponent<TextMesh>().text;
				myShadows[i].GetComponent<TextMesh>().fontSize = gameObject.GetComponent<TextMesh>().fontSize;
				myShadows[i].SetActive(true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setMyText(string newText){
		gameObject.GetComponent<TextMesh>().text = newText;
		for(int i = 0; i < myShadows.Length; i++){
			myShadows[i].GetComponent<TextMesh>().text = gameObject.GetComponent<TextMesh>().text;
		}
	}
}
