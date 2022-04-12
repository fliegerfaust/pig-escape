using System;
using UnityEngine;

namespace Code.Enemy
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private AgentMoveToPlayer _move;

    public event Action Happened;

    private void Start() =>
      _health.HealthChanged += HealthChanged;

    private void OnDestroy() =>
      _health.HealthChanged -= HealthChanged;

    private void HealthChanged()
    {
      if (_health.Current <= 0)
        Die();
    }

    private void Die()
    {
      _health.HealthChanged -= HealthChanged;

      Happened?.Invoke();

      _move.enabled = false;

      Destroy(gameObject);
    }
  }
}