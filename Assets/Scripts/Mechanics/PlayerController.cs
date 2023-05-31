using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
  enum PlayerState
  {
    IDLE,
    RUNNING,
    ATTACKING,
    JUMPING,
    FALLING,
    HURT,
    DEAD
  }

  public class PlayerController : MonoBehaviour
  {
    public Vector2 velocity;
    public Health health;
    public PlayerRenderer playerRenderer;

    Collider2D collider2d;
    Rigidbody2D body;
    PlayerState state;
    Vector3 front;

    public Bounds bounds => collider2d.bounds;

    bool OnAir => state == PlayerState.JUMPING || state == PlayerState.FALLING;
    bool IsHurt => state == PlayerState.HURT;
    bool IsAttacking => state == PlayerState.ATTACKING;

    void Awake()
    {
      health = GetComponent<Health>();
      body = GetComponent<Rigidbody2D>();
      collider2d = GetComponent<Collider2D>();
    }

    void Start()
    {
      velocity = Vector2.zero;
      front = new Vector3(1f, 0f, 0f);

      Fall();

      health.OnDeath += OnDeath;
      health.OnDamage += OnDamage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      GroundController ground = collision.gameObject.GetComponent<GroundController>();

      if (ground != null && bounds.center.y >= ground.bounds.max.y)
      {
        SetIdle();
      }
    }

    void Update()
    {
      if (OnAir && body.velocity.y <= 0f) Fall();

      if (Input.anyKey)
      {
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

            body.velocity = new Vector3(front.x * 3f, body.velocity.y, 0f);

            if (!OnAir) SetRunning();
          }

          if (!OnAir)
          {
            if (Input.GetKeyDown(KeyCode.Space))
            {
              body.velocity = new Vector3(body.velocity.x, 3f, 0f);

              Jump();
            }

            if (Input.GetKeyDown(KeyCode.C)) Attack();
          }
        }
      }
      else
      {
        if (!OnAir) SetIdle();
      }
    }

    public void TurnLeft()
    {
      front.x = -1f;

      playerRenderer.TurnLeft();
    }

    public void TurnRight()
    {
      front.x = 1f;

      playerRenderer.TurnRight();
    }

    public void SetHurt()
    {
      state = PlayerState.HURT;

      playerRenderer.SetHurt();
    }

    public void SetRunning()
    {
      state = PlayerState.RUNNING;

      playerRenderer.SetRunning();
    }

    public void SetIdle()
    {
      state = PlayerState.IDLE;

      playerRenderer.SetIdle();
    }

    public void Attack()
    {
      state = PlayerState.ATTACKING;

      playerRenderer.Attack();
    }

    public void Jump()
    {
      state = PlayerState.JUMPING;

      playerRenderer.Jump();
    }

    public void Fall()
    {
      state = PlayerState.FALLING;

      playerRenderer.Fall();
    }

    public void Die()
    {
      state = PlayerState.DEAD;

      playerRenderer.Die();
    }

    public void OnDamage(float amount)
    {
      SetHurt();

      body.velocity = (front - transform.up) * -2f;
    }

    void Deactivate()
    {
      gameObject.SetActive(false);

      playerRenderer.OnAnimationEnd -= Deactivate;
    }

    public void OnDeath()
    {
      health.OnDeath -= OnDeath;
      health.OnDamage -= OnDamage;

      Die();

      playerRenderer.OnAnimationEnd += Deactivate;
    }
  }
}
