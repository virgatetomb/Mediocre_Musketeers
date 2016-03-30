using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoTClass : StatusEffectClass {
    //this is meant to be a gap filler class, for DoT to pull data from; it is not meant to be independent, but can be
    [Tooltip("How much damage per tick does this DoT do? (int)")]
    public int dmgPerTick = 1; //how much damage there is per tick; damage not applied until first tick
    [Tooltip("How many seconds between damage instances? (float)")]
    public float tickInc = 1; //how many seconds delay there is between each instance of damage
    //public bool targetAlive = true; //check if the target is alive
    
    //!!!NOTE: the StatusEffectClass list is not working correctly; code is functional without

    public DoTClass()
    {
        //if no other values are passed through, the system will assume you want a statusEffectClass default constructor
    }

    public DoTClass(string name, float duration, float curDur, bool stackable, int mxStacks, /*List<StatusEffectClass> myStacks,*/ GameObject myEntity, int DPT, float tInc)
    {
        this.statusEffectName = name;
        this.statusEffectDuration = duration;
        this.currentDuration = curDur;
        this.isStackable = stackable;
        this.maxStacks = mxStacks;
        //this.stacks[0] = this.gameObject.GetComponent<DoTClass>();
        this.entity = myEntity;
        dmgPerTick = DPT;
        tickInc = tInc;
        
    }

    void OnEnable()
    {
        Debug.Log("dotenable");
        StartUp();
    }

	// Use this for initialization
	void Start () {
        //base.Start();
        Debug.Log("dotclasstart");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Tick()
    {
        
        
        if(this.isPlayer)
        {
            pData.TakeDamage(dmgPerTick);
        }
        else
        {
            eData.health -= dmgPerTick;
            Debug.Log("bleeding; current hp:" + eData.health);
        }
        
    }

    public void OnDisable()
    {
        //insert code to stop each coroutine; it prevents oopsies
        Debug.Log("bleed" + name + "disabled");
        CancelInvoke("IncrementDuration");
        CancelInvoke("Tick");
    }
}
