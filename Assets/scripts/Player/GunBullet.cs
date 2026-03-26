using System.Collections;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 10;
    private float _damage;

    void Start()
    {
        StartCoroutine(DestroyAfter(despawnTimer));
    }

    private IEnumerator DestroyAfter(float timer)
    {
        yield return new WaitForSeconds(timer);
        DestroyNow();
    }

    private void DestroyNow()
    {
        Destroy(gameObject);
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
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
