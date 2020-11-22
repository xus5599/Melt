using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public Transform target;
    public bool target1;
   
    
    
    
    public int vidas;

    private Rigidbody2D rb2d;
    private Animator animate;

    

    public GameObject bola;

   

    // Start is called before the first frame update
    void Start()
    {   vidas =  2 ;
        rb2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        animate.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        animate.SetBool("player", target1);
       
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "bola")
        {
            vidas--;

            if(vidas == 0)
            {
                Destroy(gameObject, 0);
            }


        }
    } 
}
