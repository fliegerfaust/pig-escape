using System;
using Code.Infrastructure.Logic;
using UnityEngine;

namespace Code.Enemy
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;

    private int _current;

    public int Max { get; set; }

    public int Current
    {
      get => _current;
      set
      {
        _current = value;
        HealthChanged?.Invoke();
      }
    }

    public void TakeDamage(int damage) =>
      Current -= damage;
  }
}