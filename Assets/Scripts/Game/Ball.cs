using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
  public Action OnReset;

  [SerializeField]
  private float initialVelocity = 10f;
  [SerializeField]
  private float maxVelocity = 20f;
  [SerializeField]
  private float velocityIncrease = 0.5f;

  private float velocity = 10f;
  private Rigidbody2D rig;

  private void Awake()
  {
    rig = GetComponent<Rigidbody2D>();
    if (rig == null)
    {
      throw new System.Exception($"Cant find Rigidbody on Ball");
    }
  }

  private void Start()
  {
    StartCoroutine(resetCoroutine());
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Paddle")
    {
      velocity += velocityIncrease;
      velocity = Mathf.Min(velocity, maxVelocity);

      rig.velocity = rig.velocity.normalized * velocity;
      HitEvent.Create().Send();
    }
  }

  public void Reset()
  {
    StartCoroutine(resetCoroutine());
  }

  private IEnumerator resetCoroutine()
  {
    OnReset?.Invoke();

    velocity = 0f;
    rig.position = new Vector2(0, 0);
    rig.velocity = Vector2.right * velocity;

    yield return new WaitForSeconds(1);

    velocity = initialVelocity;
    rig.velocity = randomDirection() * velocity;
  }

  private Vector2 randomDirection()
  {
    switch (UnityEngine.Random.Range(0, 4))
    {
      case 0:
        return new Vector2(-0.5f, 0.5f).normalized;
      case 1:
        return new Vector2(-0.5f, -0.5f).normalized;
      case 2:
        return new Vector2(0.5f, 0.5f).normalized;
      default:
        return new Vector2(0.5f, -0.5f).normalized;
    }
  }
}
