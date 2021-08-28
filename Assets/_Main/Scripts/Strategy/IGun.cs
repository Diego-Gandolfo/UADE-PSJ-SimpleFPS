using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Strategy
{
    public interface IGun : IWeapon
    {
        #region Propertys

        int MaxExtraAmmo { get; }
        int CurrentExtraAmmo { get; }
        int MaxMagazineAmmo { get; }
        int CurrentMagazineAmmo { get; }

        #endregion

        #region Public Methods

        void Reload();

        #endregion
    }
}
