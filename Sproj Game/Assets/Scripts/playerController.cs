using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    static Animator anim;
    public float speed ;
    public float rotationSpeed = 100.0f;
    static Animator sitting;
    public GameObject cam;
    private GameObject boatObject;
    public int bspeed;
    private Rigidbody rb;
    private Rigidbody boat;
    public bool inboat = false;
    private Vector3 offset;
    public GameObject wall;
    private Transform cam1;
    private bool start = true;
    private GameObject temp;

    private bool popup = false;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        sitting = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam1 = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inboat)
        {
            float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);

            // Jumping
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("isJumping");

            }
            /*else
            {
                anim.SetBool("isJumping", false);
            }*/

            // Moving in all directions
            if (translation != 0 || rotation !=0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }

        else if (inboat)
        {
            start = false;
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(x, 0f, y);
            //transform.Translate(move*bspeed*Time.deltaTime);
            boat.AddForce(move * bspeed * Time.deltaTime);
            /*Debug.Log(Vector3.Distance(boat.transform.position, wall.transform.position));
            //this.transform.position = new Vector3(this.transform.position.x + offset.x, this.transform.position.y + offset.y, this.transform.position.z + offset.z);
            if (Vector3.Distance(boat.transform.position, wall.transform.position) < 10)
            {

                Debug.Log("hello t u");
                Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
                rb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                rb.GetComponent<Rigidbody>().useGravity = true;
                sitting.SetBool("inBoat", false);
                Debug.Log("helloo");
                this.transform.parent = null;
                //boat.gameObject.SetActive(false);
                inboat = false;
                Debug.Log("yellow");
            }*/

            /*RaycastHit hit;
            if (Physics.Linecast(transform.position, wall.transform.position, out hit, 10))
            {
                if (hit.transform.tag == "boatstop")
                {
                    Debug.Log("Hit");
                }
            }*/
        }
        // EXPERIMENTAL CODE START
        if(popup)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                print("ok");
               
               
                temp.GetComponent<playerController>().enabled=true;
                temp.GetComponent<Collider>().enabled = true;
                temp.GetComponentInChildren<Camera>().gameObject.GetComponent<Camera>().enabled=true;
                temp.GetComponentInChildren<Camera>().gameObject.GetComponent<AudioListener>().enabled = true;
               //animator controller start
                if (temp.GetComponent<Animator>().GetBool("fallen"))        //if enemy was fallen
                {
                    temp.GetComponent<Animator>().SetBool("fallen", false); // enemy stand up
                    anim.SetBool("fallen", true);                           //player falls
                }
                else                                                       //enemy was standing
                {
                    anim.SetBool("fallen", true);                           //player falls
                    Debug.Log("enemy was standing");
                }
                //animator controller end
                temp.tag ="Player";
                tag = "enemy";
                 GetComponent<playerController>().enabled = false;
                GetComponent<Collider>().enabled = false;
                GetComponentInChildren<Camera>().gameObject.GetComponent<AudioListener>().enabled = false;
                GetComponentInChildren<Camera>().gameObject.GetComponent<Camera>().enabled = false;
                
            }
        }
        // EXPERIMENTAL CODE END

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "boat")
        {
            speed = 0f;
            rotationSpeed = 0f;
            boat = other.GetComponent<Rigidbody>();
            boatObject = other.gameObject;
            this.transform.position = other.transform.position;
            this.transform.position = new Vector3(this.transform.position.x + 0.38f, this.transform.position.y, this.transform.position.z);
            sitting.SetBool("inBoat", true);
            
            cam.transform.position = this.transform.position;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y +1.468f, cam.transform.position.z -0.853f);

            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            /*offset.x = this.transform.position.x - other.transform.position.x;
            offset.y = this.transform.position.y - other.transform.position.y;
            offset.z = this.transform.position.z - other.transform.position.z;*/

           // Destroy(this.rb);
            this.transform.SetParent(other.transform);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;


            Debug.Log("asdfg");
            inboat = true;
        }

        if(other.gameObject.tag == "boatstop")
        {
            Debug.Log("hello t u");
            //  this.gameObject.AddComponent<Rigidbody>();
            //Rigidbody rb = gameObject.GetComponent<Rigidbody>(); 
            this.transform.SetParent(null);
            //boat.gameObject.SetActive(false);
            inboat = false;
            sitting.SetBool("inBoat", false);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            if (!start)
            {
                setcam();
            }
            speed = 10;
            rotationSpeed = 100;
            Debug.Log("yellow");
        }
    }
    // EXPERIMENTAL CODE START
    
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "enemy")
        {
            popup = true;
            temp = other.gameObject;
        }
    }

    private void OnGUI()
    {
        if(popup)
        {
            GUI.Label(new Rect((Screen.width/2),(Screen.height/2),50,50),"Press [E] to possess");
        }
    }
    private void OnTriggerExit(Collider other)
    {
            popup = false;
            temp = null;
    }
    // EXPERIMENTAL CODE END











    /* private void OnCollisionEnter(Collision collision)
 {
     if(collision.gameObject.tag == "boatstop")
     {
         Debug.Log("hello t u");
         Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
         rb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
         rb.GetComponent<Rigidbody>().useGravity = true;
         sitting.SetBool("inBoat", false);
         Debug.Log("helloo");
         this.transform.parent = null;
         //boat.gameObject.SetActive(false);
         inboat = false;
         Debug.Log("yellow");
     }
 }*/
    private void setcam()
    {
        cam.transform.position = this.transform.position;
        cam.transform.position = new Vector3(cam.transform.position.x+0.01f, cam.transform.position.y + 1.92f, cam.transform.position.z - 1.09f);
    }

}
