using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HUDscript : MonoBehaviour
{

    public Text Health;
    public Text PowerUp;

    public PlayerScript ps;
    
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    
    void Update()
    {
              
    }
}
