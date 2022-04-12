using UnityEngine;

namespace Code.Enemy
{
  [RequireComponent(typeof(Attack))]
  public class CheckAttackRange : MonoBehaviour
  {
    private const string PlayerTag = "Player";

    [SerializeField] private Attack _attack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;

      _attack.DisableAttack();
    }

    private void TriggerEnter(Collider2D obj)
    {
      if (obj.CompareTag(PlayerTag))
        _attack.EnableAttack();
    }

    private void TriggerExit(Collider2D obj)
    {
      if (obj.CompareTag(PlayerTag))
        _attack.DisableAttack();
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= TriggerEnter;
      _triggerObserver.TriggerExit -= TriggerExit;
    }
  }
}