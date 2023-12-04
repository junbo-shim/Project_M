using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class TitleButtonManager : MonoBehaviour
{
    public GameObject firebaseManager;

    public TMP_Text statusText;
    public GameObject buttonRegisterOpen;
    public GameObject buttonLoginOpen;
    public GameObject buttonLogoutOpen;

    public GameObject inputFieldEmail;
    public GameObject inputFieldPW;
    public GameObject buttonRegister;
    public GameObject buttonLogin;
    public TMP_Text resultText;

    public GameObject buttonLoadSave;
    public GameObject buttonSaveSave;
    public TMP_Text data;

    public GameObject buttonLoadInven;
    public GameObject buttonSaveInven;
    public TMP_InputField dataInputField;

    private bool isUIOn = false;



    private void Update()
    {
        UpdateLoginStatus(FirebaseAuth.DefaultInstance.CurrentUser);
    }



    public void UpdateLoginStatus(FirebaseUser user_) 
    {
        if (user_ == null) 
        {
            statusText.text = "Login Status : LogOut";
        }
        else if (user_ != null) 
        {
            statusText.text = "Login Status : LogIn";
        }
    }

    

    #region 회원가입 버튼
    public void OnPressRegister() 
    {
        // Inputfield 가 꺼진 상태라면
        if (isUIOn == false) 
        {
            // Inputfield 를 켜주고
            inputFieldEmail.SetActive(true);
            inputFieldPW.SetActive(true);
            // 아래쪽 회원가입 버튼을 활성화 한다
            buttonRegister.SetActive(true);
            // Inputfield 상태확인용 bool -> true
            isUIOn = true;
            // 로그인 버튼은 잠시 상호작용을 꺼둔다
            buttonLoginOpen.GetComponent<Button>().interactable = false;
        }
        // Inputfield 가 켜진 상태라면
        else 
        {
            // Inputfield 를 모두 끄고 텍스트를 비워둔다
            inputFieldEmail.SetActive(false);
            inputFieldEmail.GetComponent<TMP_InputField>().text = default;
            inputFieldPW.SetActive(false);
            inputFieldPW.GetComponent<TMP_InputField>().text = default;
            // 아래쪽 회원가입 버튼을 비활성화 한다
            buttonRegister.SetActive(false);
            // Inputfield 상태확인용 bool -> false
            isUIOn = false;
            // 로그인 버튼을 다시 활성화 해둔다
            buttonLoginOpen.GetComponent<Button>().interactable = true;
            buttonLoginOpen.SetActive(false);
            buttonLoginOpen.SetActive(true);
        }
    }
    #endregion

    #region 로그인 버튼
    public void OnPressLogin() 
    {
        if (isUIOn == false) 
        {
            inputFieldEmail.SetActive(true);
            inputFieldPW.SetActive(true);
            buttonLogin.SetActive(true);
            isUIOn = true;

            buttonRegisterOpen.GetComponent<Button>().interactable = false;
        }
        else 
        {
            inputFieldEmail.SetActive(false);
            inputFieldEmail.GetComponent<TMP_InputField>().text = default;
            inputFieldPW.SetActive(false);
            inputFieldPW.GetComponent<TMP_InputField>().text = default;
            buttonLogin.SetActive(false);
            isUIOn = false;

            buttonRegisterOpen.GetComponent<Button>().interactable = true;
            buttonRegisterOpen.SetActive(false);
            buttonRegisterOpen.SetActive(true);
        }
    }
    #endregion

    #region 로그아웃 버튼
    public void OnPressLogout() 
    {
        firebaseManager.GetComponent<FirebaseManager>().LogoutFirebase();
    }
    #endregion

    #region Inputfield 에 작성한 내용을 FirebaseManager 에 전달하는 메서드
    public void SubmitRegisterInfo() 
    {
        string emailInput = inputFieldEmail.GetComponent<TMP_InputField>().text;
        string pwInput = inputFieldPW.GetComponent<TMP_InputField>().text;

        firebaseManager.GetComponent<FirebaseManager>().RegisterFirebase(emailInput, pwInput);
    }

    public void SubmitLoginInfo()
    {
        string emailInput = inputFieldEmail.GetComponent<TMP_InputField>().text;
        string pwInput = inputFieldPW.GetComponent<TMP_InputField>().text;

        firebaseManager.GetComponent<FirebaseManager>().LoginFirebase(emailInput, pwInput);
    }
    #endregion

    #region 로그인 성공 시 회원가입 버튼 및 로그인 버튼 비활성화 메서드
    public void MakeRegisterLoginDisable() 
    {
        //buttonRegisterOpen.GetComponent<Button>().interactable = false;
        buttonLoginOpen.GetComponent<Button>().interactable = false;
    }

    // 재활성화 메서드
    public void MakeRegisterLoginEnable()
    {

    }
    #endregion

    public void ShowCurrentData()
    {

    }

    public void PutCurrentData() 
    {
        
    }

    #region 세이브 데이터 다운로드 버튼
    public void OnPressLoadSave() 
    {
        firebaseManager.GetComponent<FirebaseManager>().LoadPlayerSaveFile(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
    }
    #endregion

    #region 세이브 데이터 업로드 버튼
    public void OnPressSaveSave() 
    {
        firebaseManager.GetComponent<FirebaseManager>().SavePlayerSaveFile(FirebaseAuth.DefaultInstance.CurrentUser.UserId, "1", "2", "3");
    }
    #endregion

    #region 인벤토리 데이터 다운로드 버튼
    public void OnPressLoadInven() 
    {
        firebaseManager.GetComponent<FirebaseManager>().LoadPlayerInven(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
    }
    #endregion

    #region 인벤토리 데이터 업로드 버튼
    public void OnPressSaveInven() 
    {
        firebaseManager.GetComponent<FirebaseManager>().SavePlayerInven(FirebaseAuth.DefaultInstance.CurrentUser.UserId, firebaseManager.GetComponent<FirebaseManager>().playerInventory);
    }
    #endregion
}
