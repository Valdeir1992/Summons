using UnityEngine;

public class EnemyHealthController : MonoBehaviour, IController<MinionMediator>
{
    private MinionMediator _mediator;
    private int _currentLife;
    [SerializeField] private MinionLifeBar _bar;

    public void Configure(MinionMediator mediator)
    {
        _mediator = mediator;
        _bar.Setup(_mediator.MaxLife);
    }

    public void TakeDamage(int amount)
    {
        _currentLife = Mathf.Clamp(_currentLife - amount, 0, _mediator.MaxLife);
        _bar.UpdateBar(_currentLife);
        if (_currentLife == 0)
        {
            _mediator.Dead();
        }
    }

    public void Recovery(int amount)
    {
        _currentLife = Mathf.Clamp(amount, 0, _mediator.MaxLife);
        _bar.UpdateBar(_currentLife);
    }
}
