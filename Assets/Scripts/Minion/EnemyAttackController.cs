using System.Collections;
using UnityEngine;

public abstract class EnemyAttackController : MonoBehaviour, IController<MinionMediator>
{
    protected MinionMediator mediator;
    public void Configure(MinionMediator mediator)
    {
        this.mediator = GetComponent<MinionMediator>();
    }

    public abstract void Attack();
}
