using Code.CameraLogic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Logic.Droppable
{
  public class Bomb : MonoBehaviour
  {
    private const float CameraShakeIntensity = 2f;
    private const float CameraShakeTime = .5f;
    private const int TimeToDestroy = 1;

    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _bombSprite;

    private CameraShake _cameraShake;

    public int Damage { get; set; }

    [Inject]
    public void Construct(CameraShake cameraShake) =>
      _cameraShake = cameraShake;

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.TryGetComponent(out IHealth health))
      {
        health.TakeDamage(Damage);
        ShakeCamera();
        ActivateExplosion();
        DeactivatePrefab();
        DestroyObject();
      }
    }

    private void ShakeCamera() =>
      _cameraShake.Shake(CameraShakeIntensity, CameraShakeTime);

    private void ActivateExplosion() =>
      _explosionPrefab.SetActive(true);

    private void DeactivatePrefab() =>
      _bombSprite.SetActive(false);

    private void DestroyObject() =>
      Destroy(gameObject, TimeToDestroy);
  }
}