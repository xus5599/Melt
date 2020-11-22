using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaquieta : MonoBehaviour
{
    public float speedup = 3;
    public float speeddown = 0;
    public Transform targetup;
    public Transform targetdown;
    private Vector3 start, end;
    // Start is called before the first frame update
    void Start()
    {
        if (targetup != null)
        {
            targetup.parent = null;
            start = transform.position;
            end = targetup.position;
        }
        if (targetdown != null)
        {
            targetdown.parent = null;
            start = transform.position;
            end = targetdown.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float fixedSpeed = speeddown * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetdown.position, fixedSpeed);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (targetup != null)
            {
                speedup = 3;
                speeddown = 0;

                float fixedSpeed = speedup * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetup.position, fixedSpeed);

            }


        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.tag == "Player")
            {
                speedup = 0;

                if (transform.position == targetup.position)
                {
                    speeddown = 3;
                }

            }
        }



    }
}
    
