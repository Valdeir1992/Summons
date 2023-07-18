using UnityEngine;

public sealed class PlayerMeeleAttack : PlayerAttackController
{
    private float _waitAttack;
    [SerializeField] private LayerMask _mask;
    public override void Attack()
    {
        _waitAttack += Time.deltaTime;
        if (_waitAttack < mediator.AttackSpeed) return;
        _waitAttack = 0;
        Collider2D[] contactPoint = Physics2D.OverlapCircleAll(transform.position, mediator.AttackMeeleRadius,_mask);
        foreach (Collider2D collider in contactPoint)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(mediator.MeeleDamage);
            }
        }
    }
}
