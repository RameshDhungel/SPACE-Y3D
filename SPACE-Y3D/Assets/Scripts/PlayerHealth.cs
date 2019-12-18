using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthStart = 100;
    public int currentHealth;
    PlayerMovement playerMov;
    
    bool isDead;
    bool damage;
    void Start()
    {
        playerMov = GetComponent<PlayerMovement>();
        currentHealth = healthStart; 
    }

    void Update()
    {
        //once damage take damage
        if (damage)
        {
            TakeDamage();
        }

        damage = false;
    }

    public void TakeDamage()
    {
        //subtract the currentHelath by the healthStart
        damage = true;
        currentHealth -= healthStart;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
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

}
