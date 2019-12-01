using UnityEngine;
using System.Collections;
using System;
using UdpKit;
using Bolt.Matchmaking;

public class StartGameUI : Bolt.GlobalEventListener
{
  [SerializeField]
  private GameObject loadingRotator;

  private Coroutine waitServerCoroutine;

  public void PlayOnline()
  {
    BoltLauncher.StartClient();
    waitServerCoroutine = StartCoroutine(waitfromStartServer());
    loadingRotator.SetActive(true);
  }

  public void PlayOffline()
  {
    BoltLauncher.StartSinglePlayer();
    loadingRotator.SetActive(true);
  }

  public override void BoltStartDone()
  {
    if (BoltNetwork.IsServer)
    {
      string matchName = Guid.NewGuid().ToString();

      BoltMatchmaking.CreateSession(matchName, null, "Game");
    }
  }

  public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
  {
    if (waitServerCoroutine != null)
    {
      StopCoroutine(waitServerCoroutine);
      waitServerCoroutine = null;
    }
    Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

    foreach (var session in sessionList)
    {
      UdpSession photonSession = session.Value as UdpSession;

      if (photonSession.Source == UdpSessionSource.Photon)
      {
        if (photonSession.ConnectionsCurrent == 1)
        {
          BoltNetwork.Connect(photonSession);
          return;
        }
      }
    }
    changeStartClientToServer();

  }

  private IEnumerator waitfromStartServer()
  {
    yield return new WaitForSecondsRealtime(10);
    changeStartClientToServer();
  }

  private void changeStartClientToServer()
  {
    BoltLauncher.Shutdown();
    BoltLauncher.StartServer();
  }
}
