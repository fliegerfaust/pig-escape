using UnityEngine;
using Zenject;

namespace Code.Player
{
  [RequireComponent(typeof(PlayerHealth))]
  public class PlayerDeath : MonoBehaviour
  {
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerDrop _playerDrop;

    private bool _isDead;
    private Rigidbody2D _rigidbody2D;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) =>
      _signalBus = signalBus;

    private void Start()
    {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _health.HealthChanged += HealthChanged;
    }

    private void OnDestroy() =>
      _health.HealthChanged -= HealthChanged;

    private void HealthChanged()
    {
      if (!_isDead && _health.Current <= 0)
        Die();
    }

    private void Die()
    {
      _isDead = true;
      _signalBus.Fire<PlayerDiedSignal>();

      _rigidbody2D.velocity = Vector2.zero;
      _move.enabled = false;
      _playerDrop.enabled = false;
    }
  }
}