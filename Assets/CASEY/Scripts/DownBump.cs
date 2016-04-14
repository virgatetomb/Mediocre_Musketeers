using UnityEngine;
using System.Collections;

public class DownBump : MonoBehaviour {

    public Rigidbody PlayerBody;
    public float stompMe = -1f;
    public GameObject explody;
    public float testchange = 0f;
    public float spawnHeight = .1f;

	// Use this for initialization
	void Start () {
        //get rigid body

        PlayerBody = GetComponent<Rigidbody>();
	}
	void Update()
    {
        testchange = PlayerBody.velocity.y;

    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("collide");
        Debug.Log(PlayerBody.velocity.y);

        if (testchange <= stompMe)
      {
            ContactPoint here = collision.contacts[0];
            Quaternion rot = Quaternion.identity;
            Vector3 pos = here.point;
            pos.y = pos.y + spawnHeight;
            Instantiate(explody, pos, rot);

    }

    }
    
}
