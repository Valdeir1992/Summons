using MinerMess.StudioTiziu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ClassSelectController : MonoBehaviour
{
    private Tome _selectedTome;
    private bool _wasSummoned;
    private Sprite[] _currentSprites;
    [SerializeField] private SpriteRenderer _minion;
    [SerializeField] private AssetReferenceSprite _spritesRef;
    [SerializeField] private ParticleSystem _summonParticles;
    [SerializeField] private ParticleSystem _soulParticles;
    [SerializeField] private RectTransform _containerBooks;
    [SerializeField] private GameObject[] _heros; 
    public void SelectTome(Tome currentTome)
    {
        _selectedTome = currentTome;
        _soulParticles.Play();
        _summonParticles.Play();
        var module = _soulParticles.main;
        module.startColor = currentTome.Color; 
    }
    public void SelectHero(int value)
    {
        foreach (var hero in _heros)
        {
            hero.SetActive(false);
        }
        _heros[value].gameObject.SetActive(true);
        PlayerPrefs.SetString("Hero", _heros[value].name);
    }

    private void Awake()
    {
        SetupAssets();
        SetupBooks();
    }
    private async void SetupBooks()
    {
        Task<IList<IResourceLocation>> task = Addressables.LoadResourceLocationsAsync("books").Task;
        await task;
        IResourceLocation[] books = task.Result.OrderBy(x => Guid.NewGuid()).Take(3).ToArray();
        var book1 = Addressables.InstantiateAsync(books[0]).Task;
        var book2 = Addressables.InstantiateAsync(books[1]).Task;
        var book3 = Addressables.InstantiateAsync(books[2]).Task;

        await Task.WhenAll(book1, book2, book3);
        book1.Result.GetComponent<RectTransform>().SetParent(_containerBooks);
        book2.Result.GetComponent<RectTransform>().SetParent(_containerBooks);
        book3.Result.GetComponent<RectTransform>().SetParent(_containerBooks);

    }

    private async void SetupAssets()
    {
        var task = Addressables.LoadAssetAsync<Sprite[]>(_spritesRef).Task;
        await task;
        _currentSprites = task.Result;
    }

    // Start is called before the first frame update
    void Start()
    {
        MessageSystem.Instance.Notify(new FadeMessage("FadeIn", 1.5f));
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        { 
            if (ReferenceEquals(_selectedTome, null) || !_wasSummoned)
            {
                _selectedTome = null;
                foreach (var item in FindObjectsOfType<BookButton>())
                {
                    item.Active();
                }
                _summonParticles.Stop();
                _soulParticles.Stop();
            }
            else if (_wasSummoned)
            {
                SummonUp();
            }
        }
        _soulParticles.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void SelectSummon()
    {
        if (ReferenceEquals(_selectedTome, null)) return;
        _wasSummoned = true;
        PlayerPrefs.SetString("Start Summon", _selectedTome.Upgrade);
    }

    public void DeSelectSummon()
    {
        _wasSummoned = false;
    }

    private void SummonUp()
    {
        _minion.sprite = _currentSprites.First(x => x.name == _selectedTome.CreatureBase);
        _soulParticles.Stop();
        MessageSystem.Instance.Notify(new SceneLoadMessage(("FadeOut",1),2),1);
    }
}
