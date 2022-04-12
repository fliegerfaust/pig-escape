using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.Input;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Player
{
  public class PlayerDrop : MonoBehaviour
  {
    private IInputService _inputService;
    private IGameFactory _gameFactory;
    private SignalBus _signalBus;

    private Rigidbody2D _rigidbody2D;

    public int Amount { get; set; }

    [Inject]
    public void Construct(IInputService inputService, IGameFactory gameFactory, SignalBus signalBus)
    {
      _inputService = inputService;
      _gameFactory = gameFactory;
      _signalBus = signalBus;
    }

    private void Start() =>
      _rigidbody2D = GetComponent<Rigidbody2D>();

    private void Update()
    {
      if (_inputService.isAttackButtonUp() && Amount > 0)
      {
        Amount -= 1;
        _signalBus.Fire(new BombsAmountChangedSignal() {Value = Amount});
        if (IsMoving())
          DropItemWhenMoving();
        else
          DropItemWhenStopping();
      }
    }

    private void DropItemWhenMoving() =>
      _gameFactory.CreateDroppable(_rigidbody2D.transform.localPosition +
                                   new Vector3(-_inputService.Axis.x, -_inputService.Axis.y));

    private void DropItemWhenStopping() =>
      _gameFactory.CreateDroppable(_rigidbody2D.transform.localPosition + Vector3.left);

    private bool IsMoving() =>
      _rigidbody2D.velocity.sqrMagnitude > 0.001f;
  }
}