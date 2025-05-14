using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerAll : MonoBehaviour
{
    public void GoToHomeScene()
    {
        SceneManager.LoadScene("HomeScreen");
    }
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void GoToStartCutsceneScene()
    {
        SceneManager.LoadScene("CutsceneStart");
    }
    public void GoToControlsScene()
    {
        SceneManager.LoadScene("Controls");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
