using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  public Health health;
  public GameObject counterObject;

  GameObject bar;

  void OnDamage(float amount)
  {
    GameObject counter = (GameObject)Instantiate(counterObject, transform);

    counter.GetComponent<DamageCounter>().amount = amount;
  }

  void Start()
  {
    health.OnDamage += OnDamage;
  }

  void Awake()
  {
    bar = GameObject.Find("Bar");

    bar.transform.localScale = new Vector3(health.maxHealth / 2f, 1f, 1f);
  }

  void Update()
  {
    float xScale = health.currentHealth / health.maxHealth;

    bar.transform.localScale = new Vector3(xScale, 0.1f, 1.0f);
  }
}
