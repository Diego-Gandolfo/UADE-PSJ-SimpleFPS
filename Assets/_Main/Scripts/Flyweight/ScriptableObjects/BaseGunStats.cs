using SimpleFPS.Projectiles.Bullets;
using UnityEngine;


namespace SimpleFPS.Weapons.Guns
{
    [CreateAssetMenu(fileName = "BaseGunStats", menuName = "Flyweight/Stats/Weapons/BaseGun", order = 1)]
    public class BaseGunStats : ScriptableObject
    {
        #region Serialize Fields

        [Header("Prefab")]
        [SerializeField] private Bullet _bulletPrefab;

        [Header("Ammo")]
        [SerializeField] private int _maxExtraAmmo;
        [SerializeField] private int _maxMagazineAmmo;

        [Header("Fire")]
        [SerializeField] private bool _isAutomatic;
        [SerializeField] private float _fireCooldown;

        #endregion

        #region Propertys

        // Prefab
        public Bullet BulletPrefab => _bulletPrefab;

        // Ammo
        public int MaxExtraAmmo => _maxExtraAmmo;
        public int MaxMagazineAmmo => _maxMagazineAmmo;

        // Fire
        public bool IsAutomatic => _isAutomatic;
        public float FireCooldown => _fireCooldown;

        #endregion
    }
}
