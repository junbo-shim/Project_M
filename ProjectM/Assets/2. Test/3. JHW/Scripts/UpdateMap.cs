using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMap : MonoBehaviour
{
    public GameObject player;
    private RectTransform rectTransform;


    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            rectTransform.anchoredPosition = new Vector3(playerPosition.x, playerPosition.z , -10);

        }
    }
}
