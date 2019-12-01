using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
  [SerializeField]
  private string playerId;

  public void SetPlayerId(string id)
  {
    playerId = id;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    var ball = collision.gameObject.GetComponent<Ball>();
    ball?.Reset();
    var goalEvent = GoalEven.Create();
    goalEvent.PlayerId = playerId;
    goalEvent.Send();
  }

}
