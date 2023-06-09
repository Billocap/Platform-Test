using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
  public enum EntityState
  {
    IDLE,
    RUNNING,
    ATTACKING,
    JUMPING,
    FALLING,
    HURT,
    DEAD
  }

  public class EntityController : MonoBehaviour
  {
    public Vector2 velocity;
    public Health health;
    public EntityRenderer entityRenderer;
    public bool grounded;

    [SerializeField] protected LayerMask platformLayerMask;
    protected EntityState state;
    protected Collider2D collider2d;
    protected Rigidbody2D body;
    protected Vector3 front;

    public bool OnAir => state == EntityState.JUMPING || state == EntityState.FALLING;
    public bool IsHurt => state == EntityState.HURT;
    public bool IsAttacking => state == EntityState.ATTACKING;
    public bool IsRunning => state == EntityState.RUNNING;

    void Awake()
    {
      health = GetComponent<Health>();
      body = GetComponent<Rigidbody2D>();
      collider2d = GetComponent<Collider2D>();
    }

    void Start()
    {
      SetupState();
    }

    void Update()
    {
      VerifyOnAir();
    }

    protected void SetupState()
    {
      velocity = Vector2.zero;
      front = new Vector3(1f, 0f, 0f);
      grounded = false;
    }

    protected bool IsGrounded()
    {
      Bounds bounds = collider2d.bounds;

      float heightOffset = 0.1f;

      RaycastHit2D hit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, heightOffset, platformLayerMask);

      return hit.collider != null;
    }

    protected void VerifyOnAir()
    {
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

    public void TurnLeft()
    {
      front.x = -1f;

      entityRenderer.TurnLeft();
    }

    public void TurnRight()
    {
      front.x = 1f;

      entityRenderer.TurnRight();
    }

    public void SetHurt()
    {
      state = EntityState.HURT;

      entityRenderer.SetHurt();
    }

    public void SetRunning()
    {
      state = EntityState.RUNNING;

      entityRenderer.SetRunning();
    }

    public void SetIdle()
    {
      state = EntityState.IDLE;

      entityRenderer.SetIdle();
    }

    public void Attack()
    {
      state = EntityState.ATTACKING;

      entityRenderer.Attack();
    }

    public void Jump()
    {
      state = EntityState.JUMPING;

      entityRenderer.Jump();
    }

    public void Fall()
    {
      state = EntityState.FALLING;

      entityRenderer.Fall();
    }

    public void Die()
    {
      state = EntityState.DEAD;

      entityRenderer.Die();
    }

    public void OnDamage(float amount)
    {
      SetHurt();

      body.velocity = (front - transform.up) * -2f;
    }

    public void OnDeath()
    {
      health.OnDeath -= OnDeath;
      health.OnDamage -= OnDamage;

      Die();
    }
  }
}
