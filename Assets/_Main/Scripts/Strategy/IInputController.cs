using System;

namespace Assets._Main.Scripts.Strategy
{
    public interface IInputController
    {
        #region Events

        // Weapon
        event Action OnInspect;
        event Action OnHolster;
        event Action OnKnifeAttack1;
        event Action OnKnifeAttack2;
        event Action OnThrowGrenade;
        event Action OnAimOn;
        event Action OnAimOff;
        event Action OnSliderOutOfAmmo;
        event Action OnSliderAmmoLeft;
        event Action<bool> OnWalk;
        event Action<bool> OnRun;
        event Action<IWeapon> OnReload;
        event Action<IWeapon> OnAttack;
        event Action<IWeapon> OnChangeWeapon;

        #endregion
    }
}
