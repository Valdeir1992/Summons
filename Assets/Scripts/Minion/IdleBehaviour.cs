using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Prototype/Minion/Behaviour/Idle")]
public sealed class IdleBehaviour : State
{
    public override IEnumerator Coroutine_Enter(IEntity Entity)
    {
        yield break;
    }
    public override IEnumerator Coroutine_Exit(IEntity Entity)
    {
        yield break;
    }
    public override void UpdateState(IEntity Entity)
    {
        base.UpdateState(Entity); 
    }
}
