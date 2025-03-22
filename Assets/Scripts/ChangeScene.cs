using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }
    
    public void ExitGame()
    {

       
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    //public void Win()
    //{
    //    Time.timeScale = 1.0f;
    //    SceneManager.LoadSceneAsync(2);
    //}

    //public void Lose()
    //{
    //    Time.timeScale = 1.0f;
    //    SceneManager.LoadSceneAsync(3);
    //}

}
