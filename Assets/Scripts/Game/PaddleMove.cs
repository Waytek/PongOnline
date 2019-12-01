using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
  [SerializeField]
  private float speed = 1f;

  public void SetPosition(Vector3 position)
  {
    transform.position = position;
  }

  public void Move(Vector3 position)
  {
    transform.position = Vector2.Lerp(transform.position, position, speed * BoltNetwork.FrameDeltaTime);
  }
}
