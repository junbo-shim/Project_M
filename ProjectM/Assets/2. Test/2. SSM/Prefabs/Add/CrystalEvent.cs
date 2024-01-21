using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEvent : MonoBehaviour
{
    public bool PlayerGrap;
    private bool Operationcompleted;
    [SerializeField] public List<ParticleSystem> crystalParticleSystem;
    private Rigidbody rb;
    private ParticleSystem particleSystemInstance;
    private GameObject childObj;
    private int rnadnum;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerGrap && !Operationcompleted )
        {
            Operationcompleted = true;
            rnadnum = Random.Range(0, crystalParticleSystem.Count);

            particleSystemInstance = Instantiate(
                crystalParticleSystem[rnadnum],
                transform.position, Quaternion.identity); // 파티클 시스템 자식으로 생성

            particleSystemInstance.transform.parent = transform;//부모 설정
            if(rnadnum > 0)
            {
                childObj = transform.GetChild(0).gameObject;
                childObj.AddComponent<PaticleEvent_SSM>();

                var collisionModule = particleSystemInstance.collision; // 파티클 콜리션 모듈 추가
                collisionModule.enabled = true;
                collisionModule.type = ParticleSystemCollisionType.World;
                collisionModule.sendCollisionMessages = true;
            }
         


            particleSystemInstance.Play();
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            if(particleSystemInstance.main.loop)
            {
                Invoke("DeactivateParticleSystem", 6);
            }
            else
            {
                Invoke("DeactivateParticleSystem", particleSystemInstance.main.duration);
            }
         

        }

    }


    public void onGrap()
    {
        PlayerGrap = true;
    }

    private void DeactivateParticleSystem()
    {
        particleSystemInstance.Stop();
        gameObject.SetActive(false);
    }

   
}
