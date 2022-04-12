using System;
using Code.Infrastructure.Logic;
using UnityEngine;

namespace Code.Player
{
  public class PlayerHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;

    private int _current;

    public int Current
    {
      get => _current;
      set
      {
        _current = value;
        HealthChanged?.Invoke();
      }
    }

    public int Max { get; set; }

    public void TakeDamage(int damage)
    {
      if (Current <= 0)
        return;
      Current -= damage;
    }
  }
}