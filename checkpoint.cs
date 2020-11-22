using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    
    private Animator animate;
    

    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

             
            animate.SetBool("active", true);

        }
       
            

            
        
    }
    
   
}

