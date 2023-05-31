using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
  public class EventSystemController : MonoBehaviour
  {
    public event EventHandler OnSpacePressed;

    void Start()
    {
      OnSpacePressed += OnSpacePressedTrigger;
    }

    void OnSpacePressedTrigger(object sender, EventArgs e)
    {
      Debug.Log("Space");
    }

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        OnSpacePressed?.Invoke(this, EventArgs.Empty);
      }
    }

    void OnDestroy()
    {
      OnSpacePressed -= OnSpacePressedTrigger;
    }
  }

  public class Subscriber { }
}
