using System.Collections;
using UnityEngine;

public class Boss_Die : MonsterState
{
    private Monster monsterComponent;
    private Animator monsterAni;

    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, BossStateMachine bossFSM_)
    {
        bossFSM_.StartCoroutine(DoDie(monster_));
    }

    public override void OnStateExit(BossStateMachine bossFSM_)
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
    }
    #endregion

    #region 죽음
    private IEnumerator DoDie(GameObject monster_)
    {
        // 애니메이션 재생
        monsterAni.SetTrigger(monsterComponent.deadID);

        // 애니메이션이 모두 끝날 때까지 대기
        yield return new WaitUntil(() => monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        // 아이템 드랍하기
        GetItemFromClientDB(monster_);
    }
    #endregion

    #region 변수 비우는 메서드
    private void CleanVariables()
    {
        monsterComponent = default;
        monsterAni = default;
    }
    #endregion

    #region 내부 Item Dictionary 에서 랜덤한 아이템 얻어오는 메서드
    private void GetItemFromClientDB(GameObject monster_)
    {
        GameObject testItem = GameObject.Instantiate(ItemDataBase.Instance.fieldItemPrefab,
            monster_.transform.position, Quaternion.identity);

        testItem.GetComponent<FieldItem>().SetItem(ItemDataBase.Instance.itemDB[7]);
    }
    #endregion
}
