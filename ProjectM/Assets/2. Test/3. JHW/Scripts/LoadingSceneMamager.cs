using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneMamager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        Invoke("GoToStartScene", 11f);
    }


    public void GoToStartScene()
    {
        SceneManager.LoadSceneAsync("Demo");
    }

}
