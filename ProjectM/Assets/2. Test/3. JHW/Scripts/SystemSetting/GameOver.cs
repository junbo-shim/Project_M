using UnityEngine;
using UnityEditor;

public class GameOver : MonoBehaviour
{
    public void GameExit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
