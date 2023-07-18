using MinerMess.StudioTiziu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.ResourceManagement.ResourceLocations;

public class GameplayManager : MonoBehaviour
{
    public static Action<short> OnCollectSoul;
    private static PlayerMediator _player;
    private static short _amountOfSouls; 
    [SerializeField] private Transform _startPosition;

    public short Souls
    {
        get => _amountOfSouls;
    }

    public static PlayerMediator Player { get => _player;} 

    private void Awake()
    {
        _player = FindAnyObjectByType<PlayerMediator>();
        _amountOfSouls = 0;
        MessageSystem.Instance.Notify(new FadeMessage("FadeIn",1));
        SetupHero();
    }

    private async void SetupHero()
    {
        Task<IList<IResourceLocation>> task = Addressables.LoadResourceLocationsAsync("heros").Task;
        await task; 
        IResourceLocation hero = task.Result.FirstOrDefault(x=>x.ToString().Contains(PlayerPrefs.GetString("Hero")));
        var playerTask = Addressables.InstantiateAsync(hero).Task;
        await playerTask;
        PlayerMediator mediator = playerTask.Result.GetComponent<PlayerMediator>();
        mediator.transform.position = _startPosition.position;
        _player = mediator;
    }

    public static void AddSoul(short amount)
    {
        _amountOfSouls += amount;
        OnCollectSoul?.Invoke(_amountOfSouls);
    }

    public bool UseSoul(short amount)
    {
        if((_amountOfSouls - amount) >= 0)
        {
            _amountOfSouls -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GameOver()
    {
        MessageSystem.Instance.Notify(new SceneLoadMessage(("FadeOut", 1), 1));
    }
}
