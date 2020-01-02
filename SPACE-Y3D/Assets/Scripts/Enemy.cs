using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float health = 50f;
    private float moveAmount;
    private float dealDamage = 20f;
    public Slider enemySlider;
    
    void Start()
    {
        enemySlider = this.gameObject.GetComponentInChildren<Slider>();
        enemySlider.value = health;
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
            enemySlider.value = health;
        }
        
        //Debug.Log(health);
    }
    public float DealDamage()
    {
        return dealDamage;
    }
}
