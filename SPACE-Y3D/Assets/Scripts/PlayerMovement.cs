﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Planet;
    public GameObject PlayerPlaceholder;


    public float speed = 4;
    public float JumpHeight = 1.2f;

    float gravity = 100;
    bool OnGround = false;


    float distanceToGround;
    Vector3 Groundnormal;
    float senseX = 100;
    bool rightClick = false;

    Camera mainCam;

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

        //Local Rotation

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * senseX);    

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
    }


    public void Shoot()
    {Debug.Log("while");

        if (Input.GetKeyDown("Fire2"))
        {
            Debug.Log("true");
            rightClick = true;
            //mainCam.transform.position = new Vector2(2.5f, -3);
        } 
        if (Input.GetKeyUp("Fire2"))
        {
            Debug.Log("false");
            rightClick = false;
        }
        if (rightClick)
        {
            mainCam.transform.position = new Vector2(2.5f, -3);
        }
        else {
            mainCam.transform.position = new Vector2(0.5f, -5);
        }


        
    }

 
}
