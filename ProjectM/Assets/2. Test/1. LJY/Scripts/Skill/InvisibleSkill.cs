using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvisibleSkill : SkillAction
{
    private Transform playerRigid;
    private PlayerHandManager phm;
    private NoDamage skillInfo;
    private float duration;
    private float invisibleTime;

    // Start is called before the first frame update
    void Start()
    {
        InputInfo();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        phm = player.transform.GetChild(0).GetComponent<PlayerHandManager>();
        playerRigid = player.transform.GetChild(0).GetChild(2);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(phm != null)
        {
            StartCoroutine(UseInvisible());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InputInfo()
    {
        skillInfo = ReturnInfo("Invisibility") as NoDamage;
        duration = skillInfo.SkillDuration;
    }

    // 투명 스킬 해제
    public void EndInvisible()
    {      
        if(playerRigid.gameObject.layer.Equals(LayerMask.NameToLayer("Invisible")))
        {
            Debug.Log("투명화 종료 함수 작동");
            phm.ChangeHandMat(false);
            playerRigid.gameObject.layer = LayerMask.NameToLayer("Player");
            gameObject.SetActive(false);
        }
    }

    public IEnumerator UseInvisible()
    {
        phm.ChangeHandMat(true);
        playerRigid.gameObject.layer = LayerMask.NameToLayer("Invisible");
        yield return new WaitForSeconds(5f);
        EndInvisible();       
    }
}
