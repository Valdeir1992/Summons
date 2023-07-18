using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMediator : MonoBehaviour, IDamageable
{
    private bool _isDead; 
    private SpriteController _spriteController;
    private HealthController _healthController;
    private MoveController _moveController; 
    private Vector2 _currentDirection;
    private UpgradeController _upgradeController;
    [SerializeField] private PlayerData _data; 
    private int _modMaxLife;
    private int _modMaxVelocity;
    public int MaxLife { get => _data.MaxLife + _modMaxLife; }
    public float MaxVelocity { get => _data.MaxVelocity + _modMaxVelocity; }
    public float AttackMeeleRadius { get => _data.AttackMeeleRadius;} 
    public float AttackSpeed { get => _data.AttackSpeed; }
    public int MeeleDamage { get => _data.MeeleDamage;}

    public Vector2 CurrentDirection { get => _currentDirection; }

    private void Awake()
    { 
        _healthController = GetComponent<HealthController>();
        _moveController = GetComponent<MoveController>();
        _spriteController = GetComponent<SpriteController>(); 
        _upgradeController = GetComponent<UpgradeController>();

        _healthController.Configure(this);
        _moveController.Configure(this); 
        _upgradeController.Configure(this);

        CinemachineVirtualCamera camera = FindAnyObjectByType<CinemachineVirtualCamera>();
        camera.LookAt = transform;
        camera.Follow = transform;
    }
    private void Update()
    {
        _currentDirection = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))).normalized; 
        Move(_currentDirection); 
    }

    public void TakeDamage(int amount)
    {
        if (_isDead) return;
        _spriteController.TakeDamage();
        _healthController.TakeDamage(amount);
    }

    public void Move(Vector2 direction)
    { 
        if(direction.x > 0)
        {
            _spriteController.TurnToRight();
        }
        else if(!Mathf.Approximately(direction.x,0))
        {
            _spriteController.TurnToLeft();
        } 
        _moveController.Move(direction);
    }

    public async void Dead()
    {
        Task.Delay(500);
        FindAnyObjectByType<GameplayManager>().GameOver();
    }
}

public interface IController<T>
{
    public void Configure(T mediator);
}

public interface IDamageable
{
    void TakeDamage(int damage);
}
