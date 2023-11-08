using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//! ARAVRInput 클래스는 VA/AR에 대응하는 Input 기능을 구현하는 클래스이다.
//public static class ARAVRInput
//{
//    //! 다양한 기기에서 사용할 버튼의 종류를 미리 정의해둔 것이다.
//#if BUILD_PLATFORM_PC
//    public enum ButtonTarget
//    {
//        Fire1,      /** < 발사 버튼 1번이다. */
//        Fire2,      /** < 발사 버튼 2번이다. */
//        Fire3,      /** < 발사 버튼 3번이다. */
//        Jump        /** < 점프 버튼이다. */
//    }   // ButtonTarget
//#endif

//    //! 미리 정의해 놓은 버튼을 기기별로 다르게 매핑해둔 것이다.
//    public enum Button
//    {
//#if BUILD_PLATFORM_PC
//        One = ButtonTarget.Fire1,   /**< 발사 버튼 1을 매핑했다. */
//        Two = ButtonTarget.Jump,    /**< 점프 버튼을 매핑했다. */
//        Thumbstick = ButtonTarget.Fire1,    /**< 발사 버튼 1을 매핑했다. */
//        IndexTrigger = ButtonTarget.Fire3,  /**< 발사 버튼 3을 매핑했다. */
//        HandTrigger = ButtonTarget.Fire2    /**< 발사 버튼 2를 매핑했다. */
//#elif TARGET_DEVICE_OCULUS 
//        One = OVRInput.Button.One,          /**< VR 컨트롤러의 One 버튼을 매핑했다 */
//        Two = OVRInput.Button.Two,          /**< VR 컨트롤러의 Two 버튼을 매핑했다 */
//        Thumbstick = OVRInput.Button.PrimaryThumbstick,     /**< VR 컨트롤러의 조이스틱 클릭 버튼을 매핑했다 */
//        IndexTrigger = OVRInput.Button.PrimaryIndexTrigger, /**< VR 컨트롤러의 조이스틱 IndexTrigger 버튼을 매핑했다 */
//        HandTrigger = OVRInput.Button.PrimaryHandTrigger    /**< VR 컨트롤러의 조이스틱 HandTrigger 버튼을 매핑했다 */
//#endif
//    }       // Button

//    //! 기기별로 다른 컨트롤러를 미리 정의해둔 것이다.
//    public enum Controller
//    {
//#if BUILD_PLATFORM_PC
//        LTouch,     /**< 왼쪽 컨트롤러 */
//        RTouch      /**< 오른쪽 컨트롤러 */
//#elif TARGET_DEVICE_OCULUS
//        LTouch = OVRInput.Controller.LTouch,    /**< VR 왼쪽 컨트롤러 */
//        RTouch = OVRInput.Controller.RTouch     /**< VR 오른쪽 컨트롤러 */
//#endif
//    }


//    /**
//     * @brief 오른쪽 컨트롤러의 위치를 얻어오는 프로퍼티이다.
//     * @return 카메라를 기준으로 컨트롤러의 위치를 연산해서 리턴한다.
//     */
//    public static Vector3 RHandPosition
//    {
//        get
//        {
//#if BUILD_PLATFORM_PC
//            // 마우스의 스크린 좌표 얻어오기
//            Vector3 pos = Input.mousePosition;
//            // z 값은 0.7m로 설정
//            pos.z = 0.7f;
//            // 스크린 좌표를 월드 좌표로 변환
//            pos = Camera.main.ScreenToWorldPoint(pos);
//            RHand.position = pos;
//#elif TARGET_DEVICE_OCULUS
//            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
//            pos = GetTransform().TransformPoint(pos);
//#else
//            Vector3 pos = default;
//#endif
//            return pos;
//        }
//    }       // RHandPosition

//    /**
//     * @brief 오른쪽 컨트롤러의 방향을 얻어오는 프로퍼티이다.
//     * @return 카메라를 기준으로 컨트롤러의 정면 방향을 연산해서 리턴한다.
//     */
//    public static Vector3 RHandDirection
//    {
//        get
//        {
//#if BUILD_PLATFORM_PC
//            Vector3 direction = RHandPosition - Camera.main.transform.position;
//            RHand.forward = direction;
//#elif TARGET_DEVICE_OCULUS
//            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
//            direction = GetTransform().TransformDirection(direction);
//#endif
//            return direction;
//        }
//    }       // RHandDirection

//    /**
//     * @brief 왼쪽 컨트롤러의 위치를 얻어오는 프로퍼티이다.
//     * @return 카메라를 기준으로 컨트롤러의 위치를 연산해서 리턴한다.
//     */
//    public static Vector3 LHandPosition
//    {
//        get
//        {
//#if BUILD_PLATFORM_PC
//            // 마우스의 스크린 좌표 얻어오기
//            Vector3 pos = Input.mousePosition;
//            // z 값은 0.7m로 설정
//            pos.z = 0.7f;
//            // 스크린 좌표를 월드 좌표로 변환
//            pos = Camera.main.ScreenToWorldPoint(pos);
//            LHand.position = pos;
//#elif TARGET_DEVICE_OCULUS
//            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
//            pos = GetTransform().TransformPoint(pos);
//#else
//            Vector3 pos = default;
//#endif
//            return pos;
//        }
//    }       // LHandPosition

//    /**
//    * @brief 왼쪽 컨트롤러의 방향을 얻어오는 프로퍼티이다.
//    * @return 카메라를 기준으로 컨트롤러의 정면 방향을 연산해서 리턴한다.
//    */
//    public static Vector3 LHandDirection
//    {
//        get
//        {
//#if BUILD_PLATFORM_PC
//            Vector3 direction = LHandPosition - Camera.main.transform.position;
//            LHand.forward = direction;
//#elif TARGET_DEVICE_OCULUS
//            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward;
//            direction = GetTransform().TransformDirection(direction);
//#endif
//            return direction;
//        }
//    }       // LHandDirection

//    static Transform rHand;     /**< 씬에 등록된 오른쪽 컨트롤러를 캐싱하는 변수이다. */
//    /**
//     * @brief 씬에 등록된 오른쪽 컨트롤러를 찾아 반환하는 프로퍼티이다.
//     * @return 오른쪽 컨트롤러의 Transform을 리턴한다.
//     */
//    public static Transform RHand
//    {
//        get
//        {
//            if (rHand == null)
//            {

//#if BUILD_PLATFORM_PC
//                // RHand 이름으로 게임 오브젝트를 만든다.
//                GameObject handObj = new GameObject("RHand");
//                // 만들어진 객체의 트랜스폼을 rHand에 할당
//                rHand = handObj.transform;
//                // 컨트롤러를 카메라의 자식 객체로 등록
//                rHand.parent = Camera.main.transform;
//#elif TARGET_DEVICE_OCULUS
//                rHand = GameObject.Find("RightControllerAnchor").tranform;
//#endif
//            }
//            return rHand;
//        }
//    }   // RHand
    
//    static Transform lHand;/**< 씬에 등록된 왼쪽 컨트롤러를 캐싱하는 변수이다. */
//    /**
//     * @brief 씬에 등록된 왼쪽 컨트롤러를 찾아 반환하는 프로퍼티이다.
//     * @return 왼쪽 컨트롤러의 Transform을 리턴한다.
//     */
//    public static Transform LHand
//    {
//        get
//        {
//            if (lHand == null)
//            {

//#if BUILD_PLATFORM_PC
//                // RHand 이름으로 게임 오브젝트를 만든다.
//                GameObject handObj = new GameObject("LHand");
//                // 만들어진 객체의 트랜스폼을 rHand에 할당
//                lHand = handObj.transform;
//                // 컨트롤러를 카메라의 자식 객체로 등록
//                lHand.parent = Camera.main.transform;
//#elif TARGET_DEVICE_OCULUS
//                lHand = GameObject.Find("LeftControllerAnchor").tranform;
//#endif
//            }
//            return lHand;
//        }
//    }   // LHand

//#if BUILD_PLATFORM_PC
//    static Vector3 originScale = Vector3.one * 0.02f;   /**< 크로스헤어를 그릴 때 기존 스케일을 캐싱하는 변수 */
//#else
//    static Vector3 originScale = Vector3.one * 0.005f;   /**< 크로스헤어를 그릴 때 기존 스케일을 캐싱하는 변수 */
//#endif


//#if TARGET_DEVICE_OCULUS
//    private static Transform rootTransform; /**< ??? */
//#endif

//#if TARGET_DEVICE_OCULUS
//    static Transform GetTransform()
//    {
//        if (rootTransform == null)
//        {
//            rootTransform = GameObject.Find("TrackingSpace").transform;
//        }
//        return rootTransform;
//    }
//#endif

//    /**
//     * @brief 컨트롤러의 특정 버튼을 누르고 있는 동안 true를 반환하는 함수이다.
//     * @param virtualMask 미리 정의된 버튼의 종류를 받아온다.
//     * @param hand 어느 컨트롤러의 버튼을 누를 것인지 받아온다.
//     * @return virtualMask에 들어온 값을 ButtonTarget 타입으로 변환해 리턴한다.
//     */
//    public static bool Get(Button virtualMask, Controller hand = Controller.RTouch)
//    {
//#if BUILD_PLATFORM_PC    
//        return Input.GetButton(((ButtonTarget)virtualMask).ToString());
//#elif TARGET_DEVICE_OCULUS
//        return OVRInput.Get((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
//#endif
//    }

//    /**
//     * @brief 컨트롤러의 특정 버튼을 눌렀을 때 true를 반환하는 함수이다.
//     * @param virtualMask 미리 정의된 버튼의 종류를 받아온다.
//     * @param hand 어느 컨트롤러의 버튼을 누를 것인지 받아온다.
//     * @return virtualMask에 들어온 값을 ButtonTarget 타입으로 변환해 리턴한다.
//     */
//    public static bool GetDown(Button virtualMask, Controller hand = Controller.RTouch)
//    {
//#if BUILD_PLATFORM_PC
//        return Input.GetButtonDown(((ButtonTarget)virtualMask).ToString());
//#elif TARGET_DEVICE_OCULUS
//        return OVRInput.GetDown((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
//#endif
//    }

//    /**
//     * @brief 컨트롤러의 특정 버튼을 눌렀다 떼었을 때 true를 반환하는 함수이다.
//     * @param virtualMask 미리 정의된 버튼의 종류를 받아온다.
//     * @param hand 어느 컨트롤러의 버튼을 누를 것인지 받아온다.
//     * @return virtualMask에 들어온 값을 ButtonTarget 타입으로 변환해 리턴한다.
//     */
//    public static bool GetUp(Button virtualMask, Controller hand = Controller.RTouch)
//    {
//#if BUILD_PLATFORM_PC
//        return Input.GetButtonUp(((ButtonTarget)virtualMask).ToString());
//#elif TARGET_DEVICE_OCULUS
//        return OVRInput.GetUp((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);
//#endif
//    }

//    /**
//     * @brief 컨트롤러의 Axis 입력을 반환하는 함수이다.
//     * @param axis Horizontal, Vertical 값을 갖는다.
//     * @param hand 어느 컨트롤러의 버튼을 누를 것인지 받아온다.
//     * @return 컨트롤러의 Axis 입력을 실수 형태로 리턴한다.
//     */
//    public static float GetAxis(string axis, Controller hand = Controller.LTouch)
//    {
//#if BUILD_PLATFORM_PC
//        return Input.GetAxis(axis);
//#elif TARGET_DEVICE_OCULUS
//        if (axis == "Horizontal")
//        {
//            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, (OVRInput.Controller)hand).x;
//        }
//        else
//        {
//            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, (OVRInput.Controller)hand).y;
//        }
//#endif
//    }

//    /**
//     * @brief 컨트롤러의 진동 기능을 구현하기 위한 코루틴 함수이다.
//     * @param duration 얼마나 오래 진동할 것인지 받아온다.
//     * @param frequency 얼마의 빈도로 진동할 것인지 받아온다.
//     * @param amplitude 얼마나 큰 진폭을 사용할 것인지 받아온다.
//     * @param hand 어느 컨트롤러인지 받아온다.
//     */
//#if TARGET_DEVICE_OCULUS
//    static IEnumerator VibrationCoroutine(float duration, float frequency, float amplitude, Controller hand)
//    {
//        float currentTime = 0f;

//        while (currentTime < duration)
//        {
//            currentTime += Time.deltaTime;
//            OVRInput.SetControllerVibration(frequency, amplitude, (OVRInput.Controller)hand);
//            yield return null;
//        }
//        OVRInput.SetControllerVibration(0, 0, (OVRInput.Controller)hand);
//    }
//#endif

//    /**
//     * @brief 컨트롤러의 진동을 실행하는 함수이다.
//     * @param duration 얼마나 오래 진동할 것인지 받아온다.
//     * @param frequency 얼마의 빈도로 진동할 것인지 받아온다.
//     * @param amplitude 얼마나 큰 진폭을 사용할 것인지 받아온다.
//     * @param hand 어느 컨트롤러인지 받아온다.
//     * @see VibrationCoroutine()
//     */
//    public static void PlayVibration(float duration, float frequency, float amplitude, Controller hand)
//    {
//#if TARGET_DEVICE_OCULUS
//        if(CoroutineInstance.coroutineInstance == null)
//        {
//            GameObject coroutineObj = new GameObject("CoroutineInstance");
//            coroutineObj.AddComponent<CoroutineInstance>();
//        }

//        CoroutineInstance.coroutineInstance.StopAllCoroutines();
//        CoroutineInstance.coroutineInstance.StartCoroutine(VibrationCoroutine(duration, frequency, amplitude, hand));
//#endif
//    }

//    /**
//     * @brief 컨트롤러의 진동을 실행하는 함수이다.
//     * @see PlayVibration()
//     */
//    // 컨트롤러에 진동 호출하기
//    public static void PlayVibration(Controller hand)
//    {
//#if TARGET_DEVICE_OCULUS
//    PlayVibration(0.06f, 1, 1, hand);
//#endif
//    }

//    /**
//     * @brief 타겟이 바라보는 방향을 기준으로 센터를 잡는다.
//     * @param target 바라보는 방향을 결정할 Target의 Tranform을 받아온다.
//     * @param direction 바라볼 방향을 받아온다.
//     */
//    public static void Recenter(Transform target, Vector3 direction)
//    {
//        target.forward = target.rotation * direction;
//    }   // Recenter()

//    /**
//     * @brief VR카메라가 바라보는 방향을 기준으로 센터를 잡는다.
//     */
//    public static void Recenter()
//    {
//#if TARGET_DEVICE_OCULUS
//        OVRManager.display.RecenterPose();
//#endif
//    }

//    /**
//     * @brief 레이가 닿는 곳에 크로스헤어를 위치시키는 함수이다.
//     * @param crosshair 컨트롤러의 정면에 그릴 크로스헤어 오브젝트의 Transform을 받아온다.
//     * @param isHand 컨트롤러가 존재하는지 여부를 받아온다.
//     * @param hand 어느 컨트롤러의 버튼을 누를 것인지 받아온다.
//     */
//    public static void DrawCrosshair(Transform crosshair, bool isHand = true,
//        Controller hand = Controller.RTouch)
//    {
//        Ray ray;
//        // 컨트롤러의 위치와 방향을 이용해 레이 제작
//        if(isHand)
//        {
//#if BUILD_PLATFORM_PC
//            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//#else
//            if(hand == Controller.RTouch)
//            {
//                ray = new Ray(RHandPosition, RHandDirection);
//            }
//            else
//            {
//                ray = new Ray(LHandPosition, LHandDirection);
//            }
//#endif
//        }
//        else
//        {
//            // 카메라를 기준으로 화면의 정중앙으로 레이를 제작
//            ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        }

//        // 눈에 안보이는 Plane을 만든다.
//        Plane plane = new Plane(Vector3.up, 0);
//        float distance = 0;
//        // plane을 이용해 ray를 쏜다.
//        if(plane.Raycast(ray, out distance))
//        {
//            // 레이의 GetPoint 함수를 이용해 충돌 지점의 위치를 가져온다.
//            crosshair.position = ray.GetPoint(distance);
//            crosshair.forward = -Camera.main.transform.forward;
//            // 크로스헤어의 크기를 최소 기본 크기에서 거리에 따라 더 커지도록 한다.
//            crosshair.localScale = originScale * Mathf.Max(1, distance);
//        }   // if: 레이에 충돌한 지점이 있는 경우
//        else
//        {
//            crosshair.position = ray.origin + ray.direction * 100.0f;
//            crosshair.forward = -Camera.main.transform.forward;
//            distance = (crosshair.position - ray.origin).magnitude;
//            crosshair.localScale = originScale * Mathf.Max(1.0f,distance);
//        }   // else: 레이에 충돌한 지점이 없는 경우
//    }
//}

////! ARAVRInput  클래스에서 사용할 코루틴 객체
//class CoroutineInstance : MonoBehaviour
//{
//    public static CoroutineInstance coroutineInstance = null;     /**< ARAVRInput 기능을 사용하는 
//    동안 메모리에 인스턴스화 할 코루틴 객체 변수이다. */

//    //! 싱글턴 패턴으로 코루틴 객체를 생성하는 함수
//    private void Awake()
//    {
//        if(coroutineInstance == null)
//        {
//            coroutineInstance = this;
//        }

//        DontDestroyOnLoad(gameObject);
//    }
//}
