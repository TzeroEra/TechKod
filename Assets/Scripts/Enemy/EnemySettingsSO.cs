using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySettings", menuName = "ScriptableObjects/EnemySettings")]
public class EnemySettingsSO : ScriptableObject
{
    public float shootingRange = 5f;
    public float maxHealth = 100f;
    public float bulletSpeed = 5f;
    public float shootingInterval = 1f;
    public int shotgunPelletCount = 3;
    public float minigunShootingInterval = 0.1f;
    public int minigunBulletCount = 5;
    public float minigunSpreadAngle = 20f;
    public int pointsForDestruction = 10;
    public EnemyType enemyType;
    public WeaponType weaponType;
}