using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatmove : MonoBehaviour
{
    public int speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0f, y);
        //transform.Translate(move*speed*Time.deltaTime);
        rb.AddForce(move * speed*Time.deltaTime);
        Debug.Log("AA");
        */
        
    }
}
