using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
  public class EnemyAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Update() =>
      ApplyAnimation();

    private void ApplyAnimation()
    {
      Vector3 velocity = _navMeshAgent.velocity;
      float velocityY = velocity.normalized.y;
      float velocityX = velocity.normalized.x;

      _animator.SetFloat(Horizontal, velocityX);
      _animator.SetFloat(Vertical, velocityY);
      _animator.SetFloat(Speed, velocity.sqrMagnitude);
    }
  }
}