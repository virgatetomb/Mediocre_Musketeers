using UnityEngine;
using System.Collections;

public class TestingClosestPoint : MonoBehaviour {

    public Collider pcColld;
    public Vector3 targv3;
    public float fracJourney = 0f;
    float distcovered = 0f;
    public float lerpTime = 1f;
    public float currentLerpTime;

    // Use this for initialization
    void Start () {
        Debug.Log(pcColld.bounds.ClosestPoint(transform.position));

	}
	
	// Update is called once per frame
	void Update () {
        targv3 = pcColld.bounds.ClosestPoint(transform.position);
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        fracJourney = currentLerpTime / lerpTime;
        //insert separation functionality
        transform.position = Vector3.MoveTowards(transform.position, targv3, fracJourney * 1);
    }
}
