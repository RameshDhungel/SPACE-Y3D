using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Planet;
    public GameObject PlayerPlaceholder;
    GameObject pickUpWeapon;
    private Rigidbody rb;


    public float speed = 4;
    public float JumpHeight = 1.2f;
    float distanceToGround;
    float gravity = 10000;
    float senseX = 300;
    float senseY = 300;
    bool jumping = false;

    bool OnGround = false;
    private bool pickupRange;

    Vector3 Groundnormal;

    Shooting shootingScript;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        shootingScript = FindObjectOfType<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {

        //MOVEMENT

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, 0, z);

        //Local Rotation x axis

        //transform.localRotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * senseX);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * senseX);

        //GroundControl

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.2f)
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }

            /*if (distanceToGround <= 1.2f)
            {
                jumping = false;
            }
            else
            {
                jumping = true;
            }*/
        }


        


        //GRAVITY and ROTATION


        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation; 


        
        if(pickupRange && pickUpWeapon != null)
        {
            PickUpItems(pickUpWeapon);
        }
    }
    private void FixedUpdate()
    {
        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        if (OnGround == false)
        {
            rb.AddForce(gravDirection * -gravity * Time.fixedDeltaTime);
           

        }

        //Jump
        Debug.Log(jumping);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OnGround)
            {
                rb.AddForce(transform.up * 800000 * JumpHeight * Time.deltaTime);
            }

        }
    }

    public void PickUpItems(GameObject weapon)
    {
        Debug.Log("pickup");
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("f");
            GameObject currentWeapon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            currentWeapon.transform.parent = null; // Drops guns from player to the world
            currentWeapon.AddComponent<Rigidbody>(); // adds rigidBody so it can fall with gravity
            currentWeapon.AddComponent<BoxCollider>(); // adds boxcollider so it collids with the ground 


            Destroy(weapon.GetComponent<BoxCollider>()); // Destorys the box collider of the weapon that just got picked up so player wont get stuck
            Destroy(weapon.GetComponent<Rigidbody>()); // Destroys rigid body cuz we dont need gravity after player picks up weapon

            weapon.transform.SetParent(transform.GetChild(0).transform); // set picked up weapon transfrom player child 
            weapon.transform.position = currentWeapon.transform.position;
            weapon.transform.rotation = currentWeapon.transform.rotation;

            shootingScript.weapon = transform.GetChild(0).transform.GetChild(0).transform;

            shootingScript.firepoint = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform;
            //currentWeapon.SetActive(false);




        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            pickUpWeapon = collision.gameObject;
            pickupRange = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            pickUpWeapon = null;
            pickupRange = false;
        }
    }

}
