using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestOffCountUI : MonoBehaviour
{
  private enum BestType
  {
    Hits,
    Goals
  }

  [SerializeField]
  private BestType bestType;

  private void Awake()
  {
    Text text = GetComponent<Text>();
    switch (bestType)
    {
      case BestType.Goals:
        text.text = PlayerStaticData.BestScoreGoals.ToString();
        break;
      case BestType.Hits:
        text.text = PlayerStaticData.BestScoreHits.ToString();
        break;
      default:
        throw new ArgumentException($"Best type {bestType} not supported");
    }
  }
}
