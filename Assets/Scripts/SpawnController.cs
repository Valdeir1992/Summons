using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class SpawnController : MonoBehaviour
{
    private IList<IResourceLocation> _creatureLevelOne;
    private IList<IResourceLocation> _creatureLevelTen;
    private IList<IResourceLocation> _creatureLevelFifteen;
    [SerializeField] private Transform[] _spawnPoint;

    private void Awake()
    {
        SetupCreature();
        Invoke(nameof(this.Summon10), 10);
        Invoke(nameof(this.Summon15), 15); 
    }

    private void Summon10()
    {
        StartCoroutine(Coroutine_SpawnCreature(10));
    }
    private void Summon15()
    {
        StartCoroutine(Coroutine_SpawnCreature(15));
    }
    private async void SetupCreature()
    {
        Task<IList<IResourceLocation>> task = Addressables.LoadResourceLocationsAsync("creatureLevelOne").Task;
        Task<IList<IResourceLocation>> task2 = Addressables.LoadResourceLocationsAsync("creatureLevelTen").Task;
        Task<IList<IResourceLocation>> task3 = Addressables.LoadResourceLocationsAsync("creatureLevelFifteen").Task;

        await Task.WhenAll(task, task2, task3);
        _creatureLevelOne = task.Result;
        _creatureLevelTen = task2.Result;
        _creatureLevelFifteen = task3.Result;

        StartCoroutine(Coroutine_SpawnCreature(1));
    }

    private IEnumerator Coroutine_SpawnCreature(int level)
    {
        float timeElapsed = 0;
        int randomValue = Random.Range(1, 10);
        while (true)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > randomValue)
            {
                randomValue = Random.Range(1, 10);
                timeElapsed = 0;
                switch (level)
                {
                    case 1:
                        Addressables.InstantiateAsync(_creatureLevelOne[Random.Range(0, _creatureLevelOne.Count)]).Completed += SetupPosition;
                        break;
                    case 10:
                        Addressables.InstantiateAsync(_creatureLevelTen[Random.Range(0, _creatureLevelTen.Count)]).Completed += SetupPosition;
                        break;
                    case 15:
                        Addressables.InstantiateAsync(_creatureLevelFifteen[Random.Range(0, _creatureLevelFifteen.Count)]).Completed += SetupPosition;
                        break;
                }
            }
            yield return null;
        }

        yield break;
    }

    private void SetupPosition(AsyncOperationHandle<GameObject> obj)
    {
        obj.Result.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
    }
}
