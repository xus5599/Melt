using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orca : MonoBehaviour
{
    
    public Transform targetup;
    public Transform targetdown;
    
    private Animator animate;
    public bool morder;
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animate.SetBool("morder", morder);
 
        if (transform.position == targetup.position )
        {

            morder = true;
        }

       
        if (transform.position == targetdown.position)
        {
           
            morder = false;
        }
    }



}

