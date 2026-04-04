using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
        GameEventBus.Instance.OnPlayerTakeDamage?.Invoke(new() { MaxHealth = MaxHealth, Health = _currentHealth, Damage = damage });
    }

    public override void Die()
    {
        // nothing for now
    }
}
