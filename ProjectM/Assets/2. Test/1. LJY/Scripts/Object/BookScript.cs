using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject magicUi;

    private bool isGrabbed;

    private Quaternion direction;
    public Vector3 rayDirection;
    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rayDirection = transform.forward;
        if (isGrabbed)
        {
            lineRenderer.SetPosition(0, transform.position);
            //direction = InputBridge.Instance.GetControllerLocalRotation(ControllerHand.Left);          
            ray = new Ray(transform.position, rayDirection);

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                //Debug.Log("레이캐스트 들어감");
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                // 일정 거리만큼의 끝점을 설정하거나 다른 방식으로 처리
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 1000f);
            }
        }

    }

    public void OnMagicUi()
    {
        magicUi.SetActive(true);
    }

    public void OffMagicUi()
    {
        magicUi.SetActive(false);
    }

    public void OnLineRenderer()
    {
        isGrabbed = true;
        lineRenderer.enabled = true;

    }

    public void OffLineRenderer()
    {
        isGrabbed = false;
        lineRenderer.enabled = false;
    }
}
