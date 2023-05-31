using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
  public float amount;

  private float time;

  TextMesh textMesh;

  void Awake()
  {
    GameObject text = GameObject.Find("Text");

    textMesh = text.GetComponent<TextMesh>();
  }

  void Start()
  {
    time = 1f;
  }

  void Update()
  {
    time -= 0.01f;

    transform.Translate(0f, 0.01f, 0f);

    textMesh.text = amount.ToString();

    if (time <= 0f)
    {
      transform.gameObject.SetActive(false);
    }
  }
}
