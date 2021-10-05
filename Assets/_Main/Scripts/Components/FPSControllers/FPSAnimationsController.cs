using SimpleFPS.Player;
using SimpleFPS.Weapons;
using UnityEngine;

namespace SimpleFPS.FPS
{
    public class FPSAnimationsController : MonoBehaviour, IFPSController
    {
        #region Private Fields

        // Components
        private Animator _animator;

        #endregion

        #region Propertys

        public Animator Animator => _animator;

        #endregion

        #region Private Methods

        private void OnAimOnHandler()
        {
            if (!_animator.GetBool("Run"))
            {
                _animator.SetBool("Aim", true);
            }
        }

        private void OnAimOffHandler()
        {
            _animator.SetBool("Aim", false);
        }

        private void OnKnifeAttack2Handler()
        {
            _animator.Play("Knife Attack 2", 0, 0f);
        }

        private void OnKnifeAttack1Handler()
        {
            _animator.Play("Knife Attack 1", 0, 0f);
        }

        private void OnThrowGrenadeHandler()
        {
            _animator.Play("GrenadeThrow", 0, 0.0f);
        }

        private void OnHolsterHandler()
        {
            _animator.SetBool("Holster", !_animator.GetBool("Holster"));
        }

        private void OnInspectHander()
        {
            _animator.SetTrigger("Inspect");
        }

        private void OnAttackHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is BaseGun)
            {
                if (((BaseGun)currentWeapon).CurrentMagazineAmmo > 0)
                {
                    if (_animator.GetBool("Aim")) _animator.Play("Aim Fire", 0, 0f);
                    else _animator.Play("Fire", 0, 0f);
                }
            }
        }

        private void OnReloadHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
            {
                if (((BaseGun)currentWeapon).IsMagazineEmpty && !((BaseGun)currentWeapon).IsOutOfAmmo)
                {
                    _animator.Play("Reload Out Of Ammo", 0, 0f);
                }
                else
                {
                    _animator.Play("Reload Ammo Left", 0, 0f);
                }
            }
        }

        private void OnChangeWeaponHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
                _animator = ((BaseWeapon)currentWeapon).gameObject.GetComponent<Animator>();
        }

        private void OnRunHandler(bool value)
        {
            _animator.SetBool("Run", value);
        }

        private void OnWalkHandler(bool value)
        {
            _animator.SetBool("Walk", value);
        }

        private void OnSliderOutOfAmmoHandler()
        {
            _animator.SetBool("Out Of Ammo Slider", true);
        }

        private void OnSliderAmmoLeftHandler()
        {
            _animator.SetBool("Out Of Ammo Slider", false);
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(FPSCharacterController characterController)
        {
            characterController.OnReload += OnReloadHandler;
            characterController.OnAttack += OnAttackHandler;
            characterController.OnInspect += OnInspectHander;
            characterController.OnHolster += OnHolsterHandler;
            characterController.OnThrowGrenade += OnThrowGrenadeHandler;
            characterController.OnKnifeAttack1 += OnKnifeAttack1Handler;
            characterController.OnKnifeAttack2 += OnKnifeAttack2Handler;
            characterController.OnAimOn += OnAimOnHandler;
            characterController.OnAimOff += OnAimOffHandler;
            characterController.OnChangeWeapon += OnChangeWeaponHandler;
            characterController.OnWalk += OnWalkHandler;
            characterController.OnRun += OnRunHandler;
            characterController.OnSliderAmmoLeft += OnSliderAmmoLeftHandler;
            characterController.OnSliderOutOfAmmo += OnSliderOutOfAmmoHandler;
        }

        #endregion
    }
}
