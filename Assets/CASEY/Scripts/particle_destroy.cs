using UnityEngine;
using System.Collections;


public class particle_destroy : MonoBehaviour {

    public ParticleSystem ParticleSystem;

    void Start()
    {
        Destroy(gameObject, ParticleSystem.duration);
    }
}
