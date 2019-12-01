using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStaticData
{
  public static Color Color
  {
    get
    {
      string colorJson = PlayerPrefs.GetString("ballColor");
      if (string.IsNullOrEmpty(colorJson))
      {
        return Color.white;
      }
      else
      {
        return JsonUtility.FromJson<Color>(colorJson);
      }
    }
    set
    {
      PlayerPrefs.SetString("ballColor", JsonUtility.ToJson(value));
    }
  }

  public static int BestScoreHits
  {
    get
    {
      return PlayerPrefs.GetInt("bestScoreHits", 0);
    }
    set
    {
      var current = PlayerPrefs.GetInt("bestScoreHits", 0);
      PlayerPrefs.SetInt("bestScoreHits", Mathf.Max(current, value));
    }
  }


  public static int BestScoreGoals
  {
    get
    {
      return PlayerPrefs.GetInt("bestScoreGoals", 0);
    }
    set
    {
      var current = PlayerPrefs.GetInt("bestScoreGoals", 0);
      PlayerPrefs.SetInt("bestScoreGoals", Mathf.Max(current, value));
    }
  }
}
