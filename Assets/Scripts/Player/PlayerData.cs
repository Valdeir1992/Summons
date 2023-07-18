using UnityEngine;

[CreateAssetMenu(menuName ="Prototype/Player/Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _maxLife;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackMeeleRadius;
    [SerializeField] private int _meeleDamage;
    public int MaxLife { get => _maxLife;}
    public float MaxVelocity { get => _maxVelocity;}
    public float AttackSpeed { get => _attackSpeed;}
    public float AttackMeeleRadius { get => _attackMeeleRadius; }
    public int MeeleDamage { get => _meeleDamage; }
}