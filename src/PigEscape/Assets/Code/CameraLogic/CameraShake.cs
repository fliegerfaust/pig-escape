using Cinemachine;
using UnityEngine;

namespace Code.CameraLogic
{
  public class CameraShake : MonoBehaviour
  {
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
      _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
      _cinemachineBasicMultiChannelPerlin =
        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float time)
    {
      SetIntensity(intensity);

      _startingIntensity = intensity;
      _shakeTimerTotal = time;
      _shakeTimer = time;
    }

    private void Update()
    {
      if (_shakeTimer > 0)
      {
        _shakeTimer -= Time.deltaTime;
        if (_shakeTimer <= 0f)
          SetIntensity(Mathf.Lerp(_startingIntensity, 0, 1 - _shakeTimer / _shakeTimerTotal));
      }
    }

    private void SetIntensity(float intensity) =>
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
  }
}