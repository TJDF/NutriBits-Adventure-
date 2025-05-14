using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10.0f;

    bool isjump = false;
    bool ishielded;
    public bool lost;

    public float jumpforce;
    public float jumptime;
    int jumpnumbers;
    public float doublejumpdooldown;
    public float gravity;
    public static bool doublep;
    public float sensorradius;
    public Transform sensor;
    public int health;
    public AudioClip[] audioClips;

    public HUDscript hud;
    public GameObject shieldsfx;
    public ParticleSystem dust;
    public ParticleSystem agua;
    public GameObject explosaoInimigo;
    public GameObject leiteicon;
    public GameObject aguaicon;
    public GameObject pesoicon;

    public Vector3 posicaoInimigo;

    private float direction;
    float currentjumptime;
    bool isground;    

    SpriteRenderer sr;
    Rigidbody2D rb;
    Collider2D c;
    Animator an;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        an = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        health = 0;
        rb.gravityScale = gravity;
        doublep = false;
        lost = false;

        hud = GameObject.FindGameObjectWithTag("hud").GetComponent<HUDscript>();
    }
    
    void Update()
    {
        an.SetInteger("pJump", (int)rb.velocity.y);
        an.SetInteger("pMove", (int)direction);

        //Input de botões
        direction = Input.GetAxis("Horizontal") * speed;
      
        if(isground) 
        {
            jumpnumbers = 1;
        }

        // Orientação do player (esquerda ou direita)
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //Verifica se o personagem está no chão
        isground = Physics2D.OverlapCircle(sensor.position, sensorradius, 1 << LayerMask.NameToLayer("ground"));

        if(health >= 100)
        {
           rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (doublep == false)
        {        
            if (Input.GetKeyDown(KeyCode.Space) && isground)
            {
                currentjumptime = jumptime;
                isjump = true;
                audioSource.clip = audioClips[0];
                audioSource.Play();
                CreateDust();
            }
            else if (Input.GetKey(KeyCode.Space) && currentjumptime > 0)
            {
                currentjumptime -= Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                currentjumptime = 0.0f;
                isjump = false;
            }
        }

        if (doublep == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpnumbers > 0)
            {
                currentjumptime = jumptime;
                jumpnumbers -= 1;
                isjump = true;
                CreateDust();
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            else if (Input.GetKey(KeyCode.Space) && currentjumptime > 0)
            {
                currentjumptime -= Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                currentjumptime = 0.0f;
                isjump = false;
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    void particulasAgua()
    {
        agua.Play();
    }
    private void FixedUpdate()
    {

        rb.velocity = new Vector2(direction, rb.velocity.y);
        
        if (currentjumptime > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FastFood")
        {
            if (rb.velocity.y < 0)
            {
               Destroy(collision.gameObject);
                audioSource.clip = audioClips[5];
                audioSource.Play();
                posicaoInimigo = new Vector3(collision.gameObject.transform.gameObject.transform.position.x,
                                    collision.gameObject.transform.gameObject.transform.position.y,
                                    collision.gameObject.transform.gameObject.transform.position.z
                                    );
                Instantiate(explosaoInimigo, posicaoInimigo, transform.rotation);
            }
            else if(ishielded == true)
            {
                Destroy(collision.gameObject);
                audioSource.clip = audioClips[6];
                audioSource.Play();
                shieldsfx.SetActive(false);
                ishielded = false;
                posicaoInimigo = new Vector3(collision.gameObject.transform.gameObject.transform.position.x,
                                    collision.gameObject.transform.gameObject.transform.position.y,
                                    collision.gameObject.transform.gameObject.transform.position.z
                                    );
                Instantiate(explosaoInimigo, posicaoInimigo, transform.rotation);
            }
            else 
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                print(rb.velocity.y);
                lost = true;
            }    
        }
   
    }

    
        

    IEnumerator doublejump()
    {
        doublep = true;
        yield return new WaitForSeconds(15.0f);
        doublep = false;
        leiteicon.SetActive(false);
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }

    IEnumerator shield()
    {
        ishielded = true;
        yield return new WaitForSeconds(15.0f);
        shieldsfx.SetActive(false);
        ishielded = false;
       pesoicon.SetActive(false);
        audioSource.clip = audioClips[4];
        audioSource.Play();
    }

    IEnumerator Velocidade()
    {
        speed = 10.0f;
        particulasAgua();
        yield return new WaitForSeconds(10.0f);
        speed = 8.0f;
        aguaicon.SetActive(false);
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Food")
        {
            health += 5;
            audioSource.clip = audioClips[1];
            audioSource.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "PowerUp")
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
            Destroy(collision.gameObject);
            StartCoroutine(Velocidade());
            aguaicon.SetActive(true);
        }

        if (collision.gameObject.tag == "Shield")
        {        
            Destroy(collision.gameObject);
            audioSource.clip = audioClips[4];
            audioSource.Play();
            shieldsfx.SetActive(true);
            StartCoroutine(shield());
            pesoicon.SetActive(true);
        }

        if (collision.gameObject.tag == "GameOver")
        {
            SceneManager.LoadScene("GameOver");
        }
   
        if (collision.gameObject.tag == "Force")
        {
            audioSource.clip = audioClips[3];
            audioSource.Play();
            Destroy(collision.gameObject);
            StartCoroutine(doublejump());
            leiteicon.SetActive(true);
        }
      

       
    
    }
}
