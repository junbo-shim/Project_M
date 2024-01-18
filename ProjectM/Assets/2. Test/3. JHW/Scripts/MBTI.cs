using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MBTI : MonoBehaviour
{
    public Text endingMBTIText;

    public Text illustratedMBTIText;


    private void Update()
    {
        ShowMyMbti();
    }

    private void ShowMyMbti()
    {
        endingMBTIText.text = CSVRead.instance.MbtTitleDatas[MBTIScripts.Instance.MBTIInfo(MBTIScripts.Instance.FinalMBTIDerive())].Script.ToString();
        illustratedMBTIText.text = CSVRead.instance.MbtTitleDatas[MBTIScripts.Instance.MBTIInfo(MBTIScripts.Instance.FinalMBTIDerive())].Script.ToString();

    }







}
