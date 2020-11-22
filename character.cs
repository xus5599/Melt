using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class character : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed = 5f;
    public bool grounded, plataforma, muerte, pared, pared_1, recoger, lanzar;
    public float jumpPower = 5f;


    private bool jump;
    private bool doublejump;

    private Rigidbody2D rb2d;
    private Animator animate;

    public int vidas;
    public Text textovidas;

    public int objrecog;
    public Text textomonedas;

    public GameObject bola;
    public int bolamunicion = 10;
    public Text municion;

    float timeAux;

    public int checkpoint;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();

        timeAux = Time.time;

        vidas = 3;
        textovidas.text = vidas.ToString();

        objrecog = 0;
        textomonedas.text = objrecog.ToString();

        

    bolamunicion = 10;
        municion.text = bolamunicion.ToString();

        checkpoint = 0;


    }

    // Update is called once per frame
    void Update()
    {
        float timeDif = Time.time - timeAux;

        animate.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        animate.SetFloat("y", Mathf.Abs(rb2d.velocity.y));
        animate.SetBool("ground", grounded);
        animate.SetBool("recoger", recoger);
        animate.SetBool("lanzar", lanzar);
        animate.SetBool("pared", pared || pared_1);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rb2d.AddForce(Vector3.right * speed * Time.deltaTime);
           
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector3(maxSpeed, rb2d.velocity.y, 0);
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rb2d.AddForce(Vector3.left * speed *Time.deltaTime);
            
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector3(-maxSpeed, rb2d.velocity.y, 0);
            }
        }

        if (Input.GetButtonDown("Jump") && timeDif > 1)
        {
            if (grounded || plataforma)
            {
                jump = true;
                doublejump = false;
                timeAux = Time.time;
            }

            else if (doublejump)
            {
                jump = true;
                doublejump = false;
                timeAux = Time.time;
            }

        }
        if (grounded || plataforma)
        {

            doublejump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (pared_1)
            {
                jump = false;

                Vector3 salto = new Vector3(-300, 250, 0);
                rb2d.AddForce(salto);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (pared)
            {
                jump = false;

                Vector3 salto = new Vector3(300, 250, 0);
                rb2d.AddForce(salto);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

    

        

        if (vidas <= 0)
        {
            Application.LoadLevel(3);
        }
        
       
        if (Input.GetKeyDown(KeyCode.S) && timeDif > 1 && bolamunicion > 0  )
        {
            
            lanzar = true;
            animate.SetBool("lanzar", true);
       
        }
        if (Input.GetKeyUp(KeyCode.S) && timeDif > 1 && bolamunicion > 0 && lanzar == true)
        {
            timeAux = Time.time;
            lanzar = false;
            animate.SetBool("lanzar", false);

            Vector3 pos = new Vector3(transform.position.x + 0.491f, transform.position.y + 0.467f, 0);
            GameObject clone = Instantiate(bola, pos, Quaternion.identity) as GameObject;
            clone.SetActive(true);

            Vector3 direccion = new Vector3(500, 200, 0);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.AddForce(direccion);


            
            bolamunicion--;
            municion.text = bolamunicion.ToString();
        }
            
        
            if (Input.GetKeyDown(KeyCode.R) && grounded == true && timeDif > 1  )
        {
            if (bolamunicion<10)
            {
                recoger = true;
                animate.SetBool("recoger", true);
                bolamunicion = bolamunicion + 1;
                municion.text = bolamunicion.ToString();
                timeAux = Time.time;
            }
            if (bolamunicion >= 10)
            {
                bolamunicion = 10;
                municion.text = bolamunicion.ToString();
            }


        }
        if (Input.GetKeyUp(KeyCode.R) && grounded == true  )
        {
            recoger = false;
            animate.SetBool("recoger", false);
        }
        if (vidas > 3)
        {
            vidas = 3;
            textovidas.text = vidas.ToString();
        }
        if (objrecog > 99)
        {
            vidas = vidas + 1 ;
            textovidas.text = vidas.ToString();
            objrecog = 0;
            textomonedas.text = objrecog.ToString();
        }
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            rb2d.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Suelo_escenario")
        {
           
            grounded = true;
            animate.SetBool("ground", true);
        }
        

        if (collision.transform.tag == "pared_rebote")
        {
            jump = false;
            
            grounded = false;
            pared = true;
            animate.SetBool("pared", true);
        }
        if (collision.transform.tag == "pared_rebote_1")
        {
            jump = false;
           
            grounded = false;
            pared_1 = true;
            animate.SetBool("pared_1", true);
        }
        if (collision.transform.tag == "Plataforma")
        {
            grounded = true;
            this.transform.parent = collision.transform;
            animate.SetBool("ground", true);

        }
        
        if (collision.transform.tag == "enemy" || collision.transform.tag == "bola")
        {
            vidas--;
            textovidas.text = vidas.ToString();
            if (checkpoint == 0)
            {
                
                textovidas.text = vidas.ToString();
                transform.position = new Vector3(-7.81f, -2.29f, 0);
            }
            if (checkpoint == 1)
            {
                
                textovidas.text = vidas.ToString();
                transform.position = new Vector3(58.9f, 4.75f, 0);
            }
           

        }
         
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint")
        {
            vidas = vidas+1;
            textovidas.text = vidas.ToString();
            checkpoint = checkpoint + 1;
            Destroy(collision.gameObject, 0);
        }

        
        if (collision.transform.tag == "moneda")
        {
            objrecog = objrecog + 1;
            textomonedas.text = objrecog.ToString();
            Destroy(collision.gameObject, 0);
        }

        if (collision.transform.tag == "final")
        {
            Application.LoadLevel(2);

        }
    }
 

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "pared_rebote_1")
        {
            pared_1 = false;
            animate.SetBool("pared", false);
        }
        if (collision.transform.tag == "pared_rebote")
        {
            pared = false;
            animate.SetBool("pared", false);
        }
        if (collision.transform.tag == "Suelo_escenario")
        {
            grounded = false;
            animate.SetBool("ground", false);
        }
       
        if (collision.transform.tag == "Plataforma")
        {
            grounded = false;
            this.transform.parent = null;
            animate.SetBool("ground", false);
        }

    }

    
}

