using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptAttackHolder : MonoBehaviour {
    [Tooltip("A list of attacks this enemy can make. To add more, incrememnt the \"Size\" variable by 1 and press enter. Then drag the attack prefab into the new \"Element\" space. \nCurrently, enemies only use the first attack in this list.")]
    public List<AttackClass> whatAttack;


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
