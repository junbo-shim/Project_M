using System.Collections;
using UnityEngine;

public class BossMapOff : MonoBehaviour
{
    private WaitForSeconds waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = new WaitForSeconds(3f);
        StartCoroutine(TurnOffBossMap());
    }

    private IEnumerator TurnOffBossMap() 
    {
        yield return waitTime;

        gameObject.SetActive(false);
    }
}
