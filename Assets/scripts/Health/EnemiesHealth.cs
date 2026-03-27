using UnityEngine;

public class EnemiesHealth : BaseHealth
{
    [SerializeField] private float OnCollisionDamage;
    [SerializeField] private readonly string PLAYERTAG = "Player";
    public override void Die()
    {
        // all code before this because it destroys itself after
        base.Die();
    }

    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYERTAG))
        {
            collision.gameObject.GetComponent<IDamageable>().takeDamage(OnCollisionDamage);
            Die();
        }
    }
}
