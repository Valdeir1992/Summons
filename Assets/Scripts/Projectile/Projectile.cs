using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Vector3 direction;
    protected float speed;
    protected int damage;
    protected Transform startPosition;
    public void Setup(int damage, Vector3 direction, float speed)
    {
        this.damage = damage;
        this.direction = direction;
        this.speed = speed;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
        Invoke(nameof(this.Desactive), 3);
        this.startPosition = transform.parent;
    }
    private void OnEnable()
    {
        if (ReferenceEquals(this.startPosition, null)) return;
        transform.position = this.startPosition.position;
        transform.SetParent(null);
    }
    private void OnDisable()
    {
        if (ReferenceEquals(this.startPosition, null)) return; 
        transform.SetParent(this.startPosition);
        transform.localPosition = Vector3.zero;
    }
    private void Update()
    { 
        transform.position += this.direction * (Time.deltaTime * this.speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer) return;

        if(collision.TryGetComponent(out IDamageable target))
        {
            ColliderAction(target);
        }
    }
    protected abstract void ColliderAction(IDamageable target);

    protected void Desactive()
    {
        gameObject.SetActive(false); 
    }
}
