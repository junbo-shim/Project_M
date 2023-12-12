using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScripts : MonoBehaviour
{
    float h, s, v;
    
    public void Start()
    {       
        StartCoroutine(fogDis()); // 안개 변경 효과 
    }
 
    //안개 변경 효과
    IEnumerator fogDis()
    {
        while (true)
        {
            // 상태에 따라 동적으로 포그 밀도 변경
            if (MapGameManager.instance.currentState == DayState.MORNING)
            {


                if (RenderSettings.fogDensity > 0.003f) //클경우 
                {
                    RenderSettings.fogDensity -= 0.002f;
                }
                if (v < 1f)
                {
                    fogColorChg(false);

                }

            }
            else if (MapGameManager.instance.currentState == DayState.NIGHT)
            {

                if (RenderSettings.fogDensity < 0.3f)
                {
                    RenderSettings.fogDensity += 0.001f;
                }
                if (v > 0.01f)
                {
                    fogColorChg(true);

                }
            }
            yield return new WaitForSeconds(0.1f);

        }
     
    }
    // 안개 색변경
    private void fogColorChg(bool Stay)
    {
        Color fogColor = RenderSettings.fogColor;   
        Color.RGBToHSV(fogColor, out h, out s, out v);
   
        if(Stay)
        {
            v -= 0.01f;
        }
        else
        {
            v += 0.01f;
        }
        Color rgbColor = Color.HSVToRGB(h / 360f, s, v);
        RenderSettings.fogColor = rgbColor;
    }

    
}
