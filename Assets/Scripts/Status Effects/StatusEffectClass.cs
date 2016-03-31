using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffectClass : MonoBehaviour {
    //this class is intentionally left small; create a subclass of this one for each status effect; this will allow more flex and less error
    //including example version for clarification: "Bleed I"
    //public List<StatusEffectClass> whatEffect = new List<StatusEffectClass>(5);
    [Tooltip("The name of this status effect. Use anything you want, including spaces.")]
    public string statusEffectName; //the name of the status effect; can be used for output
    [Tooltip("How long, in seconds, does this effect last? (float)")]
    public float statusEffectDuration; //how long the effect lasts; each subclass will have their own use for this
    [Tooltip("Can this status effect be stacked?")]
    public bool isStackable; //if true, the effects can stack; otherwise, new applications refresh; this is here to catch problems
    [Tooltip("How many stacks can this status affect have on a single target, at most? Use 0 if this effect cannot stack.")]
    public int maxStacks; //if stackable, how many is the maximum;
    [Tooltip("How long, in seconds, has this effect been running already? (float)")]
    public float currentDuration; //how long has the effect been running
    [Tooltip("DO NOT EDIT! Is the duration being properly checked?")]
    public bool isDurationRunning = false; //is the IncrementDuration coroutine running?
    [Tooltip("DO NOT EDIT! What game object is going to have the status effect applied to it?")]
    public GameObject entity; //whenever an attack with a status effect hits, get the data name of the gameobject hit and put it in here
    //this is crucial or else we cannot access the values of enemies and players correctly
    [Tooltip("DO NOT EDIT! What ScriptEnemyClass will be referenced by this effect?")]
    public ScriptEnemyClass eData; //this will be assigned at start of this class, it sets the ScriptEnemyClass if the effect is applied to an enemy
    [Tooltip("DO NOT EDIT! What PlayerStatScript will be referenced by this effect?")]
    public PlayerStatScript pData; //this will be assigned at start of this class, it sets the PlayersStatScript if the effect is applied to a player
    [Tooltip("DO NOT EDIT! Has this effect been applied to a player?")]
    public bool isPlayer; //has this been applied to a player
    [Tooltip("DO NOT EDIT! Is the entity safe to apply this affect to?")]
    public bool isSafe; //is this status okay to run? if yes, true; if it could cause an error, false;
    [Tooltip("Checks to see if the target already has an instance of this effect.")]
    public bool hasAStack; //this checks to see if the target has a stack of this effect already
    [Tooltip("CAUTION - is this the first time this status effect has run Awake?")]
    public bool firstSpawn = true; //used in onAwake to turn off the script until Attack class can run
    [Tooltip("CAUTION - This will hold a copy of whatever status effect this is.")]
    public StatusEffectClass myClone; //myclone is a clone of whichever statuseffect this is
    [Tooltip("How many stacks does the afflicted have?")]
    public List<StatusEffectClass> stacks;  //this holds how many stacks someone has 


    public StatusEffectClass() //default constructor; if no values passed in, this is used when new instances are created
    {
        statusEffectName = "Placeholder";
        statusEffectDuration = 1;
        isStackable = false;
        maxStacks = 0;
        currentDuration = 0;
        
        //entity = null;
        isSafe = false;
    }

    public StatusEffectClass(string name, float duration, float curDur, bool stackable, int mxStacks/*, GameObject theEntity*/) //please use this constructor instead
    {
        statusEffectName = name;
        statusEffectDuration = duration;
        currentDuration = curDur;
        isStackable = stackable;
        maxStacks = mxStacks;
        
        //entity = theEntity;
        isSafe = true;
    }
    public void Start()
    {
        //doesn't do anything
        Debug.Log("statusstart");
    }

	// Use this for initialization
    public void Awake()
    {
        Debug.Log("onawake");
        if(firstSpawn)
        {
            firstSpawn = false;
            Debug.Log(this.gameObject);
            this.enabled = false;
        }
        else
        {
            
        }
        
    }

	public void OnEnable () {
        StartUp();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IncrementDuration()
    {
        //yield return new WaitForSeconds(0.1f);
        currentDuration += 0.1f;
        if(currentDuration >= statusEffectDuration)
        {
            isDurationRunning = false;
            //stacks.Clear();
            RemoveStatusEffect();
            Destroy(this.gameObject);
            CancelInvoke("IncrementDuration");
        }
    }

    public void ResetDuration (bool isRun)
    {
        Debug.Log("resetduration entered");
        if (isRun)
            currentDuration = 0f;
    }


    //do not call AddStack, it doesn't work the way it needs to at the moment
    public void AddStack(bool isRun, List<StatusEffectClass> whatStacks, bool isStacks, int maxStacks) //use me to actually add a new instance of this
    {   //i.e. On an attack, check to see if the target has a copy of the status effect already, if so use AddStack with [0]'s data, otherwise use the normal constructor
        if(isStacks && whatStacks.Count < maxStacks)
        {
            whatStacks.Add(this.gameObject.GetComponent<StatusEffectClass>());
            foreach(StatusEffectClass element in whatStacks)
            {
                ResetDuration(isRun);
            }
        }
        else if(isStacks)
        {
            foreach(StatusEffectClass element in whatStacks)
            {
                ResetDuration(isRun);
            }
        }
        else if(whatStacks.Count < 1)
        {
            whatStacks.Add(this.gameObject.GetComponent<StatusEffectClass>());
        }
    }

    //instead use this for adding StatusEffects
    public void AddStatusEffect( GameObject attack)
    {
        
        int inLoop = 0;
        foreach(StatusEffectClass statToApp in attack.gameObject.GetComponent<AttackClass>().attackStatusToApply)
        {
            //Debug.Log("statustoapply" + statToApp.statusEffectName);
            inLoop = 0;
            foreach(StatusEffectClass statsHad in entity.GetComponent<ScriptStatusHolder>().whatStatus)
            {
                if (statsHad.enabled != true)
                    statsHad.enabled = true;
                if (statToApp.statusEffectName == statsHad.statusEffectName)
                {
                    Debug.Log(statToApp.statusEffectName + " == " + statsHad.statusEffectName);
                    inLoop = entity.GetComponent<ScriptStatusHolder>().whatStatus.Count + 1;
                    
                }
                inLoop++;
                //Debug.Log("inloop Omega " + inLoop);
            }
            //Debug.Log("inloop" + inLoop);
            
            if(inLoop == entity.GetComponent<ScriptStatusHolder>().whatStatus.Count)
            {
                Debug.Log("neweffectadded");
                myClone = this;
                entity.GetComponent<ScriptStatusHolder>().whatStatus.Add(myClone);
                
                Debug.Log(entity.GetComponent<ScriptStatusHolder>().whatStatus[0].enabled);
                foreach (StatusEffectClass enableStat in entity.GetComponent<ScriptStatusHolder>().whatStatus)
                {
                    if (enableStat.enabled)
                        enableStat.enabled = false;
                    enableStat.enabled = true;
                }
                Debug.Log("myClonedata" + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].name + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].statusEffectName + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].statusEffectDuration + " edata " + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].eData + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].entity.name + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].enabled);
                //Debug.Log("Statuseffectdata" + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].statusEffectName + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].statusEffectDuration + entity.GetComponent<ScriptStatusHolder>().whatStatus[0].entity.name);
            }                      
            
            else if(inLoop > entity.GetComponent<ScriptStatusHolder>().whatStatus.Count)
            {
                foreach (StatusEffectClass statsHad in entity.GetComponent<ScriptStatusHolder>().whatStatus)
                    statsHad.ResetDuration(isDurationRunning);
            }
            //Debug.Log("inloop 2" + inLoop);
        }
        


    }

    public void RemoveStatusEffect()
    {
        
        entity.GetComponent<ScriptStatusHolder>().whatStatus.RemoveAll(x => x.statusEffectName == this.statusEffectName);

    }

    public void SetEntityData(GameObject other)
    {
        this.entity = other;
    }
    
    public void StartUp()
    {
        //Debug.Log("statuseffectclass enabled");
        //stacks = new List<StatusEffectClass>(2);


        switch (entity.name)
        {
            case "PlayerCharacter(Unit2)":
                pData = entity.GetComponentInChildren<PlayerStatScript>();
                isPlayer = true;
                break;
            case "MeleeEnemy":
            case "RangedEnemy":
                eData = entity.GetComponentInChildren<ScriptEnemyClass>();
                isPlayer = false;
                break;
            default:
                isPlayer = false;
                //isSafe = false;
                break;
        }
        //if (isSafe != true)
        //{
        //    Destroy(this.gameObject); //this does one thing: removes any unsafe object from existance 
        //}
        //if(!isPlayer)
        //{
        //    if(eData.statHold.whatStatus.FindIndex(statusEffectName)
        //}

        InvokeRepeating("IncrementDuration", 0, 0.1f);
        isDurationRunning = true;
    }

    public void OnDisable()
    {
        CancelInvoke("IncrementDuration");
    }
}
