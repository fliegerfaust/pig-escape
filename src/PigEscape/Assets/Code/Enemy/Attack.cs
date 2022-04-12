using System.Linq;
using Code.Infrastructure.Logic;
using UnityEngine;

namespace Code.Enemy
{
  public class Attack : MonoBehaviour
  {
    public float AttackCooldown { get; set; }
    public float Cleavage { get; set; }
    public int Damage { get; set; }

    private float _attackCooldown;
    private bool _isAttacking;
    private int _layerMask;
    private Collider2D[] _hits = new Collider2D[1];

    private bool _attackIsActive;

    private void Awake() =>
      _layerMask = 1 << LayerMask.NameToLayer("Player");

    private void Update()
    {
      UpdateCooldown();

      if (CanAttack())
        StartAttack();
    }

    private void OnAttack()
    {
      if (Hit(out Collider2D hit))
        hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
      OnAttackEnded();
    }

    private void OnAttackEnded()
    {
      _attackCooldown = AttackCooldown;
      _isAttacking = false;
    }

    public void EnableAttack() =>
      _attackIsActive = true;

    public void DisableAttack() =>
      _attackIsActive = false;

    private bool Hit(out Collider2D hit)
    {
      int hitsCount =
        Physics2D.OverlapCircleNonAlloc(transform.position, Cleavage, _hits, _layerMask);

      hit = _hits.FirstOrDefault();

      return hitsCount > 0;
    }

    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _attackCooldown -= Time.deltaTime;
      else
        StartAttack();
    }

    private void StartAttack()
    {
      _isAttacking = true;
      OnAttack();
    }

    private bool CanAttack() =>
      _attackIsActive && !_isAttacking && CooldownIsUp();

    private bool CooldownIsUp() =>
      _attackCooldown <= 0f;
  }
}