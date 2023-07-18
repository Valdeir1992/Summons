using UnityEngine;

public abstract class Upgrade:ScriptableObject
{
    protected bool isReady = false;
    protected int level;
    [SerializeField] protected bool evolved;
    [SerializeField] private string _upgradeView;
    [SerializeField] protected Upgrade evolution;
    public string UpgradeView { get => _upgradeView;}

    public abstract void Execute(UpgradeController upgradeController);

    public abstract void SetupUpgrade(UpgradeController controller);

    public abstract void Evolve(UpgradeController upgradeController);
}
