using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GunBullet : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 10;
    private float _damage;
    protected Rigidbody2D _rb;
    protected Transform _t;

    void Awake()
    {
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(DestroyAfter(despawnTimer));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enemies"))
        {
            collision.collider.gameObject.GetComponent<IDamageable>().takeDamage(_damage);
        }
        DestroyNow();
    }

    private IEnumerator DestroyAfter(float timer)
    {
        yield return new WaitForSeconds(timer);
        DestroyNow();
    }

    protected virtual void DestroyNow() => Destroy(gameObject);

    public void SetDamage(float damage) => _damage = damage;

    public void Move(float force)
    {
        _rb.AddForce(force * _t.up, ForceMode2D.Impulse);
    }
}
