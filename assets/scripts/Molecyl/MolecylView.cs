using UnityEngine;
using System.Collections;

public class MolecylView : MonoBehaviour {
	
	public AnimationCurve timeCurve;
	
	public Renderer mainRenderer;
	public Renderer[] haloRenderers;
	public ParticleSystem particleRenderer;
	
	private Rigidbody myRigidbody;
	private bool isEmitting = true;
	
	void Start(){
		myRigidbody = GetComponent<Rigidbody>();
	}
	
	public void Animate(MolecylType type, MolecylRace race) {
		int colorID = (int)race;
		int sizeID = (int)type;
		
		Settings settings = Game.settings;
		MolecylColors colors = settings.molecylColors[colorID];
		mainRenderer.material.color = colors.mainColor;
		foreach(Renderer r in haloRenderers) {
			r.material.color = colors.haloColor;
		}
		particleRenderer.GetComponent<Renderer>().material.SetColor("_TintColor", colors.particleColor);
		(GetComponent<Collider>() as SphereCollider).radius = 0.4f;
		(GetComponent<Collider>() as SphereCollider).radius = 0.45f;
		//particleRenderer.particleEmitter.emitterVelocityScale = settings.molecylSizes[id];
		
		StartCoroutine(SizeTo(settings.molecylSizes[sizeID], settings.molecylSpeeds[sizeID], null));
	}
	
	public IEnumerator SizeTo(float targetScale, float tweenTime, Callback c) {
		float startTime = Time.time;
		float startScale = transform.localScale.x;
		while(Time.time < startTime + tweenTime) {
			transform.localScale = Vector3.Lerp(Vector3.one * startScale, Vector3.one * targetScale, timeCurve.Evaluate((Time.time - startTime) / tweenTime));
			yield return new WaitForEndOfFrame();
		}
		if(c != null) c();
	}
	
	public IEnumerator PositionTo(Vector3 targetPosition, float tweenTime, Callback c) {
		float startTime = Time.time;
		Vector3 startPosition = transform.position;
		while(Time.time < startTime + tweenTime) {
			transform.position = Vector3.Lerp(startPosition, targetPosition, (Time.time - startTime) / tweenTime);
			yield return new WaitForEndOfFrame();
		}
		if(c != null) c();
	}
	
	void Update() {
		if(isEmitting && myRigidbody.velocity.sqrMagnitude < 25) {
			particleRenderer.emissionRate = 0;
			isEmitting = false;
		} else if(!isEmitting && myRigidbody.velocity.sqrMagnitude > 25) {
			particleRenderer.emissionRate = 300;
			isEmitting = true;
		}
	}
	
}

[System.Serializable]
public class MolecylColors {
	public Color mainColor;
	public Color haloColor;
	public Color particleColor;
}