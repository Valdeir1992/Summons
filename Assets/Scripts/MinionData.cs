using UnityEngine;

[CreateAssetMenu(menuName = "Prototype/Minion/Data")] 
public class MinionData : ScriptableObject
{ 
    [SerializeField] private int _meeleDamage;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private int _maxLife;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackMeeleRadius;

    public int MeeleDamage { get => _meeleDamage; }
    public float MaxVelocity { get => _maxVelocity;}
    public int MaxLife { get => _maxLife;}
    public float AttackSpeed { get => _attackSpeed; }
    public float AttackMeeleRadius { get => _attackMeeleRadius; }
}
