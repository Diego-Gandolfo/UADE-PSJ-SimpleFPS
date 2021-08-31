using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Strategy
{
    public interface IGun : IWeapon
    {
        #region Propertys

        // Ammo
        int MaxExtraAmmo { get; }
        int CurrentExtraAmmo { get; }
        int MaxMagazineAmmo { get; }
        int CurrentMagazineAmmo { get; }

        // Fire
        bool IsAutomatic { get; }
        float FireCooldown { get; }

        #endregion

        #region Public Methods

        void Reload();

        #endregion
    }
}
