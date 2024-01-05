using System.Collections;
using UnityEngine;

public class Monster_Die : MonsterState
{
    private Monster monsterComponent;
    private Animator monsterAni;

    private int randomNum;

    private ObjectPool monsterPool;
    private ObjectPool dieEffectPool;
    private ParticleSystem dieEffect;



    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoDie(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
        CleanVariables();
    }




    #region 초기화
    private void Init(GameObject monster_)
    {
        // 몬스터 캐릭터 컨트롤러
        monsterComponent = monster_.GetComponent<Monster>();
        // 몬스터 애니메이터
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();
        // 몬스터 애니메이터 속도 복구
        monsterAni.speed = 1;
        // 죽음 애니메이션 인덱스 번호
        randomNum = Random.Range(0, 2);
        // 몬스터 풀
        monsterPool = monster_.transform.root.GetComponent<ObjectPool>();
        // 죽음 애니메이션 풀
        dieEffectPool = GameObject.Find("Pool_Effect_Die").GetComponent<ObjectPool>();
    }
    #endregion

    #region 죽음
    private IEnumerator DoDie(GameObject monster_, MonsterStateMachine msm_)
    {
        // 애니메이션 재생
        monsterAni.SetInteger("DieNumber", randomNum);
        monsterAni.SetTrigger(monsterComponent.deadID);

        // 애니메이션이 모두 끝날 때까지 대기
        yield return new WaitUntil(() => monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        // 애니메이션 끝나면 이펙트 재생
        yield return msm_.StartCoroutine(PlayEffect(monster_));
    }
    #endregion

    #region 죽고서 이펙트 재생 + 오브젝트 풀로 복귀하는 코루틴
    private IEnumerator PlayEffect(GameObject monster_) 
    {
        // 죽음 이펙트를 가져오기
        dieEffect = dieEffectPool.ActiveObjFromPool(monster_.transform).GetComponent<ParticleSystem>();
        dieEffect.Play();

        // 몬스터 스케일 축소
        monster_.transform.localScale = Vector3.zero;

        // 이펙트 재생이 끝날 때까지 대기하기
        yield return new WaitUntil(() => !dieEffect.isPlaying);

        // 아이템 드랍하기
        //GetItemFromClientDB(monster_);

        // 이펙트 반납하기
        dieEffectPool.ReturnObjToPool(dieEffect.gameObject);

        // 몬스터 풀로 돌아가기
        monsterPool.ReturnObjToPool(monster_);
    }
    #endregion

    #region 변수 비우는 메서드
    private void CleanVariables()
    {
        monsterComponent = default;
        monsterAni = default;
        randomNum = default;
        monsterPool = default;
        dieEffectPool = default;
        dieEffect = default;
    }
    #endregion

    #region 내부 Item Dictionary 에서 랜덤한 아이템 얻어오는 메서드
    private void GetItemFromClientDB(GameObject monster_)
    {
        GameObject testItem = GameObject.Instantiate(ItemDataBase.Instance.fieldItemPrefab,
            monster_.transform.position, Quaternion.identity);

        testItem.GetComponent<FieldItem>().SetItem(ItemDataBase.Instance.itemDB[Random.Range(0, 13)]);
    }
    #endregion
}
