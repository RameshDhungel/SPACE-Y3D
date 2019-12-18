﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float speed = 100f;
    Rigidbody rb;
    void Start()
    {
        //this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*1000);
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<Enemy>().TakeDamage(60f);
        }
    }


}
