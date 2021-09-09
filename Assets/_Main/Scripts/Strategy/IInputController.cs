using System;

namespace Assets._Main.Scripts.Strategy
{
    public interface IInputController
    {
        #region Events

        // Weapon
        event Action OnKnifeAttack1, OnKnifeAttack2, OnThrowGrenade;
        event Action OnAimOn, OnAimOff, OnSliderOutOfAmmo, OnSliderAmmoLeft, OnInspect, OnHolster;
        event Action<bool> OnWalk, OnRun, OnSneak;
        event Action<bool, float> OnMove;
        event Action<IWeapon> OnReload, OnAttack, OnChangeWeapon;

        #endregion
    }
}
