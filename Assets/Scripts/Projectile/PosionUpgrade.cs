using UnityEngine;

[CreateAssetMenu(menuName ="Prototype/Upgrades/Poison",fileName = "PosionUpgrade")]
public sealed class PosionUpgrade : Upgrade
{
    private float _attackSpeed;
    private Vector2 _direction;
    [SerializeField] private float[] _attackSpeedBase;
    [SerializeField] private int[] _damageBase;
    [SerializeField] private float[] _velocityBase;

    public override void Evolve(UpgradeController upgradeController)
    {
        int newLevel = level + 1;
        _attackSpeed = 0;
        if(newLevel == 3 && !evolved)
        {
            upgradeController.Evolve(this, evolution);
            isReady = false;
        }
        else
        {
            level = newLevel;
        }
    }

    public override void Execute(UpgradeController upgradeController)
    {
        if (!isReady) return;
        _attackSpeed += Time.deltaTime;
        if (_attackSpeed <= _attackSpeedBase[level]) return;
        _attackSpeed = 0;
        Projectile projectile = upgradeController.GetProjectile(this).Result; 
        if(upgradeController.CurrentDirection.x > 0)
        {
            _direction = Vector2.right;
        }
        else if(upgradeController.CurrentDirection.x < 0)
        {
            _direction = Vector2.left;
        } 
        projectile.Setup(_damageBase[level], _direction, _velocityBase[level]); 
        projectile.gameObject.SetActive(true);
    }

    public async override void SetupUpgrade(UpgradeController upgradeController)
    {
        isReady = false;
        level = 0;
        var task = upgradeController.SetObjectPooling(this, 10);
        await task;
        isReady = task.Result;
        _direction = Vector2.right;
    }
}
