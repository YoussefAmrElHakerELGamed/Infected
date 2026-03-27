using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
    }

    public override void Die()
    {
        // nothing for now
    }
}
