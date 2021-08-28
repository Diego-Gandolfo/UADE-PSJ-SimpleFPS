using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Strategy
{
    public interface IWeapon
    {
        #region Propertys

        float Damage { get; }

        #endregion

        #region Public Methods

        void Attack();

        #endregion
    }
}
