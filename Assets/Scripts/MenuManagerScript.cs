using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuControls;
    bool InMenu = false;
    bool jogoPausado;
    bool isControls =false;
    void Start()
    {
        menu.SetActive(false);
        menuControls.SetActive(false);
        InMenu = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isControls)
        {
            if (!InMenu)
            {
                menu.SetActive(true);
                InMenu = true;
                Pausar();
            }
            else if (InMenu)
            {
                menu.SetActive(false);               
                InMenu = false;
                Pausar();
            }
        }
    }

    public void GoToHomeScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("HomeScreen");
    }

    public void MenuControls()
    {
        menu.SetActive(false);
        menuControls.SetActive(true);
        isControls = true;
    }

    public void GoToMenuFromControls()
    {
        menu.SetActive(true);
        menuControls.SetActive(false);
        isControls = false;
    }

    public void Resume()
    {
        if (!InMenu)
        {
            menu.SetActive(true);
            InMenu = true;
            Pausar();
        }
        else if (InMenu)
        {
            menu.SetActive(false);
            InMenu = false;
            Pausar();
        }
    }

    private void Pausar()
    {
        if(!jogoPausado)
        {
            Time.timeScale = 0f;
            jogoPausado = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            jogoPausado= false;
        }
        
    }
}

