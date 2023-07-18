using System.Collections; 
using UnityEngine; 

public class StateMachine : MonoBehaviour
{
    private State _currrentState;
    private bool _isTransition;  
    public void UpdateBehaviour(IEntity entity)
    {
        if (_isTransition || ReferenceEquals(_currrentState,null)) return;
        _currrentState?.UpdateState(entity);
    }
    public void ChangeBehaviour(State newState)
    {
        _isTransition = true;
        StartCoroutine(Coroutine_ChangeBehaviour(newState));
    }

    private IEnumerator Coroutine_ChangeBehaviour(State state)
    {
        yield return state.Coroutine_Exit(GetComponent<IEntity>());
        _currrentState = state;
        yield return _currrentState.Coroutine_Enter(GetComponent<IEntity>());
        _isTransition = false;
    }
} 

public interface IEntity
{
    public float Wait { get; set; }
    public void Move(Vector2 direction);
    public Animator GetAnimator(); 
    public void ChangeBehaviour(State newState); 
    public Vector3 GetPosition(); 
    public IEnumerator Attack();
}
