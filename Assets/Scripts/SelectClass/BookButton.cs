using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookButton : MonoBehaviour, IPointerDownHandler
{
    private Image _icon;
    private bool _canClick = true;
    private Tome _currentTome;
    [SerializeField] private string _book;
    private void Awake()
    {
        _currentTome = Tome.GetTomeByName(_book);
        _icon = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_canClick) return;
        FindAnyObjectByType<ClassSelectController>().SelectTome(_currentTome);
        foreach (var item in FindObjectsOfType<BookButton>())
        {
            item.Desactive();
        }
    }

    public void Active()
    { 
        _canClick = true;
        _icon.color = Color.white;
    }

    public void Desactive()
    { 
        _canClick = false;
        _icon.color = new Color(0.7f, 0.7f, 0.7f, 1);
    }
}

public class Tome
{
    public static Tome Demons = new Tome(Color.red,"MiniDemon","FireUpgrade");
    public static Tome Zombies = new Tome(new Color32(64, 64, 64, 255),"Zombie","PoisonUpgrade");
    public static Tome Orcs = new Tome(Color.green,"MiniOrc","EathUpgrade");
    public static Tome Monks = new Tome(new Color(0.4f, 0.5f, 1, 1), "Monk","LightUpgrade");
    private static Dictionary<string, Tome> _dictionaryTomes = new Dictionary<string, Tome>() { {Demons.CreatureBase,Demons},
        {Zombies.CreatureBase,Zombies},{Orcs.CreatureBase,Orcs},{Monks.CreatureBase,Monks}};
    private readonly Color _color;
    private readonly string _creatureBase;
    private readonly string _upgrade;
    public Color Color { get => _color; }

    public string CreatureBase { get => _creatureBase; }
    public string Upgrade { get => _upgrade; }
    private Tome(Color color, string creatureBase, string upgrade)
    {
        _color = color;
        _creatureBase = creatureBase;
        _upgrade = upgrade;
    }

    public static Tome GetTomeByName(string name)
    {
        return _dictionaryTomes[name];
    }
}
