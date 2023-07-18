using UnityEngine;

public sealed class MeeleAttackController : EnemyAttackController
{
    [SerializeField] private LayerMask _mask; 
    public override void Attack()
    {
        Collider2D[] contactPoint = Physics2D.OverlapCircleAll(transform.position,mediator.AttackMeeleRadius, ~_mask);
        foreach (Collider2D collider in contactPoint)
        {
            if (collider != mediator.AgentCollider)
            {
                if (collider.TryGetComponent(out IDamageable target))
                {
                    target.TakeDamage(mediator.MeeleDamage);
                }
            }
        }
    }
}
