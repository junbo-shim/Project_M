using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScripts : MonoBehaviour
{
    float h, s, v;
    
    public void Start()
    {
        ColorRead(false);
    }
    void Update()
    {
        
        // 상태에 따라 동적으로 포그 밀도 변경
        if (MapGameManager.instance.currentState == DayState.MORNING)
        {
          
           
            if (RenderSettings.fogDensity > 0.002f)
            {
                RenderSettings.fogDensity -= 0.00001f;
            }
            if(v < 0.9f)
            {
                ColorRead(false);
        
            }

        }
        else if (MapGameManager.instance.currentState == DayState.NIGHT)
        {

            if (RenderSettings.fogDensity < 0.05f)
            {
                RenderSettings.fogDensity += 0.00001f;
            }
            if(v > 0.01f)
            {
                ColorRead(true);
          
            }
        }
        
    }
    private void ColorRead(bool Stay)
    {
        Color fogColor = RenderSettings.fogColor;   
        Color.RGBToHSV(fogColor, out h, out s, out v);
   
        if(Stay)
        {
            v -= 0.001f;
        }
        else
        {
            v += 0.0001f;
        }
        Color rgbColor = Color.HSVToRGB(h / 360f, s, v);
        RenderSettings.fogColor = rgbColor;
    }

    
}
