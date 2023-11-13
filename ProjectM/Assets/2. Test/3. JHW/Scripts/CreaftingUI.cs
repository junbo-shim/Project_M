using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreaftingUI : MonoBehaviour
{
    Inventory inventory;

    public Text[] text;
    //private int currentNumber;
    //private int currentNumber1;
    //private int currentNumber2;
    //private int currentNumber3;
    private int[] currentNumbers; // 각 아이템에 대한 개수 배열

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        currentNumbers = new int[text.Length]; // 배열 초기화
    }

    // 특정 조건을 확인하여 숫자를 증가시키는 메소드
    public void IncreaseNumber()
    {

        //currentNumber = 0;
        //currentNumber1 = 0;
        //currentNumber2 = 0;
        //currentNumber3 = 0;

        // 배열 초기화
        for (int i = 0; i < currentNumbers.Length; i++)
        {
            currentNumbers[i] = 0;
        }



        //for (int i = 0; i < inventory.items.Count; i++)
        //{
        //    if (inventory.items[i].itemName == "Battery")
        //    {
        //        currentNumber++;
        //    }
        //    else if (inventory.items[i].itemName == "Ladder")
        //    {
        //        currentNumber1++;
        //    }
        //    else if (inventory.items[i].itemName == "SleepBag")
        //    {
        //        currentNumber2++;
        //    }
        //    else if (inventory.items[i].itemName == "Supply")
        //    {
        //        currentNumber3++;
        //    }
        //}


        // 각 아이템에 대한 개수 계산
        for (int i = 0; i < inventory.items.Count; i++)
        {
            for (int j = 0; j < currentNumbers.Length; j++)
            {
                if (inventory.items[i].itemName == GetItemNameByIndex(j))
                {
                    currentNumbers[j]++;
                }
            }
        }

    }

    // Text에 현재 숫자를 업데이트하는 메소드
    private void UpdateNumberText()
    {

        IncreaseNumber();

        for(int i = 0; i < 4; i++)
        {
            if (text[i] != null)
            {
                //text[0].text = currentNumber.ToString() + "/4";
                //text[1].text = currentNumber1.ToString() + "/4";
                //text[2].text = currentNumber2.ToString() + "/4";
                //text[3].text = currentNumber3.ToString() + "/4";

                text[i].text = currentNumbers[i].ToString() + "/4";

            }
        }
    }


    // index에 따라 해당하는 아이템의 이름을 반환하는 메소드
    private string GetItemNameByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return "Battery";
            case 1:
                return "Ladder";
            case 2:
                return "SleepBag";
            case 3:
                return "Supply";
            default:
                return "";
        }
    }

    private void Update()
    {
        UpdateNumberText();
    }
}
