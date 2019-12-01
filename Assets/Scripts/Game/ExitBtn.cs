using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour
{
  private void Awake()
  {
    var btn = GetComponent<Button>();
    btn?.onClick.AddListener(() =>
    {
      BoltLauncher.Shutdown();
      SceneManager.LoadScene(BoltScenes.Menu);
    });
  }
}
