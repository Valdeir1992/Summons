public sealed class PoisonEvolvedProjectile : Projectile
{
    protected override void ColliderAction(IDamageable target)
    {
        target.TakeDamage(damage);
    }
}
