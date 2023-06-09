using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
  public class PlayerController : EntityController
  {
    public float HSpeed;
    public float JSpeed;

    void Start()
    {
      SetupState();

      Fall();

      health.OnDeath += OnDeath;
      health.OnDamage += OnDamage;
    }

    void Update()
    {
      if (Input.anyKey)
      {
        if (IsGrounded()) SetIdle();

        if (!IsAttacking || !IsHurt)
        {
          if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
          {
            if (Input.GetKey(KeyCode.RightArrow))
            {
              TurnRight();
            }
            else
            {
              TurnLeft();
            }

            body.velocity = new Vector3(front.x * HSpeed, body.velocity.y, 0f);

            if (IsGrounded()) SetRunning();
          }

          if (IsGrounded())
          {
            if (Input.GetKeyDown(KeyCode.Space))
            {
              body.velocity = new Vector3(body.velocity.x, JSpeed, 0f);
            }

            if (Input.GetKeyDown(KeyCode.C)) Attack();
          }
        }
      }
      else
      {
        body.velocity = new Vector3(0f, body.velocity.y, 0f);

        if (IsGrounded()) SetIdle();
      }

      if (!IsGrounded())
      {
        if (body.velocity.y <= 0f)
        {
          Fall();
        }
        else
        {
          Jump();
        }
      }
    }

    void Respawn()
    {
      GameObject spawn = GameObject.Find("SpawnPoint");

      transform.position = spawn.transform.position;

      health.Respawn();

      entityRenderer.OnAnimationEnd -= Respawn;
    }

    public void OnDeath()
    {
      base.OnDeath();

      entityRenderer.OnAnimationEnd += Respawn;
    }
  }
}
