using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionLifeBar : MonoBehaviour
{
    private float _maxLife;
    private SpriteRenderer _spriteRender;
    [SerializeField] private Sprite[] _lifeSprites;  

    public void Setup(int maxLife)
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _maxLife = maxLife;
        UpdateBar(maxLife);
    } 
    public void UpdateBar(int currentLife)
    {
        int value = Mathf.CeilToInt((currentLife/_maxLife) * 4);
        _spriteRender.sprite = _lifeSprites[value];
    }
}
