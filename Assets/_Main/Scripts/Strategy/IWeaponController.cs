using System.Collections.Generic;

namespace SimpleFPS.Weapons
{
    public interface IWeaponController
    {
        public IWeapon CurrentWeapon { get; }
        public List<BaseWeapon> WeaponList { get; }
}
}
