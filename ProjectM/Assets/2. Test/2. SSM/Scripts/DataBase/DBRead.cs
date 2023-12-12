using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using System;

public class DBRead : MonoBehaviour
{
    //FirestoreDb db = FirestoreDb.Create(project);
    CollectionReference usersRef;
    QuerySnapshot snapshot;
    public void Awake()
    {
  /*      usersRef = db.Collection("users");
        snapshot = await usersRef.GetSnapshotAsync();
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            Console.WriteLine("User: {0}", document.Id);
            Dictionary<string, object> documentDictionary = document.ToDictionary();
            Console.WriteLine("First: {0}", documentDictionary["First"]);
            if (documentDictionary.ContainsKey("Middle"))
            {
                Console.WriteLine("Middle: {0}", documentDictionary["Middle"]);
            }
            Console.WriteLine("Last: {0}", documentDictionary["Last"]);
            Console.WriteLine("Born: {0}", documentDictionary["Born"]);
            Console.WriteLine();
        }*/
    }
 
}
