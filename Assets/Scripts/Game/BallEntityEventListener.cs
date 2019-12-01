using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEntityEventListener : EntityEventListener<IBallState>
{
  private SpriteRenderer spriteRenderer;
  private Ball ball;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    if(spriteRenderer == null)
    {
      throw new System.Exception("Cant find Sprite Renderer on ball");
    }
    ball = GetComponent<Ball>();
    if (ball == null)
    {
      throw new System.Exception("Cant find Ball component on ball");
    }
    ball.OnReset += onBallReset;
  }

  public override void Attached()
  {
    state.SetTransforms(state.BallTransform, transform);

    state.AddCallback("BallColor", onColorChanged);

    state.AddCallback("BallScale", onScaleChanged);

    if (entity.IsOwner)
    {
      state.BallColor = PlayerStaticData.Color;
    }
  }

  private void onColorChanged()
  {
    spriteRenderer.color = state.BallColor;
  }

  private void onScaleChanged()
  {
    transform.localScale = Vector3.one * state.BallScale;
  }

  private void onBallReset()
  {
    if (entity.IsOwner)
    {
      state.BallScale = Random.Range(1f, 2f);
    }
  }
}
