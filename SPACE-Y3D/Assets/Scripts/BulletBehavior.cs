using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float speed = 5000f;
    Rigidbody rb;
    
    GameObject player;
    public float Enemydamage;
    
    void Start()
    {
        //this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*1000);
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed*Time.deltaTime;
        player = GameObject.Find("Player");
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            float damage = player.GetComponent<PlayerHealth>().GiveDamage();
            collision.collider.GetComponent<Enemy>().TakeDamage(damage);
            
        }
        if (collision.collider.tag == "Player")
        {

            Debug.Log(Enemydamage);
            Debug.Log("collision");
            collision.collider.GetComponent<PlayerHealth>().TakeDamage(Enemydamage);

        }
        Destroy(this.gameObject);
    }
    


}
