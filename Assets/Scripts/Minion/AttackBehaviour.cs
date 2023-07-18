using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Prototype/Minion/Behaviour/Attack")]
public sealed class AttackBehaviour : State
{
    [SerializeField] private float _waitTime;
    public override IEnumerator Coroutine_Enter(IEntity Entity)
    {
        yield return new WaitForSeconds(_waitTime);
        yield return Entity.Attack(); 
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
