using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
  [SerializeField]
  private Text scoreText;

  public void SetGoals(int playerGoals, int enemyGoals)
  {
    scoreText.text = $"Goals:{playerGoals}/{enemyGoals}";
  }

  public void SetHits(int hits)
  {
    scoreText.text = $"Hits:{hits}";
  }
}
