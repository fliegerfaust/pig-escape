using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Player
{
  public class PlayerMove : MonoBehaviour
  {
    [SerializeField] private float _movementSpeed = 200f;
    [SerializeField] private Animator _animator;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private IInputService _inputService;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _movementVector;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Start() =>
      _rigidbody2D = GetComponent<Rigidbody2D>();

    private void Update()
    {
      GetInput();
      ApplyAnimation();
    }

    private void GetInput()
    {
      _movementVector = Vector3.zero;
      _movementVector = _inputService.Axis;
      _movementVector.Normalize();
    }

    private void ApplyAnimation()
    {
      _animator.SetFloat(Horizontal, _movementVector.x);
      _animator.SetFloat(Vertical, _movementVector.y);
      _animator.SetFloat(Speed, _movementVector.sqrMagnitude);
    }

    private void FixedUpdate() =>
      _rigidbody2D.velocity = _movementVector * _movementSpeed * Time.fixedDeltaTime;
  }
}