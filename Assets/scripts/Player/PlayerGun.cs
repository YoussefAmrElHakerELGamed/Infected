using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Gun gun;

    private Transform _t;
    private Rigidbody2D _pRb;
    private int _bulletFired;
    #region inputs
    private NewInputSystem _inputActions;
    void Awake()
    {
        _inputActions = new();
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.Disable();
    }
    #endregion
    void Start()
    {
        _t = transform;
        _pRb = _t.parent.GetComponent<Rigidbody2D>();
        _inputActions.Player.Fire.performed += _ => Fire();
        _inputActions.Player.Reload.performed += _ => Reload();
    }

    private Coroutine _fire;
    void Fire()
    {
        if (_bulletFired >= gun.GunClipSize)
            return;
        _fire ??= StartCoroutine(fireGun());
    }

    private IEnumerator fireGun()
    {
        int m_RFireLoc = Random.Range(0, gun.GunFirePoints.Length);

        GameObject m_spawnedBullet = Instantiate(
            gun.GunBulletPrefab,
            gun.GunFirePoints[m_RFireLoc],
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, _t.up)));

        m_spawnedBullet.GetComponent<GunBullet>().SetDamage(gun.GunDamage);
        _pRb.AddForce(gun.GunRecoilForce * -_t.up, ForceMode2D.Impulse);

        m_spawnedBullet.GetComponent<Rigidbody2D>().AddForce(gun.GunFiringForce * m_spawnedBullet.transform.up, ForceMode2D.Impulse);

        _bulletFired++;
        yield return new WaitForSeconds(1f / gun.GunFireRate);
        _fire = null;
    }

    private Coroutine _reloading;
    void Reload()
    {
        _reloading ??= StartCoroutine(reloadGun());
    }

    private IEnumerator reloadGun()
    {
        yield return new WaitForSeconds(gun.GunReloadTime);
        // visuals will be taken care of by firing events with the time
        _bulletFired = 0;
        _reloading = null;
    }
}
