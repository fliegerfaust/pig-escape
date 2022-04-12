using Code.Infrastructure.Logic;
using UnityEngine;
using Zenject;

namespace Code.UI.Elements
{
  public class ActorUI : MonoBehaviour
  {
    [SerializeField] private HpBar _hpBar;

    private IHealth _health;

    [Inject]
    public void Construct(IHealth health)
    {
      _health = health;
      _health.HealthChanged += UpdateHpBar;
    }

    private void Start()
    {
      IHealth health = GetComponent<IHealth>();

      if (health != null)
        Construct(health);
    }

    private void OnDestroy() =>
      _health.HealthChanged -= UpdateHpBar;

    private void UpdateHpBar() =>
      _hpBar.SetValue(_health.Current, _health.Max);
  }
}