using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] public float currentHealth;
    [SerializeField] private float maximumHealth;
    [SerializeField] private AudioSource playerDeath;

    public float RemainingHealthPercentage
    {
         get
         {
            return currentHealth / maximumHealth;
         }
     }
    

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    public void TakeDamage(float damageAmount)
    {
        if(currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        currentHealth -= damageAmount;

        if (Input.GetButtonDown("Fire1"))
        {
            currentHealth -= 10;
        }

        OnHealthChanged.Invoke();

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if(currentHealth == 0)
        {
            playerDeath.Play();
            OnDied.Invoke();
            LevelManager.manager.GameOver();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if(currentHealth == maximumHealth)
        {
            return;
        }

        currentHealth += amountToAdd;

        OnHealthChanged.Invoke();

        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }
}
