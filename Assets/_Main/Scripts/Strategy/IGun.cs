using SimpleFPS.Generics.Pool;
using SimpleFPS.Projectiles;

namespace SimpleFPS.Weapons
{
    public interface IGun : IWeapon
    {
        #region Propertys

        // Ammo
        int MaxExtraAmmo { get; }
        int CurrentExtraAmmo { get; }
        int MaxMagazineAmmo { get; }
        int CurrentMagazineAmmo { get; }
        bool IsMagazineEmpty { get; }
        bool IsOutOfAmmo { get; }

        // Fire
        bool IsAutomatic { get; }
        float FireCooldown { get; }
        bool CanAttack { get; }

        #endregion

        #region Public Methods

        void Reload();
        void SetBulletPool(Pool<Bullet> bulletPool);

        #endregion
    }
}
