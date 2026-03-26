using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Objects/Gun")]
public class Gun : ScriptableObject
{
    public GameObject GunBulletPrefab;
    public Vector2[] GunFirePoints;
    public int GunFireRate;
    public int GunDamage;
    public float GunRecoilForce;
    public float GunFiringForce;
    public int GunClipSize;
    public float GunReloadTime;
}
