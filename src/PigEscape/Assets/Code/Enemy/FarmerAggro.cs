using System.Collections;
using UnityEngine;

namespace Code.Enemy
{
  public class FarmerAggro : MonoBehaviour
  {
    private const string PlayerTag = "Player";

    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private Follow _follow;
    [SerializeField] private float _cooldown;

    private Coroutine _aggroCoroutine;
    private bool _hasAggroTarget;
    private WaitForSeconds _switchFollowAfterCooldown;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;

      _switchFollowAfterCooldown = new WaitForSeconds(_cooldown);

      SwitchFollowOff();
    }

    private void TriggerEnter(Collider2D obj)
    {
      if (!_hasAggroTarget && obj.CompareTag(PlayerTag))
      {
        _hasAggroTarget = true;
        StopAggroCoroutine();
        SwitchFollowOn();
      }
    }

    private void TriggerExit(Collider2D obj)
    {
      if (_hasAggroTarget && obj.CompareTag(PlayerTag))
      {
        _hasAggroTarget = false;
        _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
      }
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
      yield return _switchFollowAfterCooldown;
      SwitchFollowOff();
    }

    private void StopAggroCoroutine()
    {
      if (_aggroCoroutine != null)
      {
        StopCoroutine(_aggroCoroutine);
        _aggroCoroutine = null;
      }
    }

    private bool SwitchFollowOn() =>
      _follow.enabled = true;

    public bool SwitchFollowOff() =>
      _follow.enabled = false;

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= TriggerEnter;
      _triggerObserver.TriggerExit -= TriggerExit;
    }
  }
}