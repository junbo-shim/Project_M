using UnityEngine;
using Firebase.Firestore;


[FirestoreData]
public class SaveDataFile : MonoBehaviour
{
    [FirestoreProperty]
    public string Savetime { get; set; }

    [FirestoreProperty]
    public string Location { get; set; }

    [FirestoreProperty]
    public string Chapter { get; set; }

    [FirestoreProperty]
    public string Quest { get; set; }
}
