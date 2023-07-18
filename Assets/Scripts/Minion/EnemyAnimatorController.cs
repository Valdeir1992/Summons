using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Dead()
    {
        _animator.SetBool("IsDead", true);
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public void SetVelocity(Vector2 direction)
    {
        _animator.SetFloat("Velocity", direction.sqrMagnitude);
        _animator.SetBool("Idle", Mathf.Approximately(direction.sqrMagnitude, 0));
    }
}
