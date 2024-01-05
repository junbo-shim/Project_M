using UnityEngine;

public class MyCamera : MonoBehaviour
{

    // 새로운 렌더러 카메라
    public Camera newRendererCamera;

    void Start()
    {
        // 캔버스 컴포넌트 가져오기
        Canvas canvas = GetComponent<Canvas>();

        if (canvas != null && newRendererCamera != null)
        {
            // 캔버스의 worldCamera 속성을 새로운 렌더러 카메라로 설정
            canvas.worldCamera = newRendererCamera;
        }
        else
        {
            Debug.LogError("Canvas 또는 새로운 렌더러 카메라가 없습니다.");
        }
    }

}
