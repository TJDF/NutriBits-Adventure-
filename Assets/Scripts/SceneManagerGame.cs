using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerGame : MonoBehaviour
{
    public PlayerScript ps;
    static bool s;
    static bool lose;
    Animator an;
    AudioSource audioSource;
    Rigidbody2D rb;
    Collider2D c;
    public AudioClip[] audioClips;

    private void Start()
    {
        s = false;
        lose = false;
        an = GetComponent<Animator>();
        c = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
        if(ps.health >= 100 && s == false)
        {
            ps.enabled = false;
            audioSource.clip = audioClips[0];
            audioSource.Play();
            StartCoroutine(Win());
            s = true;
        }
        if(ps.lost == true && lose == false)
        {
            ps.enabled = false;
            lose = true;
            audioSource.clip = audioClips[1];
            audioSource.Play();
            an.SetBool("pLost", lose);
            StartCoroutine(Lost());
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("CutsceneEnd");
    }

    IEnumerator Lost()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver");
    }
}
