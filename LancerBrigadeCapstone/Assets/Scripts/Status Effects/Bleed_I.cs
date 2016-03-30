using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bleed_I : DoTClass {
    //by replacing MonoBehavior with DoTClass, we denote that Bleed_I is a subclass/child of DoTClass
    //this means that anything DoTClass is capable of, Bleed_I is also capable of (Inheritance)
    //this means statusEffectName, statusEffectDuration, and the default constructor are all useable by Bleed_I
    //as are tickInc, dmgPerTick, and the default constructor
    //basically, Bleed_I is where we define what we want bleed to do

    //!!!NOTE: the StatusEffectClass list is not working correctly, but code works without it 

    public Bleed_I()
    {
        //if no other values are passed through, the system will assume you want a statusEffectClass default constructor
        
    }

    public Bleed_I(string name, float duration, float curDur, bool stackable, int mxStacks, /*List<StatusEffectClass> myStacks,*/ GameObject myEntity, int DPT, float tInc)
    {
        this.statusEffectName = name;
        this.statusEffectDuration = duration;
        this.currentDuration = curDur;
        this.isStackable = stackable;
        this.maxStacks = mxStacks;
        //this.stacks[0] = this.gameObject.GetComponent<DoTClass>();
        this.entity = myEntity;
        this.dmgPerTick = DPT;
        this.tickInc = tInc;

    }

    void OnEnable()
    {
        //base.Start();
        StartUp();
        Debug.Log("bleedstart");
        InvokeRepeating("Tick", tickInc, tickInc);
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
