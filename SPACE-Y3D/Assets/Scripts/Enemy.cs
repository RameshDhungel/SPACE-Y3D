using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float health = 80f;
    private float currentHealth;
    private float moveAmount;
    private float dealDamage = 20f;
    public Image healthIMG;
    
    void Start()
    {
        healthIMG = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        currentHealth = health;
        healthIMG.fillAmount = currentHealth / health;
    }

   
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {

        if((currentHealth - damage) <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            currentHealth -= damage;
            healthIMG.fillAmount = currentHealth / health;
        }
        
        //Debug.Log(health);
    }
    public float DealDamage()
    {
        return dealDamage;
    }
}
