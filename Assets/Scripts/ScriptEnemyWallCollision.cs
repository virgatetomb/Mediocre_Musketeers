using UnityEngine;
using System.Collections;

public class ScriptEnemyWallCollision : MonoBehaviour {
    [Tooltip("This enemy's detection script.")]
    public ScriptDetectionRadius detect;
    [Tooltip("This enemy's movement script.")]
    public ScriptEnemyMovement eMove;
    [Tooltip("LayerMask; holds the value of the layerMask for walls.")]
    public LayerMask wallMask = 8;
    [Tooltip("Is this enemy close to or colliding with a wall?")]
    public bool isWallCollision = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("wallhit" + other.name);
        if (other.tag == "Barrier")
        {
            isWallCollision = true;
            //eMove.isMovementRunning = false;
            //fancy version
            //Debug.Log("eCollideWall");
            if (eMove.isMovementRunning)
            {
                
                RaycastHit wallRay;
                if(Physics.Raycast(this.gameObject.transform.position, eMove.playerLoc, out wallRay, detect.detectSphere.radius))
                {
                    Debug.Log(wallRay.collider + " " + wallRay.distance);
                    Debug.Log(other.tag);
                    if (wallRay.collider.transform.tag == "Barrier")
                        eMove.isMovementRunning = false;
                }
                //Debug.Log("nothittingawall");
                
            }

        }
    }

    //void OnCollisionStay(Collision other)
    //{
    //    //Debug.Log("hitting something");
    //    //Debug.Log(other.collider.name);
    //    if (other.collider.tag == "Barrier")
    //    {
    //        Debug.Log("wallstillhitting");
    //        //fancy version
    //        //Debug.Log("eCollideWall");
    //        //if (eMove.isMovementRunning)
    //        //{
    //        //    RaycastHit wallRay;
    //        //    Physics.Raycast(this.gameObject.transform.position, eMove.playerLoc, out wallRay, detect.detectSphere.radius, wallMask);
    //        //    Debug.Log(wallRay.collider + " " + wallRay.distance);
    //        //    if (wallRay.collider.tag == "Barrier")
    //        //        eMove.isMovementRunning = false;
    //        //}
    //        //else
    //        //{
    //        //    RaycastHit wallRay;
    //        //    Physics.Raycast(this.gameObject.transform.position, eMove.playerLoc, out wallRay, detect.detectSphere.radius, wallMask);
    //        //    if(wallRay.collider.tag != "Barrier")
    //        //    {
    //        //        eMove.enabled = false;
    //        //        eMove.playerCollider = detect.colliderHolder;
    //        //        eMove.isMovementRunning = true;
    //        //        eMove.enabled = true;
    //            //}
    //       // }

    //    }
    //}
}
