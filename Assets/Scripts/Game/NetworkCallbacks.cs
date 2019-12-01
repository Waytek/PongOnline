using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Game")]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
  private BoltEntity scoreEntity;
  private bool hasEnemy;
  private Ball currentBall;
  private BoltEntity otherPaddle;

  public override void Connected(BoltConnection connection)
  {
    spawnOrResetEnemyPaddle();

    otherPaddle.AssignControl(connection);

    spawnOrResetBall();

    ScoreReset.Create(scoreEntity).Send();

    hasEnemy = true;
  }

  public override void Disconnected(BoltConnection connection)
  {
    spawnOrResetEnemyPaddle();

    otherPaddle.TakeControl();

    spawnOrResetBall();

    ScoreReset.Create(scoreEntity).Send();

    hasEnemy = false;
  }

  public override void SceneLoadLocalDone(string map)
  {
    var spawnPosition = new Vector3(0, -50, 0);

    var paddleEntity = BoltNetwork.Instantiate(BoltPrefabs.Paddle, spawnPosition, Quaternion.Euler(0, 0, 90));

    paddleEntity.TakeControl();

    spawnOrResetEnemyPaddle();

    otherPaddle.TakeControl();

    spawnOrResetBall();

    spawnGoalsOnServer();

    scoreEntity = BoltNetwork.Instantiate(BoltPrefabs.ScoreCanvas);

    ScoreReset.Create(scoreEntity).Send();
  }

  public override void OnEvent(GoalEven evnt)
  {
      var goalEvent = GoalEven.Create(scoreEntity);
      goalEvent.PlayerId = evnt.PlayerId;
      goalEvent.HasEnemy = hasEnemy;
      goalEvent.Send();
  }

  public override void OnEvent(HitEvent evnt)
  {
    if (!hasEnemy)
    {
      HitEvent.Create(scoreEntity).Send();
    }
  }

  private void spawnOrResetBall()
  {

    if (currentBall)
    {
      currentBall.Reset();
    }
    else
    {
      var ball = BoltNetwork.Instantiate(BoltPrefabs.Ball, Vector2.zero, Quaternion.identity);
      currentBall = ball.GetComponent<Ball>();
    }
  }

  private void spawnOrResetEnemyPaddle()
  {
    if (otherPaddle)
    {
      Destroy(otherPaddle.gameObject);
    }
    var spawnPosition = new Vector3(0, 50, 0);

    otherPaddle = BoltNetwork.Instantiate(BoltPrefabs.Paddle, spawnPosition, Quaternion.Euler(0, 0, 90));
  }

  private void spawnGoalsOnServer()
  {
    var spawnPosition = new Vector3(0, -75, 0);

    var goal = BoltNetwork.Instantiate(BoltPrefabs.Goal, spawnPosition, Quaternion.identity);

    goal.GetComponent<Goal>()?.SetPlayerId("Player1");

    spawnPosition = new Vector3(0, 75, 0);

    goal = BoltNetwork.Instantiate(BoltPrefabs.Goal, spawnPosition, Quaternion.identity);

    goal.GetComponent<Goal>()?.SetPlayerId("Player2");
  }
}
