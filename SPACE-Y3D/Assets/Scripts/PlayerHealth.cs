using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float healthStart = 100;
    public float currentHealth;
    PlayerController playerMov;
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
    }

    public void TakeDamage(float takenDamage)
    {
        //subtract the currentHelath by the healthStart
        Debug.Log("in here playerMethod");
        damage = true;
        currentHealth -= takenDamage;

        if(currentHealth <= 0)
        {
            Debug.Log("dead");
            Death();
            isDead = true;
        }
        Debug.Log("Player health " + currentHealth);
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
