using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRenderer : MonoBehaviour
{
  public delegate void AnimationEndHandler();
  public event AnimationEndHandler OnAnimationEnd;

  Animator animator;
  SpriteRenderer spriteRenderer;

  void Awake()
  {
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void TurnLeft()
  {
    transform.parent.transform.localScale = new Vector3(-1f, 1f, 1f);
  }

  public void TurnRight()
  {
    transform.parent.transform.localScale = Vector3.one;
  }

  public void SetRunning()
  {
    animator.SetBool("running", true);
  }

  public void SetIdle()
  {
    animator.SetBool("hurt", false);
    animator.SetBool("onAir", false);
    animator.SetBool("running", false);
    animator.SetBool("dead", false);
    animator.ResetTrigger("fall");
  }

  public void Attack()
  {
    animator.SetTrigger("attack");
  }

  public void Jump()
  {
    animator.SetBool("onAir", true);
    animator.SetBool("running", false);
    animator.SetTrigger("jump");
  }

  public void Fall()
  {
    animator.SetBool("onAir", true);
    animator.SetTrigger("fall");
  }

  public void SetHurt()
  {
    animator.SetBool("hurt", true);
  }

  public void Die()
  {
    animator.SetBool("dead", true);
  }

  public void AnimationEnd()
  {
    OnAnimationEnd?.Invoke();
  }
}
