using Assets._Main.Scripts.Entities;
using Assets._Main.Scripts.Generics;
using System.Collections.Generic;

namespace Assets._Main.Scripts.Strategy
{
    public interface IWeaponController
    {
        public Pool<Bullet> BulletPool { get; }
        public Pool<BulletImpact> BulletImpactPool { get; }
        public IWeapon CurrentWeapon { get; }
        public List<BaseWeapon> WeaponList { get; }
}
}
