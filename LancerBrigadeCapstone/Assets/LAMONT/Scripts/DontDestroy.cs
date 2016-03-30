using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	private static DontDestroy menuMusic;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this);

		if (menuMusic == null) {
			menuMusic = this;
		} else {
			DestroyObject (gameObject);
		}
	}
}
