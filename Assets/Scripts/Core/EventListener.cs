using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Core
{
  public class EventListener : MonoBehaviour
  {
    public EventTrigger eventTrigger;

    void Awake()
    {
      eventTrigger = GetComponent<EventTrigger>();
    }

    void OnEvent(object sender, EventArgs e)
    {
      Debug.Log("event happened");
    }

    void Start()
    {
      eventTrigger.OnEvent += OnEvent;
    }

    void OnDestroy()
    {
      eventTrigger.OnEvent -= OnEvent;
    }
  }
}