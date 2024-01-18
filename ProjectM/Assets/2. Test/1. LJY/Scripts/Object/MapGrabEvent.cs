using BNG;
using UnityEngine;

public class MapGrabEvent : MonoBehaviour
{
    public MiniMapController mc;
    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(InputBridge.Instance.RightTriggerUp)
        {
            CloseAndOpenMap();
        }
    }

    public void CloseAndOpenMap()
    {
        Debug.Log("트리거업");

        if (!mc.isacting)
        {
            if (!isOpened)
            {
                isOpened = true;
                mc.OpenMap();
            }
            else
            {
                isOpened = false;
                mc.CloseMap();
            }
        }
    }

    public void ReleaseMapObj()
    {
        Debug.Log("놓았음");

        if (isOpened)
        {
            isOpened = false;
            mc.CloseMap();
        }
    }
}
