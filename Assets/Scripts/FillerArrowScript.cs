using UnityEngine;
using System.Collections;

public class FillerArrowScript : AttackClass {
    [Tooltip("The speed at which this projectile should move. This is filtered through the phyics engine and then force is applied, so these speeds will not match kinematic movement of the same speed.")]
    public float speed = 30;
    [Tooltip("Used for determining a target area. Currently unimplemented.")]
    public XCharacterControllerLancer playerScript; //use this later to create a target zone
    [Tooltip("The rigidbody on this object.")]
    public Rigidbody thisRigid;
    [Tooltip("The attack holder script of this object.")]
    public ScriptAttackHolder attackArray;

	// Use this for initialization
	void Start () {
        if (attackName != "Arrow")
        {
            for (int i = 0; i < attackArray.whatAttack.Capacity; i++)
            {
                if (attackArray.whatAttack[i].attackName == "Arrow")
                {
                    attackName = attackArray.whatAttack[i].attackName;
                    attackDamage = attackArray.whatAttack[i].attackDamage;
                    attackIsRanged = attackArray.whatAttack[i].attackIsRanged;
                    attackSpeed = attackArray.whatAttack[i].attackSpeed;
                    attackStatusToApply = attackArray.whatAttack[i].attackStatusToApply;
                    attackRecoveryFrames = attackArray.whatAttack[i].attackRecoveryFrames;
                    attackPlaceholderAnimFrames = attackArray.whatAttack[i].attackPlaceholderAnimFrames;
                    break;
                }

            }
        }
        speed = attackSpeed;
        thisRigid.AddForce(transform.forward * speed *150);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is one more arrow towards being a porcupine.");
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
        }
            
        if (other.tag != "RangedRadius" && other.tag != "Untagged")
        { }
        
            StartCoroutine("TimedKill");
    }

    IEnumerator TimedKill()
    {

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        
    }
}
