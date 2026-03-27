using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected float MaxHealth;
    protected float _currentHealth;
    protected bool _isAlive => _currentHealth > 0;

    void Start()
    {
        _currentHealth = MaxHealth;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void takeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }
}
