using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float health = 50f;
    private float moveAmount;
    private float dealDamage = 20f;
    public GameObject enemySlider;
    
    void Start()
    {
        enemySlider = GameObject.Find("EnemySlider");
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
            enemySlider.GetComponent<Slider>().value = health;
        }
        
        //Debug.Log(health);
    }
    public float DealDamage()
    {
        return dealDamage;
    }
}
