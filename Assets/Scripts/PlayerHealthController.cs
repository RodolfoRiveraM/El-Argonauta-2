using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float invicibilityLength = 1f;
    private float invCounter;

    public GameObject deathEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SaveManager.instance.activeSave.maxHealth;
        
        currentHealth = maxHealth;

        UIManager.instance.UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(invCounter > 0)
        {
            invCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invCounter <= 0)
        {
            currentHealth -= damageAmount;
            invCounter = invicibilityLength;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                gameObject.SetActive(false);

                Instantiate(deathEffect, transform.position, transform.rotation);

                AudioManager.instance.PlaySFX(4);

                GameManager.instance.Respawn();
            }

            UIManager.instance.UpdateHealth();

            AudioManager.instance.PlaySFX(7);
        }
    }

    public void RestoreHealth(int healthToRestore)
    {
        currentHealth += healthToRestore;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIManager.instance.UpdateHealth();
    }
}
