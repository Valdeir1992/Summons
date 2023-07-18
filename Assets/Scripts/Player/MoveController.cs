using UnityEngine;

public class MoveController : MonoBehaviour, IController<PlayerMediator>
{
    private PlayerMediator _mediator;
    private Rigidbody2D _rig;
    private Vector2 _currentVelocity;

    public void Configure(PlayerMediator mediator)
    {
        _mediator = mediator;
        _rig = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 move)
    {
        _currentVelocity = _rig.velocity;
        Vector2 desiredVelocity = move * _mediator.MaxVelocity;
        Vector2 newVelocity = Vector2.MoveTowards(_currentVelocity, desiredVelocity, 100 * Time.deltaTime);  
        _rig.velocity = newVelocity;
    }
}