using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListUI : MonoBehaviour
{
    public GameObject[] showItem;

    public void ScareCrow()
    {
        showItem[0].SetActive(true);
        showItem[1].SetActive(false);
    }

    public void Trab()
    {
        showItem[0].SetActive(false);
        showItem[1].SetActive(true);
    }
}
