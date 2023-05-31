using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
  internal Collider2D collider2d;

  public Bounds bounds => collider2d.bounds;

  void Awake()
  {
    collider2d = GetComponent<Collider2D>();
  }
}
