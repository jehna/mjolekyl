using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallCreator : MonoBehaviour {
	
	public Molecyl molecyl;
	
	public Molecyl CreateMolecyl(MolecylType type, MolecylRace race, Vector3 position, Vector3 velocity) {
		Molecyl newMolecyl = Instantiate(molecyl, position, Quaternion.identity) as Molecyl;
		newMolecyl.type = type;
		newMolecyl.race = race;
		newMolecyl.GetComponent<Rigidbody>().velocity = velocity;
		return newMolecyl;
	}
}
