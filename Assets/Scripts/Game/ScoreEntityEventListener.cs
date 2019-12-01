using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEntityEventListener : EntityEventListener<IScoreState>
{
  private ScoreUI scoreUI;

  private void Awake()
  {
    scoreUI = GetComponent<ScoreUI>();
    if (scoreUI == null)
    {
      throw new System.Exception($"Cant find ScoreUI on ScoreEntity {name}");
    }
  }

  public override void Attached()
  {
    state.AddCallback("Player1Goals", onPlayerGoalsChanged);
    state.AddCallback("Player2Goals", onPlayerGoalsChanged);
    state.AddCallback("Hits", onHitsChanged);
  }

  public override void OnEvent(GoalEven evnt)
  {
    if (entity.IsOwner)
    {
      if (evnt.HasEnemy)
      {
        if (evnt.PlayerId == "Player1")
        {
          state.Player2Goals++;
        }
        if (evnt.PlayerId == "Player2")
        {
          state.Player1Goals++;
        }
      }
      else
      {
        state.Hits = 0;
      }
    }
  }

  public override void OnEvent(HitEvent evnt)
  {
    if (entity.IsOwner)
    {
      state.Hits++;
      PlayerStaticData.BestScoreHits = state.Hits;
    }
  }

  public override void OnEvent(ScoreReset evnt)
  {
    if (entity.IsOwner)
    {
      state.Player1Goals = 0;
      state.Player2Goals = 0;
      state.Hits = 0;
    }
  }

  private void onPlayerGoalsChanged()
  {
    if (entity.IsOwner)
    {
      scoreUI.SetGoals(state.Player1Goals, state.Player2Goals);
      PlayerStaticData.BestScoreGoals = state.Player1Goals;
    }
    else
    {
      scoreUI.SetGoals(state.Player2Goals, state.Player1Goals);
      PlayerStaticData.BestScoreGoals = state.Player2Goals;
    }
  }

  private void onHitsChanged()
  {
    scoreUI.SetHits(state.Hits);
  }
}
