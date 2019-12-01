using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitOtherPlayersUI : Bolt.GlobalEventListener
{
  [SerializeField]
  private GameObject waitPlayersText;

  public void Awake()
  {
    waitPlayersText.gameObject.SetActive(false);
    if (!BoltNetwork.IsSinglePlayer && BoltNetwork.IsServer)
    {
      waitPlayersText.gameObject.SetActive(true);
    }
  }
  public override void Connected(BoltConnection connection)
  {
    waitPlayersText.gameObject.SetActive(false);
  }
}
