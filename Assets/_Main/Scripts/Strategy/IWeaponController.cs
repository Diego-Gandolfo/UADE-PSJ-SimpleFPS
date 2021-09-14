using SimpleFPS.Entities.Weapons;
using SimpleFPS.Generics.Pool;
using System.Collections.Generic;

namespace SimpleFPS.Strategy.Weapons
{
    public interface IWeaponController
    {
        public Pool<Bullet> BulletPool { get; }
        public Pool<BulletImpact> BulletImpactPool { get; }
        public IWeapon CurrentWeapon { get; }
        public List<BaseWeapon> WeaponList { get; }
}
}
