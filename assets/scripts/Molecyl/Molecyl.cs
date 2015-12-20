using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Molecyl : MonoBehaviour {
	
	public ParticleSystem diePartcles;
	public MolecylType type = MolecylType.Small;
	public MolecylRace race = MolecylRace.White;
	public bool isDummy = false;
	//public List<Rigidbody> linkedRigidbodies = new List<Rigidbody>();
	[HideInInspector]
	public FixedJoint joint;
	[HideInInspector]
	public Molecyl fixedJointTarget;
	[HideInInspector]
	private Collider[] colliders;
	
	private bool isBuffed = false;
	private MolecylView view;
	
	// Use this for initialization
	void Start () {
		if(!isDummy && GetComponent<Rigidbody>().velocity == Vector3.zero) GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
		view = gameObject.GetComponent<MolecylView>();
		view.Animate(this.type, this.race);
		
		if(!isDummy) Game.time.AddTime(0.0f);
		colliders = gameObject.GetComponentsInChildren<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnCollisionEnter(Collision c) {
		if(c.relativeVelocity.sqrMagnitude > 300.0f) Game.sound.Play("collision", c.relativeVelocity.sqrMagnitude / 2000.0f);
		if(!c.rigidbody || isBuffed) return; // No body, not a ball.
		
		Molecyl other = c.rigidbody.GetComponent<Molecyl>();
		if(other.type != this.type || other.race != this.race) return; // No same type, ignoring this shit
		
		
		// Createe & connect joints
		if(!fixedJointTarget && !other.fixedJointTarget) { // No joint, first connection. Let's create one.
			
			joint = gameObject.AddComponent<FixedJoint>();
			other.joint = joint; // Sync both to same joint, no crossjointing.
			joint.connectedBody = other.GetComponent<Rigidbody>();
			
			fixedJointTarget = other;
			other.fixedJointTarget = this;
			
			// Set no collisions between these two guys (optimization)
			foreach(Collider myColl in colliders) {
				foreach(Collider otherColl in other.colliders) {
					Physics.IgnoreCollision(myColl, otherColl);
				}
			}
			
		} else if(other.fixedJointTarget == this) { // Joint present with self. All good.
			
		} else { // Has a joint, but not with me. This guy alrady has a partner. Threesome time!
			
			if(this.fixedJointTarget && other.fixedJointTarget) {
				CreateExtraBall();
			}
			
			Molecyl newMolecyl = CreateBiggerBall();
			
			Transform newMolecylTransform = null;
			if(newMolecyl != null) newMolecylTransform = newMolecyl.transform;
			
			
			if(other.fixedJointTarget) other.fixedJointTarget.PuffTo(newMolecylTransform);
			if(fixedJointTarget) fixedJointTarget.PuffTo(newMolecylTransform);
			other.PuffTo(newMolecylTransform);
			this.PuffTo(newMolecylTransform);
			
			// Set transform parents so they follow
			/*Transform newMolecylTransform = newMolecyl.transform;
			if(other.fixedJointTarget) other.fixedJointTarget.transform.parent = newMolecylTransform;
			if(fixedJointTarget) fixedJointTarget.transform.parent = newMolecylTransform;
			other.transform.parent = newMolecylTransform;
			this.transform.parent = newMolecylTransform;*/
		}
	}
	
	public void PuffTo(Transform target) {
		//Vector3 targetPos = (target == null) ? transform.position : target.position;
		
		this.isBuffed = true;
		GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		
		//float speed = Game.settings.molecylSizes[(int)this.race];
		//StartCoroutine(this.view.SizeTo(0.01f, speed,));
		DestroyMe();
		
		// Give scores
		switch(type) {
			case MolecylType.Small:
				Game.score.Add(1);
				Game.sound.Play("connect", 0.3f);
				break;
			case MolecylType.Medium:
				Game.score.Add(3);
				Game.sound.Play("connect", 0.3f);
				break;
			case MolecylType.Large:
				Game.score.Add(9);
				Game.sound.Play("explosion", 5.0f);
			
				ParticleSystem myDeath = Instantiate(diePartcles, transform.position, transform.rotation) as ParticleSystem;
				myDeath.GetComponent<Renderer>().material.SetColor("_TintColor", Game.settings.molecylColors[(int)race].particleColor);
			
				break;
		}
	}
			
	void DestroyMe() {
		//yield return new WaitForSeconds(0.1f);
		Destroy(gameObject);
	}
	
	public Molecyl CreateBiggerBall() {
		MolecylType spawnType = MolecylType.Small;
		switch(this.type) {
			case MolecylType.Small:
				spawnType = MolecylType.Medium;
				break;
			case MolecylType.Medium:
				spawnType = MolecylType.Large;
				break;
			case MolecylType.Large:
				Game.time.AddTime(20.0f);
				Explode();
				Instantiate(Game.settings.gotTime, transform.position - Vector3.forward * 0.8f, Quaternion.identity);
				return null;
		}
		return Game.ballCreator.CreateMolecyl(spawnType, this.race, transform.position, GetComponent<Rigidbody>().velocity);
	}
	
	public Molecyl CreateExtraBall() {
		return Game.ballCreator.CreateMolecyl(this.type, this.race, transform.position, GetComponent<Rigidbody>().velocity);
	}
	
	void Explode() {
		float radius = 5.0F;
    	float power = 800.0F;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders) {
            if (!hit)
            
			Debug.Log("pum" + hit.GetComponent<Rigidbody>());
            if (hit.GetComponent<Rigidbody>())
                hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 5.0F);
            
        }
		
	}
}

public enum MolecylType {
	Small,
	Medium,
	Large
}

public enum MolecylRace {
	White,
	Red,
	Black
}