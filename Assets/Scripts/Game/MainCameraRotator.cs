using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraRotator : MonoBehaviour
{
  private void Awake()
  {
    if (BoltNetwork.IsClient)
    {
      transform.rotation = Quaternion.Euler(0, 0, 180);
    }
  }
}
