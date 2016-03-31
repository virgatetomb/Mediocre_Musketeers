using UnityEngine;
using System.Collections;

public class ScriptEnemyClass : MonoBehaviour
{
    //insert tooltip here: "Which type of enemy is this?"
    
    public enum EnemyType {BasicMelee, BasicRanged, MidMelee, MidRanged, Boss };
    //insert tooltip: "How far away can this enemy detect the player?"
    [Tooltip("Distance this enemy can detect player.")]
    public int sightRange;
    //tooltip: "How far away can the enemy strike the player?"
    [Tooltip("How far away can this enemy strike the player?")]
    public SphereCollider attackRange;
    //tooltip
    //[Tooltip("What is the animation of the attack?")] //these two mean nothing since attacks now have indivual animations
    //public Animation attackAnimation;
    [Tooltip("What animation is played when this enemy dies?")]
    public Animation deathAnimation;
    //[Tooltip("asdf")]
    //public int attackDamage; //perhaps this is irrelevant at this point as each attack has its own damage value now; still it can be used for other things
    [Tooltip("How much health does this enemy have?")]
    public int health = 1;
    [Tooltip("How much health does this enemy start with?")]
    public int startingHealth = 100;
    [Tooltip("How much armor does this enemy have?")]
    public int armor;
    [Tooltip("How fast can this enemy move?")]
    public float speed;
    //tooltip: "How many frames of delay does this enemy have before it can react to the player?"
    [Tooltip("How many frames of delay does this enemy have before it can react to the player?")]
    public int reactionTime;
    //tooltip: "Can this enemy cancel its animations?"
    [Tooltip("Can this enemy cancel its animations?")]
    public bool canFrameCancel;

    //public Projectile[10];
    [Tooltip("This enemy's detection script:")]
    public ScriptDetectionRadius detector;
    [Tooltip("This enemy's movement script:")]
    public ScriptEnemyMovement eMove;

    //inform designers to not fuck with these variable
    [Tooltip("DO NOT EDIT! Is this enemy's movement script running?")]
    public bool isMovementRunning = false;
    [Tooltip("DO NOT EDIT! Is this enemy's attack script running?")]
    public bool isAttackRunning = false;

    [Tooltip("Holds all instances of status effects to be applied to this enemy.")]
    public ScriptStatusHolder statHold;


	// Use this for initialization
	void Start () {
        eMove = GetComponent<ScriptEnemyMovement>();
        detector = GetComponentInChildren<ScriptDetectionRadius>();
		health = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//wtf, this is instantly triggering...
		if (health <= -1)
			Destroy (this.gameObject);
	}

    

    public IEnumerator EnemyBasicMovement()
    {
        Debug.Log("EnemyBasicMovement");
        if(isMovementRunning != true && eMove.isMovementRunning != true)
        {
            eMove.enabled = true;
        }
        //includes functionality of movement
        isMovementRunning = true;
        //this.gameObject.transform.Translate(detector.thePlayer.position);
        //do the movmement with lerping instead
        eMove.isMovementRunning = true;
        return null;
    }

    public IEnumerator EnemyBasicAttack()
    {
        //includes functionality of attacking
        isAttackRunning = true;
        return null;
    }

    public void EnemyRemoval()
    {
        //includes functionality of removing dead enemies from the game
        Destroy(this.gameObject);
    }


}
