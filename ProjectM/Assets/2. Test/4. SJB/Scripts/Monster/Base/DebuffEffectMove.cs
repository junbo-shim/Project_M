using UnityEngine;

public class DebuffEffectMove : MonoBehaviour
{
    public CharacterController monster;

    private void Update()
    {
        // 몬스터 몸통 가운데에서 이펙트 위치
        transform.position = monster.transform.position + (Vector3.up * monster.height * 0.5f);
    }
}
