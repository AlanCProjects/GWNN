using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("left")){
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey("right")){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
