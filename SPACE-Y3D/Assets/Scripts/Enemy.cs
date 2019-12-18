using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 50f;
    private float moveAmount;
    private float dealDamage;
    
    void Start()
    {
        Debug.Log(health);
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
        
    }
    public float DealDamage()
    {
        return dealDamage;
    }
}
