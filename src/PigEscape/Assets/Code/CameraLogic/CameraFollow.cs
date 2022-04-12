using Cinemachine;
using UnityEngine;
using Zenject;

namespace Code.CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
    private CinemachineVirtualCamera _cinemachine;
    private PolygonCollider2D _confiner;

    [Inject]
    public void Construct(PolygonCollider2D confiner)
    {
      _confiner = confiner;

      _cinemachine = GetComponent<CinemachineVirtualCamera>();
      _cinemachine.GetComponent<CinemachineConfiner>().m_BoundingShape2D = _confiner;
    }

    public void Follow(Transform target) =>
      _cinemachine.Follow = target;
  }
}