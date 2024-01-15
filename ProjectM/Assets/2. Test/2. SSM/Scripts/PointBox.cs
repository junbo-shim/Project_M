using UnityEngine;

public class PointBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MapGameManager.instance.ChangeDayState();
            MapGameManager.instance.ridingSnapZone.BanSwitchRiding(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MapGameManager.instance.ChangeDayState();
            MapGameManager.instance.ridingSnapZone.BanSwitchRiding(true);
        }
    }
}
