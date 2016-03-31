using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackClass : MonoBehaviour {
    //add in an array to hold AttackClass objects or separate object to hold said array
    [Tooltip("The name of this attack. Used anything you want, including spaces.")]
    public string attackName;
    [Tooltip("How much damage does this attack do?")]
    public int attackDamage;
    //[Tooltip("What is this attack's animation?")]
    //public Animation attackAnim;
    [Tooltip("Is this attack ranged?")]
    public bool attackIsRanged;
    [Tooltip("How fast does this attack travel?")]
    public float attackSpeed;
    [Tooltip("A list of status effects for this attack to apply. To add more, incrememnt the \"Size\" variable by 1 and press enter. Then drag the status effect prefab into the new \"Element\" space.")]
    public List<StatusEffectClass> attackStatusToApply;
    //public GameObject attackSpawnedBy; //which gameobject spawned this attack
    [Tooltip("What object will be spawned when this attack hits? The object can/should include particles, sound, etc.")]
    public GameObject attackOnHitObject; //what game object to spawn when hitting
    //public GameObject attackOnMissObject; //what game object to spawn on miss
    [Tooltip("Attach the collider of this attack here.")]
    public Collider attackHitbox; //what is the collider that this attack possesses
    [Tooltip("How many frames are there after this attack before the entity that spawned it can attack again?")]
    public int attackRecoveryFrames; //how many frame delay is there on the enemy before they can attack again
    [Tooltip("How many frames long is the actual attack? Use this to substitute for an animation if you don't have one.")]
    public int attackPlaceholderAnimFrames; //use this to fake an animation by delaying the attack start by this many frames
    [Tooltip("This holds the script that the Attack uses. Currently: FillerAttackScript only.")]
    //public AttackScript atkScript;
    public FillerAttackScript atkScript;
    [Tooltip("This holds the default version of this attack. It is created when the default constructor is called.")]
    GameObject instantiationDefaults; //this variable is filled by a gameobject whose sole purpose is for holding default data

    public AttackClass()
    {
        attackName = "";
        attackDamage = 1;
        //attackAnim = GetComponent<Animation>(); //do not uncomment until animations exist
        attackIsRanged = false;
        attackSpeed = 1;
        attackStatusToApply = new List<StatusEffectClass>();
        //not sure how attackSpawnedBy even works since attack class only shows up when an object spawns it using a non-default constructor
        //!!!insert code for what particle effects to spawn!!!

        //insert default collider -- handled by default attack object
        attackRecoveryFrames = 15;
        attackPlaceholderAnimFrames = 60;
        
    }
	// Use this for initialization
	void Start () {
        atkScript = gameObject.GetComponent<FillerAttackScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        //Debug.Log(gameObject.GetComponent<Collider>());
        if (gameObject.GetComponent<Collider>() != null && gameObject.GetComponent<Rigidbody>() == null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(gameObject.GetComponent<Collider>().bounds.center, gameObject.GetComponent<Collider>().bounds.size);
        }
        if(this.gameObject.name == "Attack - TestMelee(Clone)" || this.gameObject.name == "Attack - MeleeBleed(Clone)")
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(gameObject.GetComponent<Collider>().bounds.center, gameObject.GetComponent<Collider>().bounds.size);
        }
    }

    public void AttackRemoveAttack()
    {
        //use this function to self delete the attack created once it is done
        //run once atkScript has been run
    }

    void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.GetComponent<Collider>().enabled != true)
        {
            this.gameObject.GetComponent<Collider>().enabled = true;
        }
        Debug.Log("collidedwith" + other.name);
        Debug.Log(other.gameObject.tag);
        switch(other.gameObject.tag)
        {
            case "Player":
                Debug.Log("hitplayer");
                other.gameObject.GetComponent<PlayerStatScript>().TakeDamage(attackDamage);
                Debug.Log(other.gameObject);
                Debug.Log(GetComponent<Collider>().enabled + "attacktemplateenabled");
                attackOnHitObject.SetActive(true);
                Debug.Log(GetComponent<Collider>().enabled + "attacktemplateenabled");
                if (attackStatusToApply.Count > 0)
                {
                    foreach (StatusEffectClass whatEffect in attackStatusToApply)
                    {
                        Debug.Log("whateffect" + whatEffect.statusEffectName);
                        whatEffect.entity = other.gameObject;
                    }
                    attackStatusToApply[0].AddStatusEffect(gameObject);
                }
                
                //atkScript.newAttack = null;
                //SDestroy(this.gameObject);
                break;
            default: //if not one of the above cases
                break;
        }
        //if(other.gameObject.name == "MeleeEnemy") //including for testing purposes, don't use because enemies will auto kill themselves 
        //{
        //    if (attackStatusToApply.Count > 0)
        //    {
        //        foreach (StatusEffectClass whatEffect in attackStatusToApply)
        //        {
        //            //Debug.Log("whateffect" + whatEffect.statusEffectName + "entity hit " + other.gameObject);
        //            whatEffect.entity = other.gameObject;
        //        }
        //        attackStatusToApply[0].AddStatusEffect(gameObject);
        //    }
        //    Destroy(this.gameObject);
        //}
    }

    void OnDestroy()
    {
        Debug.Log("Deadattack");
    }
}
