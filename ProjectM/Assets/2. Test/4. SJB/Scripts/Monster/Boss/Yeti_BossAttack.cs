using System.Collections;
using UnityEngine;

public class Yeti_BossAttack : MonsterState
{
    private CharacterController monsterControl;
    private Yeti_Boss monsterComponent;
    private Animator monsterAni;
    private float speed;
    private float gravity;
    private float radius;
    public GameObject target;

    private float shootCooltimer;
    private float chargeCooltimer;

    private float shootCooltime;
    private float chargeCooltime;

    private float punchRange;
    private float shootRange;
    private float chargeRange;

    private WaitForSeconds oneSecond;

    private ObjectPool projectilePool;
    //private Vector3 chargeRot;

    private SphereCollider punchCollider;
    private BoxCollider chargeCollider;

    private WaitForSeconds punchAniDelay;
    private WaitForSeconds shootAniDelay;
    private WaitForSeconds chargeAniDelay_1;
    private WaitForSeconds chargeAniDelay_2;


    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(BossStateMachine bossFSM_)
    {
        bossFSM_.StartCoroutine(DoCycle(bossFSM_));
    }

    public override void OnStateExit(BossStateMachine bossFSM_)
    {
        bossFSM_.StopCoroutine(SetProjectile());
        bossFSM_.StopCoroutine(Punch(bossFSM_));
        bossFSM_.StopCoroutine(Shoot(bossFSM_));
        bossFSM_.StopCoroutine(Charge(bossFSM_));
        bossFSM_.StopCoroutine(DoCycle(bossFSM_));

        CleanVariables();
    }



    #region 초기화
    private void Init(GameObject monster_) 
    {
        // 몬스터 캐릭터 컨트롤러
        monsterControl = monster_.GetComponent<CharacterController>();
        monsterComponent = monster_.GetComponent<Yeti_Boss>();
        // 몬스터 애니메이터
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();

        // 몬스터 이동 속도, 중력값, 타겟
        speed = monsterComponent.monsterData.MonsterMoveSpeed;
        gravity = -9.81f;
        radius = 100f;

        // 보스의 쿨타임 체크용 변수
        shootCooltimer = default;
        chargeCooltimer = default;

        // 보스 패턴 쿨타임
        shootCooltime = monsterComponent.monsterData.Skill2Cooltime;
        chargeCooltime = monsterComponent.monsterData.Skill3Cooltime;

        // 공격 거리 설정
        punchRange = 6f;
        shootRange = 25;
        chargeRange = 4f;

        // 패턴 쿨타임 체크용 타이머
        oneSecond = new WaitForSeconds(1f);

        // 투사체 풀 캐싱
        projectilePool = GameObject.Find("Pool_YetiProjectile").GetComponent<ObjectPool>();
        // 돌진공격 기울기 값
        //chargeRot = new Vector3(30, 0, 0);

        punchCollider = 
            monsterComponent.transform.GetChild(0).
            transform.GetChild(0).transform.GetChild(2).
            transform.GetChild(0).transform.GetChild(0).
            transform.GetChild(0).transform.GetChild(0).
            transform.GetChild(0).transform.GetChild(0).
            transform.GetChild(0).GetComponent<SphereCollider>();

        chargeCollider =
            monsterComponent.transform.GetChild(1).GetComponent<BoxCollider>();

        punchCollider.enabled = false;
        chargeCollider.enabled = false;

        punchAniDelay = new WaitForSeconds(1.6f);
        shootAniDelay = new WaitForSeconds(6f);

        chargeAniDelay_1 = new WaitForSeconds(1.4f);
        chargeAniDelay_2 = new WaitForSeconds(1.1f);
    }
    #endregion

    private IEnumerator DoCycle(BossStateMachine bossFSM_)
    {
        // 타이머 충전 시작
        bossFSM_.StartCoroutine(PlusShootTimer());
        bossFSM_.StartCoroutine(PlusChargeTimer());

        CheckTarget();

        while (bossFSM_.currentState.Equals(BossStateMachine.BossState.Yeti_BossAttack))
        {
            yield return bossFSM_.StartCoroutine(ChoosePattern(bossFSM_));
        }
    }

    private IEnumerator ChoosePattern(BossStateMachine bossFSM_)
    {
        CheckTarget();

        // 공격 선택 순서 -> 1. 돌진 타이머 체크, 2. 투사체 타이머 체크, 3. 기본 공격 
        if (chargeCooltime <= chargeCooltimer)
        {
            yield return bossFSM_.StartCoroutine(Charge(bossFSM_));
        }
        else
        {
            if (shootCooltime <= shootCooltimer)
            {
                yield return bossFSM_.StartCoroutine(Shoot(bossFSM_));
            }
            else
            {
                yield return bossFSM_.StartCoroutine(Punch(bossFSM_));
            }
        }
    }

    #region Punch
    // 기본공격 코루틴
    private IEnumerator Punch(BossStateMachine bossFSM_)
    {
        // 타겟까지 이동
        // 목표 좌표와의 거리가 특정 거리 이하가 될 때까지만 while 문 지속
        while (Vector3.Distance(target.transform.position, monsterComponent.transform.position) > punchRange)
        {
            yield return null;

            // 타겟을 바라보게 한다
            monsterComponent.transform.LookAt(target.transform);

            // 중력값 추가를 위한 지역변수
            Vector3 tempMove =
                new Vector3(target.transform.position.x - monsterComponent.transform.position.x,
                gravity, target.transform.position.z - monsterComponent.transform.position.z).normalized;

            // 타겟의 위치로 움직인다
            monsterControl.Move(tempMove * speed * Time.deltaTime);

            monsterAni.SetBool(monsterComponent.isMovingID, true);
        }
        monsterAni.SetBool(monsterComponent.isMovingID, false);

        // 보스 25도 앞으로 기울이기
        monsterComponent.transform.rotation 
            = Quaternion.Euler(new Vector3(25, monsterComponent.transform.rotation.eulerAngles.y, monsterComponent.transform.rotation.z));

        // 기본공격 애니메이션 재생
        monsterAni.SetBool(monsterComponent.isAttackingID, true);

        // 콜라이더 On
        punchCollider.enabled = true;

        // 애니메이션이 모두 끝난 후까지 대기
        yield return punchAniDelay;

        // 만약 콜라이더가 켜져있다면 끄기
        if (punchCollider.enabled == true) 
        {
            punchCollider.enabled = false;
        }

        // 기본 공격 애니메이션 정지
        monsterAni.SetBool(monsterComponent.isAttackingID, false);

        // 회전값 복구
        monsterComponent.transform.rotation = Quaternion.Euler(Vector3.zero);

        // 타겟 리셋
        target = default;
    }
    #endregion

    #region Shoot
    // 투사체공격 코루틴
    private IEnumerator Shoot(BossStateMachine bossFSM_)
    {
        // 타겟까지 이동
        // 목표 좌표와의 거리가 특정 거리 이하가 될 때까지만 while 문 지속
        while (Vector3.Distance(target.transform.position, monsterComponent.transform.position) > shootRange)
        {
            yield return null;

            // 타겟을 바라보게 한다
            monsterComponent.transform.LookAt(target.transform);

            // 중력값 추가를 위한 지역변수
            Vector3 tempMove =
                new Vector3(target.transform.position.x - monsterComponent.transform.position.x,
                gravity, target.transform.position.z - monsterComponent.transform.position.z).normalized;

            // 타겟의 위치로 움직인다
            monsterControl.Move(tempMove * speed * Time.deltaTime);

            monsterAni.SetBool(monsterComponent.isMovingID, true);
        }
        monsterAni.SetBool(monsterComponent.isMovingID, false);

        // 타겟 바라보기
        monsterComponent.transform.LookAt(target.transform);

        // 공격 애니메이션 재생
        monsterAni.SetBool(monsterComponent.isShootingID, true);

        // 투사체 풀에서 오브젝트 대여 및 시작 위치 설정
        bossFSM_.StartCoroutine(SetProjectile());

        // 애니메이션이 모두 끝난 후까지 대기
        yield return shootAniDelay;

        // 공격 애니메이션 정지
        monsterAni.SetBool(monsterComponent.isShootingID, false);

        // 타이머 초기화
        shootCooltimer = 0f;

        // 타이머 충전
        bossFSM_.StartCoroutine(PlusShootTimer());

        // 타겟 리셋
        target = default;
    }

    private IEnumerator SetProjectile() 
    {
        // 2 ~ 5 개 사이의 랜덤한 숫자만큼 투사체 발사
        //int projectileCount = Random.Range(2, monsterComponent.projectileParent.childCount);

        for (int i = 0; i < monsterComponent.projectileParent.childCount-1; i++) 
        {
            Debug.Log(monsterComponent.projectileParent.GetChild(i).position);
            projectilePool.ActiveObjFromPool(monsterComponent.projectileParent.GetChild(i+1).position);
            yield return oneSecond;
        }
    }
    #endregion

    #region Charge
    // 돌진공격 코루틴
    private IEnumerator Charge(BossStateMachine bossFSM_)
    {
        // 타겟 바라보기
        monsterComponent.transform.LookAt(target.transform);

        // 공격 애니메이션 재생(가슴두드리기)
        monsterAni.SetBool(monsterComponent.isChargingID_1, true);

        // 끝날 때까지 대기
        yield return chargeAniDelay_1;

        // 타겟 바라보기
        monsterComponent.transform.LookAt(target.transform);

        // 공격 애니메이션 정지 및 이후 시퀸스 재생(발구르기)
        monsterAni.SetBool(monsterComponent.isChargingID_1, false);
        monsterAni.SetBool(monsterComponent.isChargingID_2, true);

        // 끝날 때까지 대기
        yield return chargeAniDelay_2;

        // 공격 애니메이션 정지 및 이후 시퀸스 재생(돌진)
        monsterAni.SetBool(monsterComponent.isChargingID_2, false);
        monsterAni.SetBool(monsterComponent.isChargingID_3, true);

        // 콜라이더 On
        chargeCollider.enabled = true;

        // 타겟 위치 저장
        Vector3 lastPos = target.transform.position;

        // 보스와 타겟의 마지막 위치의 방향 구하기
        Vector3 direction = (lastPos - monsterComponent.transform.position).normalized * 2f;

        // 타겟 위치에서 뒤쪽으로 조금 벗어난 vector 구하기
        Vector3 chargePos = lastPos + direction;

        // 타겟 위치로 돌진
        while (Vector3.Distance(chargePos, monsterComponent.transform.position) > chargeRange) 
        {
            yield return null;

            // 타겟 바라보기
            monsterComponent.transform.LookAt(chargePos);

            // 중력값 추가를 위한 지역변수
            Vector3 tempMove =
                new Vector3(chargePos.x - monsterComponent.transform.position.x,
                gravity, chargePos.z - monsterComponent.transform.position.z).normalized;

            // 타겟의 위치로 움직인다
            monsterControl.Move(tempMove * speed * Time.deltaTime);
        }

        // 애니메이션 정지
        monsterAni.SetBool(monsterComponent.isChargingID_3, false);

        // 콜라이더 켜져 있다면 끄기
        if (chargeCollider.enabled == true) 
        {
            chargeCollider.enabled = false;
        }

        // 타이머 초기화
        chargeCooltimer = 0f;

        // 타이머 충전
        bossFSM_.StartCoroutine(PlusChargeTimer());

        // 타겟 리셋
        target = default;
    }
    #endregion

    private IEnumerator PlusShootTimer() 
    {
        int currentTime = default;

        while (shootCooltime >= currentTime) 
        {
            yield return oneSecond;
            currentTime += 1;
        }

        shootCooltimer = currentTime;
    }

    private IEnumerator PlusChargeTimer()
    {
        int currentTime = default;

        while (chargeCooltime >= currentTime)
        {
            yield return oneSecond;
            currentTime += 1;
        }

        chargeCooltimer = currentTime;
    }

    #region 타겟 확인 메서드
    private void CheckTarget()
    {
        // OverlapSphere 을 사용하여 Player Layer 를 검출시도한다
        Collider[] colliders = Physics.OverlapSphere(monsterComponent.transform.position, radius, LayerMask.GetMask("Player"));

        if (colliders.Length <= 0) 
        {
            //Debug.LogError("타겟 검출 에러");
            target = GameObject.Find("Player_LJY").transform.Find("PlayerController").Find("PlayerRigid").gameObject;
        }
        // 만약 검출된 것이 있다면
        else if (colliders.Length > 0)
        {
            // foreach 반복을 통해 colliders 를 모두 검사한다
            foreach (var collider in colliders)
            {
                // 만약 검출 대상이 Rigidbody 컴포넌트를 가지고 있다면,
                // 허수아비 > 플레이어의 순서로 공격대상을 선정
                if (collider.GetComponent<Rigidbody>() == true)
                {
                    // 허수아비이면 바로 메서드 종료
                    if (collider.gameObject.name.Contains("Scarecrow"))
                    {
                        target = collider.gameObject;
                        monsterComponent.target = target;
                        return;
                    }
                    // 플레이어라면 일단 target 에 캐싱
                    else
                    {
                        target = collider.gameObject;
                        monsterComponent.target = target;
                    }
                }
            }
        }
    }
    #endregion

    #region 변수 비우기
    private void CleanVariables()
    {
        monsterControl = default;
        monsterComponent = default;
        monsterAni = default;
        speed = default;
        gravity = default;
        radius = default;
        target = default;

        shootCooltimer = default;
        chargeCooltimer = default;

        shootCooltime = default;
        chargeCooltime = default;

        punchRange = default;
        shootRange = default;
        chargeRange = default;

        oneSecond = default;

        projectilePool = default;

        punchCollider = default;
        chargeCollider = default;
    }
    #endregion
}
