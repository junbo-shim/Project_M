using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Firestore;
using System.Collections.Generic;
using Firebase.Extensions;

public partial class FirebaseManager : MonoBehaviour
{
    // DefaultInstance 캐싱용 변수
    private FirebaseFirestore defaultStore;
    private DocumentReference playerData;

    private void CheckAndInitPlayerData(string userID_) 
    {
        Debug.LogWarning("?");
        DocumentReference docRef = defaultStore.Collection("users").Document("alovelace");

        Dictionary<string, object> user = new Dictionary<string, object>
        {
            {"Name", userID_}
        };

        //docRef.SetAsync(user).ContinueWithOnMainThread(ContinueCheckInit);
    }

    private void ContinueCheckInit(Task<DocumentReference> task_) 
    {
        // 만약 실행한 Task 가 실패하거나 취소되었다면 return 으로 메서드 실행을 중지한다
        if (task_.IsCanceled || task_.IsFaulted)
        {
            Debug.LogError("Failed to Create new Account" + "\nFailure Reason : " + task_.Result);
            return;
        }

        DocumentReference result = task_.Result;
    }

    private void LoadPlayerData() 
    {
        
    }

    private void SavePlayerData() 
    {
    
    }
}
