using UnityEngine;

public abstract class PlayerAttackController : MonoBehaviour, IController<PlayerMediator>
{
    protected PlayerMediator mediator;
    public void Configure(PlayerMediator mediator)
    {
        this.mediator = mediator;
    }

    public abstract void Attack();
}
