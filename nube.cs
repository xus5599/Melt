using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nube : MonoBehaviour
{
    public float speed;
    public Transform target;
    Vector3 target2;
    private Vector3 start, end;

    // Start is called before the first frame update
    void Start()
    {
        target2 = new Vector3  (162.5f, 7.103747f, 0);
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);

        }

        if (transform.position == target.position)
        {
            transform.position = target2;
        }
    }

}
