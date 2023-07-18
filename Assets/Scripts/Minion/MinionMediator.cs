using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks; 
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MinionMediator : MonoBehaviour,IEntity, IDamageable
{
    private SpriteController _spriteController;
    private StateMachine _stateMachine;
    private EnemyAttackController _attackController;
    private EnemyHealthController _healthController;
    private EnemyAnimatorController _animatorController;
    private Vector3 _direction;
    private float _wait;
    private bool _isDead;
    private Collider2D _collider;
    [SerializeField] private AssetReferenceGameObject _soulRef;
    [SerializeField] private MinionData _data;
    [SerializeField] private State _seekPlayer;

    public float AttackSpeed { get => _data.AttackSpeed; }
    public float AttackMeeleRadius { get => _data.AttackMeeleRadius; }
    public int MeeleDamage { get => _data.MeeleDamage; }
    public Collider2D AgentCollider { get => _collider;}
    public float MaxVelocity { get => _data.MaxVelocity; }
    public int MaxLife { get => _data.MaxLife; }
    public float Wait { get => _wait; set => _wait=value; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteController = GetComponent<SpriteController>();
        _healthController = GetComponent<EnemyHealthController>();
        _attackController = GetComponent<EnemyAttackController>();
        _animatorController = GetComponent<EnemyAnimatorController>();
        _stateMachine = GetComponent<StateMachine>();


        _attackController.Configure(this);
        _healthController.Configure(this);
    }

    private void Start()
    {
        _stateMachine.ChangeBehaviour(_seekPlayer);
    }
    private void Update()
    {
        _stateMachine.UpdateBehaviour(this);
    }

    public async void Dead()
    {
        _isDead = true;
        _animatorController.Dead();
        GetComponent<Collider2D>().enabled = false;
        var task = Addressables.InstantiateAsync(_soulRef).Task;
        await task;
        task.Result.transform.position = transform.position;
        await Task.Delay(400);
        gameObject.SetActive(false);
    }

    public void Move(Vector2 direction)
    {
        if (_isDead) return;
        if (direction.x > 0)
        {
            _spriteController.TurnToRight();
        }
        else if (!Mathf.Approximately(direction.x, 0))
        {
            _spriteController.TurnToLeft();
        }
        _direction = direction.normalized * (Time.deltaTime * MaxVelocity); 
        transform.position += new Vector3(_direction.x, _direction.y,0);
        _animatorController.SetVelocity(direction);
    } 

    public Animator GetAnimator()
    {
        return _animatorController.GetAnimator();
    }

    public void ChangeBehaviour(State newState)
    {
        _wait = 0;
        _stateMachine.ChangeBehaviour(newState);
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    Vector3 IEntity.GetPosition()
    {
        return transform.position;
    }

    public void TakeDamage(int amount)
    {
        _spriteController.TakeDamage();
        _healthController.TakeDamage(amount);
    }

    public IEnumerator Attack()
    {
        if (_isDead) yield break;
        _attackController.Attack(); 
        yield return new WaitForSeconds(AttackSpeed);
    }
}
