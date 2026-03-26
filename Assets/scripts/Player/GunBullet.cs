using System.Collections;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 10;
    private float _damage;
    protected Rigidbody2D _rb;
    protected Transform _t;

    void Start()
    {
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfter(despawnTimer));
    }

    private IEnumerator DestroyAfter(float timer)
    {
        yield return new WaitForSeconds(timer);
        DestroyNow();
    }

    protected virtual void DestroyNow()
    {
        Destroy(gameObject);
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public virtual void Move(float force)
    {
        _rb.AddForce(force * _t.up, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.GetMask("enemies"))
        {
            collision.collider.gameObject.GetComponent<IDamageable>().takeDamage(_damage);
        }
        DestroyNow();
    }
}
