using Assets._Main.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
{
    public class AsaultRifle : BaseGun
    {
        #region Public Methods

        public override void Attack(IWeaponController weaponController)
        {
            base.Attack(weaponController);
            //TODO: AsaultRifle Attack
        }

        public override void Reload()
        {
            base.Reload();
        }

        #endregion
    }
}
