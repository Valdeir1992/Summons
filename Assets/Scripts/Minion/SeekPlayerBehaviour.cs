using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Prototype/Minion/Behaviour/Seek")]
public sealed class SeekPlayerBehaviour : State
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
        if (ReferenceEquals(GameplayManager.Player, null)) return;
        Vector3 direction = (GameplayManager.Player.transform.position - Entity.GetPosition());
        Entity.Move(direction);
    }
}
