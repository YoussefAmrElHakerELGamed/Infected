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
        GetComponent<SpriteRenderer>().sprite = gun.GunSprite;

        _inputActions.Player.Reload.performed += _ => Reload();
    }

    void Update()
    {
        if (_inputActions.Player.Fire.IsInProgress())
        {
            Fire();
        }
    }

    private Coroutine _fire;
    void Fire()
    {
        if (_bulletFired >= gun.GunClipSize || _reloading != null)
            return;
        _fire ??= StartCoroutine(fireGun());
    }

    private IEnumerator fireGun()
    {
        int m_RFireLoc = Random.Range(0, gun.GunFirePoints.Length);

        GameObject m_spawnedBullet = Instantiate(
            gun.GunBulletPrefab,
            // local transition y * up + x * right
            gun.GunFirePoints[m_RFireLoc].y * _t.up + gun.GunFirePoints[m_RFireLoc].x * _t.right + _t.position,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, _t.up)));

        m_spawnedBullet.GetComponent<GunBullet>().SetDamage(gun.GunDamage);
        m_spawnedBullet.GetComponent<GunBullet>().Move(gun.GunFiringForce);

        _pRb.AddForce(gun.GunRecoilForce * -_t.up, ForceMode2D.Impulse);

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
        yield return new WaitForSeconds((float)_bulletFired / gun.GunClipSize * gun.GunReloadTime);
        // visuals will be taken care of by firing events with the time
        _bulletFired = 0;
        _reloading = null;
    }
}
