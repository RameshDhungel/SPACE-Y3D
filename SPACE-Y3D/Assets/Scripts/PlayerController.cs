using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Planet;
    public GameObject PlayerPlaceholder;


    public float speed = 4;
    public float JumpHeight = 1.2f;

    float gravity = 100;
    bool OnGround = false;


    float distanceToGround;
    Vector3 Groundnormal;
    float senseX = 300;
    float senseY = 300;
    float rotateAmount;
    bool rightClick = false;

    Camera mainCam;
    public GameObject crosshair;
    public Transform firepoint;
    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform weapon;
    private Vector3 mousePos;
    private bool pickupRange;
    GameObject pickUpWeapon;


    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

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
        transform.localEulerAngles += (Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * senseX);

        //Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 40000 * JumpHeight * Time.deltaTime);

        }



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


        }


        //GRAVITY and ROTATION

        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        if (OnGround == false)
        {
            rb.AddForce(gravDirection * -gravity);

        }

        //

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation; 


        Shoot();
        if(pickupRange && pickUpWeapon != null)
        {
            PickUpItems(pickUpWeapon);
        }
    }


    public void Shoot()
    {

        if (Input.GetKeyDown("mouse 1"))
        {
            rightClick = true;
        }
        if (Input.GetKeyUp("mouse 1"))
        {
            rightClick = false;
        }
        if (rightClick)
        {
            mainCam.transform.localPosition = new Vector3(1.5f, 0.5f, -3);
            crosshair.SetActive(true);

            //Vertical Rotation of the camera
            rotateAmount += Input.GetAxis("Mouse Y") * Time.deltaTime * senseY;
            rotateAmount = Mathf.Clamp(rotateAmount, -20, 16);
            mainCam.transform.localEulerAngles = Vector3.left * rotateAmount;
            mousePos = mainCam.transform.localEulerAngles;

            //Firepoint follows crosshair
            weapon.transform.localEulerAngles = Vector3.left * (rotateAmount+1);
            //weapon.rotation = new Quaternion(0, 90, mainCam.transform.rotation.x, 1);
            //weapon.rotation = mainCam.transform.localEulerAngles;
            




            if (Input.GetKeyDown("mouse 0"))
            {
                if (weapon.name != "Rocketlauncher")
                {
                    GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                }
                else
                {
                    GameObject rocket = Instantiate(rocketPrefab, firepoint.position, firepoint.rotation);
                }
                
                
            }
           
        }
        else
            {
                mainCam.transform.localPosition = new Vector3(0f, 0.5f, -5);
                mainCam.transform.localEulerAngles = new Vector3(10, 0, 0);
                rotateAmount = 0f;
                crosshair.SetActive(false);
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
            this.weapon = transform.GetChild(0).transform.GetChild(0).transform;
            firepoint = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform;
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
