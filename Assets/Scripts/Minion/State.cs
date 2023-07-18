using System.Collections;
using UnityEngine;

public abstract class State:ScriptableObject
{
    [SerializeField] private Transition[] _arrayTransition;
    public abstract IEnumerator Coroutine_Enter(IEntity Entity);
    public abstract IEnumerator Coroutine_Exit(IEntity Entity);

    public virtual void UpdateState(IEntity Entity)
    {
        foreach (var transition in _arrayTransition) 
        {
            if (transition.Check(Entity))
            {
                Entity.ChangeBehaviour(transition.NextBehaviour);
                return;
            }
        }
    }
}
