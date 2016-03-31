using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptStatusHolder : MonoBehaviour {
    [Tooltip("This holds a list that is filled during runtime. This list holds the status effects currently affecting this gameobject. Editing allows manual appliaction of effects.")]
    public List<StatusEffectClass> whatStatus = new List<StatusEffectClass>();


	// Use this for initialization
	void Start () {
        //whatStatus = ; //instead, use this on enemies and players to hold what effects they have
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
