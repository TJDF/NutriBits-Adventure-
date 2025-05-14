using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(35.0f);
        SceneManager.LoadScene("Gameplay");
    }
   
}
