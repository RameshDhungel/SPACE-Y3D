using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float healthStart = 100;
    private static float currentHealth;
    PlayerController playerMov;
    public Slider healthSlider;
    [SerializeField]
    private float giveDamage = 20f;
    
    
    bool isDead = false;
    bool damage;
    void Start()
    {
        playerMov = GetComponent<PlayerController>();
        currentHealth = healthStart; 
    }

    void Update()
    {
        //once damage take damage
        //if (damage)
        //{
        //    TakeDamage();
        //}

        //damage = false;
        if (currentHealth <= 0)
        {
            Death();
            isDead = true;
        }
        
        Debug.Log("in here playerMethod" + currentHealth);
    }

    public void TakeDamage(float takenDamage)
    {

        
        damage = true;
        currentHealth -= takenDamage;
        healthSlider.value = (int)currentHealth;
        Debug.Log("healthsilder"+ healthSlider.value);
        
    }

   public void Death()
    {
        if (isDead)
        {
            GameObject Player = GameObject.Find("Player");
            Destroy(Player);
            Time.timeScale = 0;
        }


    }
    public float GiveDamage()
    {
        return giveDamage;
    }

}
