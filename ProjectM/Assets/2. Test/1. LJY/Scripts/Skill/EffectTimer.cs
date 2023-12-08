using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EffectTimer : MonoBehaviour
{
    private ParticleSystem particle;
    public float offTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        Invoke("OffParticle", offTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void OffParticle()
    {
        var particleLoop = particle.main;
        particleLoop.loop = false;
    }
}
