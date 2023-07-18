using UnityEngine;

public abstract class Transition:ScriptableObject
{
    public abstract State NextBehaviour { get; }
    public abstract bool Check(IEntity Entity);
}
