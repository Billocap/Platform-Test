using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  public float maxHealth;

  internal float currentHealth;

  public delegate void OnHealHandler(float amount);
  public event OnHealHandler OnHeal;

  public delegate void OnDamageHandler(float amount);
  public event OnDamageHandler OnDamage;

  public delegate void OnDeathHandler();
  public event OnDeathHandler OnDeath;

  void Awake()
  {
    currentHealth = maxHealth;
  }

  public void Respawn()
  {
    currentHealth = maxHealth;
  }

  public void Heal(float amount)
  {
    currentHealth = Mathf.Max(currentHealth + amount, maxHealth);

    OnHeal?.Invoke(amount);
  }

  public void Damage(float amount)
  {
    currentHealth = Mathf.Max(currentHealth - amount, 0.0f);

    if (currentHealth == 0.0f)
    {
      OnDeath?.Invoke();
    }
    else
    {
      OnDamage?.Invoke(amount);
    }
  }
}
