using UnityEngine;
using System.Collections;

public class InfoText : MonoBehaviour {
	
	public TextMesh[] textHolders;
	public float lerpTime = 1.4f;
	public float showCharTime = 0.2f;
	public AnimationCurve lerpIn;
	public AnimationCurve lerpOut;
	
	private Vector3 visiblePos;
	private Vector3 startPos;
	private Vector3 endPos;
	
	private bool started = false;
	
	void Start () {
		if(started) return;
		
		started = true;
		visiblePos = transform.position;
		startPos = visiblePos + Vector3.up * 6.0f;
		endPos = visiblePos + Vector3.up * -6.0f;
		
		transform.position = startPos;
	}
	
	public void Show(string text, Callback onReady) {
		if(!started) Start();
		
		foreach(TextMesh textHolder in textHolders) {
			textHolder.text = text;
		}
		StartCoroutine(LerpIn(onReady, text.Length * showCharTime));
	}
			
	IEnumerator LerpIn(Callback onReady, float waitFor) {
		float startTime = Time.time;
		while(Time.time < startTime + lerpTime) {
			transform.position = Math.Lerp(startPos, visiblePos, lerpIn.Evaluate((Time.time - startTime) / lerpTime));
			yield return new WaitForEndOfFrame();
		}
		
		yield return new WaitForSeconds(waitFor);
		
		if(onReady != null) onReady();
		
		startTime = Time.time;
		while(Time.time < startTime + lerpTime) {
			transform.position = Math.Lerp(visiblePos, endPos, lerpOut.Evaluate((Time.time - startTime) / lerpTime));
			yield return new WaitForEndOfFrame();
		}
	}
}
