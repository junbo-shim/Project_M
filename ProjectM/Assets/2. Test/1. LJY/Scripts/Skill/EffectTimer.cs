using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    private ParticleSystem particle;
    private PowerPoison powerPoison;
    private float offTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<PowerPoison>() != null)
        {
            powerPoison = GetComponent<PowerPoison>();
            offTime = powerPoison.duration;
        }
        particle = GetComponent<ParticleSystem>();
        Invoke("OffParticle", offTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!particle.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void OffParticle()
    {
        var particleLoop = particle.main;
        particleLoop.loop = false;
    }
}
