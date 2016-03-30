using UnityEngine;
using System.Collections;

public class DestroyMenuMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (GameObject.Find ("Menu_Music"));
	}
}
