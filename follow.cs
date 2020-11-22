using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject seguir;
    public Vector2 mincampos, maxcampos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float posX = seguir.transform.position.x;
        float posY = seguir.transform.position.y;

        transform.position = new Vector3(
            Mathf.Clamp(posX, mincampos.x, maxcampos.x),
           Mathf.Clamp(posY, mincampos.y, maxcampos.y),
            transform.position.z);
    }
}
