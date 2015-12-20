using UnityEngine;
using System.Collections;

public class CreateRandomBalls : MonoBehaviour {
	
	float spawnRate = 0.8f;
	public Transform leftWall;
	public Transform rightWall;
	bool gameIsOver = false;
	
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(0.5f); // To wait until the camera has panned
		Game.infoText.Show("Ready?", ActuallyStart);
		Game.OnGameOver += new Callback(OnGameOver);
	}
	
	void ActuallyStart() {
		StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	IEnumerator Spawn () {
		yield return new WaitForSeconds(spawnRate);
		if(!gameIsOver) {
			spawnRate += (0.2f - spawnRate) * 0.01f;
			Game.ballCreator.CreateMolecyl(
				MolecylType.Small,
				(MolecylRace)Random.Range(0,MolecylRace.GetNames(typeof(MolecylRace)).Length),
				Vector3.up * 10 + Vector3.left * Random.Range(leftWall.position.x + 0.5f, rightWall.position.x - 0.5f),
				Vector3.down
			);
			StartCoroutine(Spawn());
			
		}
	}
	
	void OnGameOver() {
		if(gameIsOver) return;
		gameIsOver = true;
		// käy kaik ballsit läpi ja stoppaa
		foreach(Molecyl m in FindObjectsOfType(typeof(Molecyl)) as Molecyl[]) {
			m.GetComponent<Collider>().enabled = false;
			m.GetComponent<Rigidbody>().isKinematic = true;
		}
		Game.infoText.Show("Great job!\nYou connected\n" + Game.score.value + "\nmjölekyls!", new Callback(EndGame));
	}
	
	void EndGame() {
		Game.loader.LoadAndPanTo("Menu", CameraMenuPosition.EnterScore);
	}
}
