using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
  class EnemyController : MonoBehaviour
  {
    public float damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
      var player = collision.gameObject.GetComponent<PlayerController>();

      if (player != null)
      {
        player.health.Damage(damage);
      }
    }
  }
}