using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float healthStart = 100;
    private static float currentHealth;
    static PlayerController playerMov;
    private Image healthIMG;

    [SerializeField]
    private float giveDamage = 20f;
    
    
    bool isDead = false;
    bool damage;
    void Start()
    {
        healthIMG = GameObject.Find("PlayerHealthbarBackground").transform.GetChild(0).GetComponent<Image>();
        playerMov = GetComponent<PlayerController>();
        currentHealth = healthStart;
        Debug.Log(healthIMG);
        healthIMG.fillAmount = currentHealth / healthStart;
    }

    void Update()
    {
       
        if (currentHealth <= 0)
        {
            Death();
            isDead = true;
        }
    }

    public void TakeDamage(float takenDamage)
    {

        
        damage = true;
        currentHealth -= takenDamage;
        healthIMG.fillAmount = currentHealth / healthStart;
        
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
