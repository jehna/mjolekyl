using UnityEngine;
using System.Collections;

public class MolecylHaloExperiementRotator : MonoBehaviour {
	
	public string materialName = "_Halo1Tex";
	public Vector2 amount;
	
	private Material m;
	private Vector2 startVector;
	
	// Use this for initialization
	void Start () {
		m = GetComponent<Renderer>().material;
		startVector = m.GetTextureOffset(materialName);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate((Vector3.up * 40)*Time.deltaTime, Space.Self);
		startVector = startVector + amount * Time.deltaTime;
		m.SetTextureOffset(materialName, startVector);
	}
}
