using UnityEngine;

[CreateAssetMenu(menuName = "Prototype/Minion/Transition/Wait")]
public class Wait: Transition
{ 
    [SerializeField] private float _time;
    [SerializeField] private State _nextBehaviour;

    public override State NextBehaviour => _nextBehaviour;

    public override bool Check(IEntity Entity)
    {
        Entity.Wait += Time.deltaTime;
        return Entity.Wait > _time;
    }
} 

