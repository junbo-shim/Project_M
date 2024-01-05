using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class SaveNpc : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;// 현재 대화 문구
    [SerializeField] private GameObject TextCanvas; // 대화 오브젝트
    [SerializeField] private GameObject choiceObj; // 선택지 오브젝트
    private WaitForSeconds timeSeconds;// 세이브 진행 딜레이시간
    private bool saveSuccess = false; // 성공여부
    private StringBuilder stringBuilder;
    private string maxKey;//maxKey 값
    private int maxValue;//max vlaue 값
    private int mbtiID;// titleMbti값
    private string minKey;//minkey 값
    private int minValue;//minValue값
    // Start is called before the first frame update
    void Start()
    {
        stringBuilder = new StringBuilder();
        timeSeconds = new WaitForSeconds(1f);
        textMeshPro.text = "지금까지의 여정을 기록하겠는가?";
    }

    public void OnClickChoice()
    {

        StartCoroutine(SaveText());
        choiceObj.SetActive(false);
    }

    public void TalkChange()
    {
        if (saveSuccess)
        {
            if(stringBuilder != null)
            {
                stringBuilder.Clear();
            }
            
            MBTIMaxKeySearch();
            stringBuilder.Append(CSVRead.instance.MbtTitleDatas[mbtiID.ToString()].Script);
            stringBuilder.Append("\n남은 여정도 축복과 함께 함께하길..");
            textMeshPro.text = stringBuilder.ToString();
        }
        else
        {
            TextCanvas.SetActive(true);
        }
    }

    public void ExitTalk()
    {
        saveSuccess = false;
        textMeshPro.text = "지금까지의 여정을 기록하겠는가?";
        TextCanvas.SetActive(false);
        choiceObj.SetActive(true);
    }
    private void OnDisable()
    {
        
    }

    private void MBTIMaxKeySearch()
    {
       
        var MbtTitle = CSVRead.instance;

        maxKey = MBTIScripts.Instance.MaxMbti();
        maxValue = MBTIScripts.Instance.MBTiScore_[maxKey];
        minKey = MBTIScripts.Instance.minKey;
        minValue = MBTIScripts.Instance.minValue;

        int Lv = 0;
        Debug.Log(maxKey);
        if(maxValue + minValue < 7)
        {
            Lv = 0;
        }
        else if(maxValue + minValue >= 7)
        {
            if (maxValue - minValue < 5)
            {
                Lv = 1;
            }
            else if(maxValue - minValue >= 5) 
            {
                Lv = 2;
            }
        }

        switch (maxKey)
        {
            case "E":
                if(Lv == 0)
                {
                    mbtiID = 100001;
                }else if(Lv == 1)
                {
                    mbtiID = 100002;
                }else if(Lv == 2)
                {
                    mbtiID = 100003;
                }
            break;
            case "I":
                if (Lv == 0)
                {
                    mbtiID = 100004;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100005;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100006;
                }
            break;
            case "S":
                if (Lv == 0)
                {
                    mbtiID = 100007;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100008;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100009;
                }
            break;
            case "N":
                if (Lv == 0)
                {
                    mbtiID = 100010;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100011;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100012;
                }
             break;
            case "F":
                if (Lv == 0)
                {
                    mbtiID = 100013;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100014;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100015;
                }
             break;
            case "T":
                if (Lv == 0)
                {
                    mbtiID = 100016;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100017;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100018;
                }
            break;
            case "J":
                if (Lv == 0)
                {
                    mbtiID = 100019;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100020;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100021;
                }
             break;
            case "P":
                if (Lv == 0)
                {
                    mbtiID = 100022;
                }
                else if (Lv == 1)
                {
                    mbtiID = 100023;
                }
                else if (Lv == 2)
                {
                    mbtiID = 100024;
                }
             break;
         
        }
    }
   
    private IEnumerator SaveText()
    {
      
        int i = 0; 
        while (i < 3)
        {
            i += 1;
            if (!textMeshPro.text.Equals("세이브 중...."))
            {
                textMeshPro.text = "세이브 중....";
            }

            yield return timeSeconds;
        }
        textMeshPro.text = "지금까지의 여정을 기록했다네";
        saveSuccess = true;
    }
}
