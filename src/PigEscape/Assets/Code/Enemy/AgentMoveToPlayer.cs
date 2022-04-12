using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private GameObject _player;

    [Inject]
    public void Construct(GameObject player)
    {
      _player = player;

      _navMeshAgent.updateRotation = false;
      _navMeshAgent.updateUpAxis = false;
      _navMeshAgent.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    private void Update() =>
      SetDestination();

    private void SetDestination() =>
      _navMeshAgent.destination = _player.transform.position;
  }
}