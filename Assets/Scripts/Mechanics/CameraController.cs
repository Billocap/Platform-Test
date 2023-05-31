using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameObject target;

  // Update is called once per frame
  void Update()
  {
    Vector3 position = target.transform.position;

    transform.position = new Vector3(position.x, position.y, -10f);
  }
}
