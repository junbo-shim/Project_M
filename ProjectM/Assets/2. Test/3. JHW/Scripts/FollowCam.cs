using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Camera followCam;

    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
        }
    }

    void Update()
    {
        if (followCam != null)
        {
            transform.position = followCam.transform.position + followCam.transform.forward * 50;
            transform.rotation = followCam.transform.rotation;
        }
    }
}
