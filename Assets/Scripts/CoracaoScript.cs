using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoracaoScript : MonoBehaviour
{
    public PlayerScript ps;
    Animator an;
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        an = GetComponent<Animator>();  
    }

    void Update()
    {
        an.SetInteger("pHealth", ps.health);
    }
}
