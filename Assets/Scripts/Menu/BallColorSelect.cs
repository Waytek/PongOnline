using UnityEngine;
using UnityEngine.UI;

public class BallColorSelect : MonoBehaviour
{
  [SerializeField]
  private Image img;
  [SerializeField]
  private Toggle toggle;

  private void Awake()
  {
    toggle.isOn = PlayerStaticData.Color == img.color;
    toggle.onValueChanged.AddListener(onToggleChanged);
  }

  private void onToggleChanged(bool toggle)
  {
    if (toggle)
    {
      PlayerStaticData.Color = img.color;
    }
  }
}
