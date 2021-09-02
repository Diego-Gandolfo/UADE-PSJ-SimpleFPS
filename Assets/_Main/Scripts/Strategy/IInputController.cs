using Assets._Main.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Main.Scripts.Strategy
{
    public interface IInputController
    {
        #region Events

        // Weapon
        event Action<IWeapon> OnReload;
        event Action<IWeapon> OnAttack;
        event Action OnInspect;
        event Action OnHolster;
        event Action OnKnifeAttack1;
        event Action OnKnifeAttack2;
        event Action OnThrowGrenade;
        event Action<bool> OnAim;
        event Action<IWeapon> OnChangeWeapon;
        event Action<bool> OnWalk;
        event Action<bool> OnRun;

        #endregion
    }
}
