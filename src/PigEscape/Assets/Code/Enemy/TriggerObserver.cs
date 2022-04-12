using System;
using UnityEngine;

namespace Code.Enemy
{
  [RequireComponent(typeof(Collider2D))]
  public class TriggerObserver : MonoBehaviour
  {
    public Action<Collider2D> TriggerEnter;
    public Action<Collider2D> TriggerExit;

    private void OnTriggerEnter2D(Collider2D col) =>
      TriggerEnter?.Invoke(col);

    private void OnTriggerExit2D(Collider2D col) =>
      TriggerExit?.Invoke(col);
  }
}