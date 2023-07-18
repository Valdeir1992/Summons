using UnityEngine;

[CreateAssetMenu(menuName = "Prototype/Upgrades/Poison Evolved", fileName = "PosionEvolvedUpgrade")]
public sealed class PosionEvolved : Upgrade
{
    private float _attackSpeed;
    [SerializeField] private float _attackSpeedBase;
    [SerializeField] private int _damageBase;
    [SerializeField] private float _velocityBase;
    public override void Evolve(UpgradeController upgradeController)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute(UpgradeController upgradeController)
    {
        if (!isReady) return;
        _attackSpeed += Time.deltaTime;
        if (_attackSpeed <= _attackSpeedBase) return;
        _attackSpeed = 0;
        Projectile projectile = upgradeController.GetProjectile(this).Result;
        Vector2 direction = Vector2.zero;
        if (upgradeController.CurrentDirection.x > 0)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        projectile.Setup(_damageBase, direction, _velocityBase);
        projectile.gameObject.SetActive(true);
    }

    public async override void SetupUpgrade(UpgradeController controller)
    {
        isReady = false;
        level = 0;
        var task = controller.SetObjectPooling(this, 10);
        await task;
        isReady = task.Result;
    }
}
