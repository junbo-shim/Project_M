using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Firestore;
using System.Collections.Generic;
using Firebase.Extensions;
using Unity.VisualScripting;
using System.Web;
using System.Text;

public partial class FirebaseManager : MonoBehaviour
{
    // DefaultInstance 캐싱용 변수
    private FirebaseFirestore defaultStore;

    // 테스트 전용
    private SaveDataFile playerSaveData;
    public List<Item> playerInventory;
    // 테스트 전용
    public GameObject testObject;
    public GameObject testDBObject;



    #region 플레이어 데이터를 초기화하는 메서드(없으면 만듦)
    private void CheckAndInitPlayerData(string userID_, string userEmail_) 
    {
        // 유저의 UID 를 이름으로 한 Firestore Document 를 생성한다 (경로 : UserData/'로그인한 유저 UID')
        DocumentReference mydata = defaultStore.Collection("UserData").Document(userID_);

        // Document 에 담을 데이터를 Dictionary 형태로 만든다
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            // 메일 주소를 저장한다
            {"Name", userEmail_},
        };

        // 데이터를 담은 Dictionary 를 비동기 업로드한다
        mydata.UpdateAsync(user).ContinueWithOnMainThread(ContinueCheckInit);
    }
    // 업로드 처리용 메서드
    private void ContinueCheckInit(Task task_) 
    {
        // 만약 실행한 Task 가 실패하거나 취소되었다면 return 으로 메서드 실행을 중지한다
        if (task_.IsCanceled || task_.IsFaulted)
        {
            Debug.LogError("Failed to Check Player Data" + "\nFailure Reason : " + task_.Exception);
            return;
        }
        Debug.Log("Player Data Check Success");
    }
    #endregion



    #region 데이터 다운로드
    // 플레이어 세이브 파일 다운로드 메서드
    public void LoadPlayerSaveFile(string userID_)
    {
        // 유저의 UID 를 이름으로 한 Firestore Document 를 저장한다 (경로 : UserData/'로그인 유저 UID'/SaveFile/'로그인 유저 UID')
        DocumentReference mySave = defaultStore.Collection("UserData").Document(userID_).Collection("SaveFile").Document(userID_);

        // 만약 mySave 가 없다면 중단한다
        if (mySave == null)
        {
            Debug.LogError("There is no SaveFile");
            return;
        }

        // mySave 에서 세이브파일을 받는다
        mySave.GetSnapshotAsync().ContinueWithOnMainThread(ContinueLoadSaveFile);
    }
    // 다운로드 처리용 메서드
    private void ContinueLoadSaveFile(Task<DocumentSnapshot> task_) 
    {
        DocumentSnapshot snapShot = task_.Result;

        if (snapShot.Exists == false) 
        {
            Debug.LogError("Error during downloading save data");
            titleButtonManager.GetComponent<TitleButtonManager>().data.text =
                "Error during downloading save data";
        }
        else 
        {
            // 받아온 데이터를 SaveDataFile 클래스 형태로 변환해서 playerSaveData 에 저장한다
            playerSaveData = snapShot.ConvertTo<SaveDataFile>();

            // 테스트 전용
            testObject.GetComponent<Test>().testSaveFile = playerSaveData;

            Debug.LogWarning("Save data download complete");
            titleButtonManager.GetComponent<TitleButtonManager>().data.text =
                "Save data download complete\n"
                + "\n" + playerSaveData.Savetime
                + "\n" + playerSaveData.Location
                + "\n" + playerSaveData.Chapter
                + "\n" + playerSaveData.Quest;
        }
    }

    // 플레이어 인벤토리 다운로드 메서드
    public void LoadPlayerInven(string userID_) 
    {
        // 유저의 UID 를 이름으로 한 Firestore Document 를 저장한다 (경로 : UserData/'로그인 유저 UID'/Inventory/'로그인 유저 UID')
        DocumentReference myInven = defaultStore.Collection("UserData").Document(userID_).Collection("Inventory").Document(userID_);

        // 만약 myInven 가 없다면 중단한다
        if (myInven == null)
        {
            Debug.LogError("There is no Inventory Data");
            return;
        }

        // myInven 에서 인벤토리 정보를 받는다
        myInven.GetSnapshotAsync().ContinueWithOnMainThread(ContinueLoadInventory);
    }
    // 다운로드 처리용 메서드
    private void ContinueLoadInventory(Task<DocumentSnapshot> task_) 
    {
        DocumentSnapshot snapShot = task_.Result;

        if (snapShot.Exists == false)
        {
            Debug.LogError("Error during downloading Inventory data");
            titleButtonManager.GetComponent<TitleButtonManager>().data.text =
                "Error during downloading Inventory data";
        }
        else
        {
            // 받아온 데이터를 Dictionary<int, string> 형태로 변환하여 지역변수로 생성한다
            Dictionary<int, string> loadedInven = snapShot.ConvertTo<Dictionary<int, string>>();

            // Todo : Item 형태로 저장해야하기 때문에 이름을 기준으로 Item 데이터를 찾아와서 저장한다
            for (int i = 0; i < loadedInven.Count; i++) 
            {
                if (testDBObject.GetComponent<TestItemDB>().testItemDB.ContainsKey(loadedInven[i])) 
                {
                    playerInventory[i] = testDBObject.GetComponent<TestItemDB>().testItemDB[loadedInven[i]];
                }
            }

            // 테스트 전용
            testObject.GetComponent<Test>().testItemList = playerInventory;
            Debug.LogWarning("Inventory data download complete");

            StringBuilder temp = new StringBuilder();
            for (int i = 0; i < playerInventory.Count; i++) 
            {
                temp.Append("\n");
                temp.Append(i);
                temp.Append("번째 아이템 : ");
                temp.Append(playerInventory[i]);
            }
            string result = temp.ToString();

            titleButtonManager.GetComponent<TitleButtonManager>().data.text =
                "Inventory data download complete\n" + result;
        }
    }
    #endregion



    #region 데이터 업로드
    // 플레이어 세이브 파일 업로드 메서드
    public void SavePlayerSaveFile(string userID_, string lastLocation_, string lastChapter_, string lastQuest_) 
    {
        // 유저의 UID 를 이름으로 한 Firestore Document 를 생성한다 (경로 : UserData/'로그인 유저 UID'/SaveFile/'로그인 유저 UID')
        DocumentReference mySave = defaultStore.Collection("UserData").Document(userID_).Collection("SaveFile").Document(userID_);

        // SaveDataFile 이라는 클래스 형식을 만들어 데이터를 담는다
        SaveDataFile save = new SaveDataFile
        {
            Savetime = FieldValue.ServerTimestamp.ToString(),
            Location = lastLocation_,
            Chapter = lastChapter_,
            Quest = lastQuest_
        };

        // 해당 경로의 파일에 데이터를 덮어쓴다 (비동기)
        mySave.SetAsync(save).ContinueWithOnMainThread(ContinueDataUpdate);
    }
    // 플레이어 인벤토리 업로드 메서드
    public void SavePlayerInven(string userID_, List<Item> currentInven_) 
    {
        // 유저의 UID 를 이름으로 한 Firestore Document 를 생성한다 (경로 : UserData/'로그인 유저 UID'/Inventory/'로그인 유저 UID')
        DocumentReference myInven = defaultStore.Collection("UserData").Document(userID_).Collection("Inventory").Document(userID_);

        // Document 에 담을 데이터를 Dictionary 형태로 만든다
        Dictionary<int, string> items = new Dictionary<int, string>();
        int n = default;

        // foreach 를 통해서 매개변수로 받아온 Inventory(List) 를 Dictionary 에 담는다
        foreach (var item in currentInven_)
        {
            items.Add(n, item.itemName);
            n++;
        }

        // 해당 경로의 파일에 데이터를 덮어쓴다 (비동기)
        myInven.SetAsync(items).ContinueWithOnMainThread(ContinueDataUpdate);
    }
    // 업로드 처리용 Task 메서드
    private void ContinueDataUpdate(Task task_)
    {
        // 만약 실행한 Task 가 실패하거나 취소되었다면 return 으로 메서드 실행을 중지한다
        if (task_.IsCanceled || task_.IsFaulted)
        {
            Debug.LogError("Failed to Update Player Data" + "\nFailure Reason : " + task_.Exception);
            return;
        }

        Debug.Log("Player Data Update Success");
    }
    #endregion
}
