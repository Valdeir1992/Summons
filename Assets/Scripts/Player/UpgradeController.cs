using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class UpgradeController : MonoBehaviour,IController<PlayerMediator>
{
    private PlayerMediator _mediator; 
    private Upgrade[] _arrayUpgrade;
    private Dictionary<int, Transform> _upgradePositions = new Dictionary<int, Transform>();
    private Dictionary<int, List<Projectile>> _dictionaryProjectilePool = new Dictionary<int, List<Projectile>>(); 
    [SerializeField] private AssetReferenceSprite _spritesRef;
    [SerializeField] private Transform[] _upgradePosition;

    public Vector2 CurrentDirection { get => _mediator.CurrentDirection; }
    private void Awake()
    {
        _arrayUpgrade = new Upgrade[4];
    }
    private void Start()
    {
        SetupStartUpgrade();
    }
    private async void SetupStartUpgrade()
    {
        Task<IList<IResourceLocation>> task = Addressables.LoadResourceLocationsAsync("upgrades").Task;
        await task; 
        IResourceLocation startSummon = task.Result.FirstOrDefault(x => x.ToString().Contains(PlayerPrefs.GetString("Start Summon")));
        var taskSummon = Addressables.LoadAssetAsync<Upgrade>(startSummon).Task;
        await taskSummon; 
        SetUpgrade(taskSummon.Result, 0);
    }
    private async void SetUpgradeView(string sprite, int pos) 
    {
        var task = Addressables.LoadAssetAsync<Sprite[]>(_spritesRef).Task;
        await task;
        _upgradePosition[pos].GetComponent<SpriteRenderer>().sprite = task.Result.First(x=>x.name ==sprite);
    }

    public void SetUpgrade(Upgrade upgrade,int pos)
    {
        _arrayUpgrade[pos] = upgrade; 
        _upgradePositions.Add(upgrade.GetInstanceID(), _upgradePosition[pos]);
        upgrade.SetupUpgrade(this);
        SetUpgradeView(upgrade.UpgradeView, pos);  
    }

    public void Evolve(Upgrade current, Upgrade upgrade)
    {
        for (int i = 0; i < 4; i++)
        {
            if (ReferenceEquals(_arrayUpgrade[i], current))
            {
                _arrayUpgrade[i] = upgrade;
                _upgradePositions.Remove(current.GetInstanceID());
                ReleaseProjectiles(current);
                SetUpgrade(upgrade,i);
                return;
            }
        }
    }

    private void ReleaseProjectiles(Upgrade upgrade)
    {
        List<Projectile> projectiles = _dictionaryProjectilePool[upgrade.GetInstanceID()];
        for (int i = 0; i < projectiles.Count; i++)
        {
            Addressables.ReleaseInstance(projectiles[i].gameObject); 
        }
    }

    public void LevelUpUpgrade(int level)
    {
        _arrayUpgrade[level].Evolve(this);
    }

    private void Update()
    {
        for (int i = 0; i < _arrayUpgrade.Length; i++)
        {
            Upgrade currentUpgrade = _arrayUpgrade[i];
            if (!ReferenceEquals(currentUpgrade, null))
            {
                currentUpgrade.Execute(this);
            }
        } 
    }

    public void Configure(PlayerMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> SetObjectPooling(Upgrade type, int v)
    {
        Task<IList<IResourceLocation>> task = Addressables.LoadResourceLocationsAsync("projectile").Task;
        await task;
        Debug.Log(type.GetType().Name);
        IResourceLocation projectile = task.Result.First(x =>x.ToString().Contains(type.GetType().Name));
        if (_dictionaryProjectilePool.ContainsKey(type.GetInstanceID()))
        { 
            List<Projectile> listProjectile = _dictionaryProjectilePool[type.GetInstanceID()];
            for (int i = 0; i < v; i++)
            {
                var projectileTask = Addressables.InstantiateAsync(projectile);
                await projectileTask.Task;
                projectileTask.Result.gameObject.layer = _mediator.gameObject.layer;
                projectileTask.Result.gameObject.SetActive(false);
                Projectile result = projectileTask.Result.GetComponent<Projectile>();
                Transform position = _upgradePositions[type.GetInstanceID()];
                result.transform.SetParent(position);
                result.transform.localPosition = Vector3.zero; 
                listProjectile.Add(result);
            }
            _dictionaryProjectilePool.Add(type.GetInstanceID(), listProjectile);
        }
        else
        {
            List<Projectile> listProjectile = new List<Projectile>();
            for (int i = 0; i < v; i++)
            {
                var projectileTask = Addressables.InstantiateAsync(projectile);
                await projectileTask.Task; 
                projectileTask.Result.gameObject.layer = _mediator.gameObject.layer;
                projectileTask.Result.gameObject.SetActive(false);
                Projectile result = projectileTask.Result.GetComponent<Projectile>();
                Transform position = _upgradePositions[type.GetInstanceID()];
                result.transform.SetParent(position);
                result.transform.localPosition = Vector3.zero;
                listProjectile.Add(result);
            }
            _dictionaryProjectilePool.Add(type.GetInstanceID(), listProjectile);
        }
        return true;
    }

    public async Task<Projectile> GetProjectile(Upgrade type)
    {
        List<Projectile> listProjectile = _dictionaryProjectilePool[type.GetInstanceID()];
        var projectile = listProjectile.FirstOrDefault(x => !x.gameObject.activeSelf);

        if (ReferenceEquals(projectile, null))
        {
            var task = SetObjectPooling(type, 3);
            await task;
            return GetProjectile(type).Result;
        }
        else
        {
            return projectile;
        }
    }
} 
