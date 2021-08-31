using Assets._Main.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Main.Scripts.Strategy
{
    public interface ICharacterBehaviour
    {
        #region Events

        // Weapon
        event Action OnReload;
        event Action OnAttack;
        event Action OnInspect;
        event Action OnHolster;
        event Action OnKnifeAttack1;
        event Action OnKnifeAttack2;
        event Action OnThrowGrenade;
        event Action<bool> OnAim;
        event Action<BaseWeaponController> OnChangeWeapon;
        event Action<bool> OnWalk;
        event Action<bool> OnRun;

        #endregion
    }
}
