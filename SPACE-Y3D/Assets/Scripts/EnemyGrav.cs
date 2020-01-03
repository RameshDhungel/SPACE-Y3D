using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrav : MonoBehaviour
{

    private GameObject Planet;
    public GameObject PlayerPlaceholder;

    float gravity = 10000;
    bool OnGround = false;


    float distanceToGround;
    Vector3 Groundnormal;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Planet = GameObject.FindGameObjectWithTag("Planet");
    }
    void Update()
    {
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



        //

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
transform.rotation = toRotation;   
    }
    private void FixedUpdate()
    {
        //GRAVITY and ROTATION

        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        if (OnGround == false)
        {
            rb.AddForce(gravDirection* -gravity * Time.deltaTime);

        }
        
    }

}
