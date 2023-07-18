public sealed class PoisonProjectile : Projectile
{
    protected override void ColliderAction(IDamageable target)
    {
        target.TakeDamage(damage);
        Desactive();
    }
}
