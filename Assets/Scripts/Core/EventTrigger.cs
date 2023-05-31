using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Core
{
  public class EventTrigger : MonoBehaviour
  {
    public event EventHandler OnEvent;

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        OnEvent?.Invoke(this, EventArgs.Empty);
      }
    }
  }
}