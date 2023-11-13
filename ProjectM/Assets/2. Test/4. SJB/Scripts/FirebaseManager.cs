using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;

public partial class FirebaseManager : MonoBehaviour
{
    public GameObject titleButtonManager;

    #region 유니티 라이프 사이클
    private void Awake()
    {
        // DefaultInstance 캐싱 
        defaultStore = FirebaseFirestore.DefaultInstance;
        CheckFirebaseDependency();
    }
    #endregion



    #region 파이어베이스 종속성 검사 메서드
    private void CheckFirebaseDependency() 
    {
        // CheckAndFixDependenciesAsync 메서드를 실행한다, 반환형은 Task<DependencyStatus> 이다
        Task<DependencyStatus> task = FirebaseApp.CheckAndFixDependenciesAsync();

        // CheckAndFixDependenciesAsync 를 실행한 뒤에 ContinueWithOnMainThread 메서드를 실행한다
        task.ContinueWithOnMainThread(ContinueDependencyCheck);
    }

    private void ContinueDependencyCheck(Task<DependencyStatus> task_) 
    {
        // CheckAndFixDependenciesAsync 의 결과값을 지역변수로 생성한다
        DependencyStatus status = task_.Result;

        // 만약 결과가 Available 이면
        if (status == Firebase.DependencyStatus.Available) 
        {
            Debug.LogError("Dependency Status : " + status);
        }
        // 만약 결과가 그 이외라면
        else
        {
            Debug.LogError("Dependency Error, Status : " + status);
        }
    }
    #endregion

    #region 파이어베이스 회원가입 메서드
    public void RegisterFirebase(string email_, string pw_) 
    {
        // CreateUserWithEmailAndPasswordAsync 메서드를 실행한다, 반환형은 Task<AuthResult> 이다
        Task<AuthResult> registerTask =
            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email_, pw_);

        // CreateUserWithEmailAndPasswordAsync 를 실행한 뒤에 ContinueWithOnMainThread 메서드를 실행한다
        registerTask.ContinueWithOnMainThread(ContinueRegister);
    }

    private void ContinueRegister(Task<AuthResult> task_) 
    {
        // 만약 실행한 Task 가 실패하거나 취소되었다면 return 으로 메서드 실행을 중지한다
        if (task_.IsCanceled || task_.IsFaulted) 
        {
            Debug.LogError("Failed to Create new Account" + "\nFailure Reason : " + task_.Result);
            return;
        }

        // CreateUserWithEmailAndPasswordAsync 의 결과값을 지역변수로 생성한다
        AuthResult result = task_.Result;
        Debug.LogFormat("Successfully Created" + 
            "\nYour Name : " + result.User.DisplayName + 
            "\nYour UID : " + result.User.UserId);
    }
    #endregion

    #region 파이어베이스 비밀번호 초기화 메서드
    private void ResetFirebasePW() 
    {
    
    }
    #endregion

    #region 파이어베이스 로그인 메서드
    public void LoginFirebase(string email_, string pw_) 
    {
        // SignInWithEmailAndPasswordAsync 메서드를 실행한다, 반환형은 Task<AuthResult> 이다
        Task<AuthResult> loginTask =
            FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email_, pw_);

        // SignInWithEmailAndPasswordAsync 를 실행한 뒤에 ContinueWithOnMainThread 메서드를 실행한다
        loginTask.ContinueWithOnMainThread(ContinueLogin);
    }

    private void ContinueLogin(Task<AuthResult> task_) 
    {
        // 만약 실행한 Task 가 실패하거나 취소되었다면 return 으로 메서드 실행을 중지한다
        if (task_.IsCanceled || task_.IsFaulted)
        {
            Debug.LogError("Failed to Login" + "\nFailure Reason : " + task_.Result);
            return;
        }

        // SignInWithEmailAndPasswordAsync 의 결과값을 지역변수로 생성한다
        AuthResult result = task_.Result;
        Debug.LogFormat("Login Successful" +
            "\nYour Name : " + result.User.DisplayName +
            "\nYour UID : " + result.User.UserId);

        // 회원가입 및 로그인 버튼 비활성화 - 버그
        //titleButtonManager.GetComponent<TitleButtonManager>().MakeRegisterLoginDisable();

        CheckAndInitPlayerData(result.User.UserId);
    }
    #endregion

    #region 파이어베이스 로그아웃 메서드
    public void LogoutFirebase() 
    {
        if (FirebaseAuth.DefaultInstance == null) 
        {
            Debug.LogWarning(FirebaseAuth.DefaultInstance);
        }
        else if (FirebaseAuth.DefaultInstance != null) 
        {
            FirebaseAuth.DefaultInstance.SignOut();

            // 회원가입 및 로그인 버튼 재활성화 - 버그
            //titleButtonManager.GetComponent<TitleButtonManager>().MakeRegisterLoginEnable();
            Debug.LogWarning("Logout Successful");
        }
    }
    #endregion
}
