using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleEntityEventListener : EntityEventListener<IPaddleState>
{
  private PaddleMove paddleMove;

  private void Awake()
  {
    paddleMove = GetComponent<PaddleMove>();
  }

  public override void Attached()
  {
    state.SetTransforms(state.PaddleTransform, transform);
  }

  public override void SimulateController()
  {
    IPlayerCommandInput input = PlayerCommand.Create();

    if (Input.GetKey(KeyCode.Mouse0))
    {
      Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      input.TargetPosition = new Vector3(position.x, transform.position.y, transform.position.z);
    }

    entity.QueueInput(input);
  }

  public override void ExecuteCommand(Command command, bool resetState)
  {
    PlayerCommand cmd = (PlayerCommand)command;

    if (!cmd.IsFirstExecution)
    {
      transform.position = cmd.Result.Position;
    }
    if (cmd.Input.TargetPosition != Vector3.zero)
    {
      paddleMove.Move(cmd.Input.TargetPosition);
    }
    cmd.Result.Position = transform.position;
  }
}
