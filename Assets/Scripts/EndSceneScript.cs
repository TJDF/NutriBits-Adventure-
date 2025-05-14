using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(19.0f);
        SceneManager.LoadScene("HomeScreen");
    }
}
