using System;

namespace Code.Infrastructure.Logic
{
  public interface IHealth
  {
    event Action HealthChanged;
    int Max { get; set; }
    int Current { get; set; }
    void TakeDamage(int damage);
  }
}