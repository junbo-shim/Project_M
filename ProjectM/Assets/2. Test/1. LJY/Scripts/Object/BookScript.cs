using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : GrabbableEvents
{
    private LineRenderer lineRenderer;
    public GameObject magicUi;
    public Transform book;
    public GameObject closeBook;

    public Vector3 target;

    public GameObject poison;
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
        
        if (isGrabbed)
        {
            rayDirection = -book.forward;
            lineRenderer.SetPosition(0, transform.position);
            ray = new Ray(transform.position, rayDirection);
            int terrainLayerMask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(ray, out hit, 500f, terrainLayerMask))
            {
                lineRenderer.SetPosition(1, hit.point);
                target = hit.point;
            }
            else
            {
                // 일정 거리만큼의 끝점을 설정하거나 다른 방식으로 처리
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 500f);
                target = lineRenderer.GetPosition(1);
            }
        }
    }

    public void GrabEvent()
    {
        OnLineRenderer();
        OnMagicUi();
        book.gameObject.SetActive(true);
        closeBook.SetActive(false);
        //StartCoroutine(ViewRayZone());
    }

    public override void OnRelease()
    {
        base.OnRelease();
        OffMagicUi();
        OffLineRenderer();
        closeBook.SetActive(true);
        book.gameObject.SetActive(false);
        //StopCoroutine(ViewRayZone());
    }

    public override void OnSnapZoneEnter()
    {
        base.OnSnapZoneEnter();
        OffMagicUi();
        OffLineRenderer();
        closeBook.SetActive(true);
        book.gameObject.SetActive(false);
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

    private IEnumerator ViewRayZone()
    {
        while(isGrabbed)
        {
            rayDirection = transform.forward;
            if (isGrabbed)
            {
                lineRenderer.SetPosition(0, transform.position);
                ray = new Ray(transform.position, rayDirection);
                int terrainLayerMask = LayerMask.GetMask("Terrain");
                if (Physics.Raycast(ray, out hit, 500f, terrainLayerMask))
                {
                    lineRenderer.SetPosition(1, hit.point);
                    target = hit.point;
                }
                else
                {
                    // 일정 거리만큼의 끝점을 설정하거나 다른 방식으로 처리
                    lineRenderer.SetPosition(1, ray.origin + ray.direction * 500f);
                    target = lineRenderer.GetPosition(1);
                }
            }
            yield return new WaitForEndOfFrame();
        }      
        yield return null;
    }
}
