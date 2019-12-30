using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 50f;
    private float moveAmount;
    private float dealDamage = 20f;
    
    void Start()
    {
    }

   
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {

        if((health - damage) <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            health -= damage;
        }
        
        //Debug.Log(health);
    }
    public float DealDamage()
    {
        return dealDamage;
    }
}
