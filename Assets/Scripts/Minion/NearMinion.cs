using UnityEngine;

[CreateAssetMenu(menuName ="Prototype/Minion/Transition/Near Minion")]
public class NearMinion : Transition
{
    [SerializeField] private State _nextBehaviour;
    [SerializeField] private float _maxDistance;
    public override State NextBehaviour => _nextBehaviour;

    public override bool Check(IEntity Entity)
    {
        if (ReferenceEquals(GameplayManager.Player, null)) return false;
        Vector2 distance = (GameplayManager.Player.transform.position - Entity.GetPosition());
        float sqrDistance = distance.sqrMagnitude; 
        if(sqrDistance > (_maxDistance * _maxDistance))
        {
            return false;
        }
        return true;
    }
}

