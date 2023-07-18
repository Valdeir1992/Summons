using UnityEngine;

public class HealthController : MonoBehaviour, IController<PlayerMediator>
{
    private PlayerMediator _mediator;
    private int _currentLife;
    public void Configure(PlayerMediator mediator)
    {
        _mediator = mediator;
    }

    public void TakeDamage(int amount)
    {
        _currentLife = Mathf.Clamp(_currentLife - amount, 0, _mediator.MaxLife);
        if(_currentLife == 0)
        {
            _mediator.Dead();
        }
    }

    public void Recovery(int amount)
    {
        _currentLife = Mathf.Clamp(amount, 0, _mediator.MaxLife);
    }
}
