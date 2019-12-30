using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float healthStart = 100;
    private static float currentHealth;
    static PlayerController playerMov;
    private GameObject healthSlider;
    [SerializeField]
    private float giveDamage = 20f;
    
    
    bool isDead = false;
    bool damage;
    void Start()
    {
        healthSlider = GameObject.Find("HealthSlider");
        Debug.Log(healthSlider);
        playerMov = GetComponent<PlayerController>();
        currentHealth = healthStart;
    }

    void Update()
    {
       
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
        Debug.Log(healthSlider);
        healthSlider.GetComponent<Slider>().value = currentHealth;
        
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
