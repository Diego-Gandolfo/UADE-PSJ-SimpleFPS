using Assets._Main.Scripts.Strategy;
using System;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class AnimationsController : MonoBehaviour
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
            if (currentWeapon is BaseGunController)
            {
                if (((BaseGunController)currentWeapon).CurrentMagazineAmmo > 0)
                {
                    if (_animator.GetBool("Aim")) _animator.Play("Aim Fire", 0, 0f);
                    else _animator.Play("Fire", 0, 0f);
                }
            }
        }

        private void OnReloadHandler(IWeapon currentWeapon)
        {
            if (((BaseGunController)currentWeapon).IsMagazineEmpty && !((BaseGunController)currentWeapon).IsOutOfAmmo)
            {
                _animator.Play("Reload Out Of Ammo", 0, 0f);
            }
            else
            {
                _animator.Play("Reload Ammo Left", 0, 0f);
            }
        }

        private void OnChangeWeaponHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
                _animator = ((BaseWeaponController)currentWeapon).gameObject.GetComponent<Animator>();
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

        public void SuscribeEvents(IInputController inputController)
        {
            inputController.OnReload += OnReloadHandler;
            inputController.OnAttack += OnAttackHandler;
            inputController.OnInspect += OnInspectHander;
            inputController.OnHolster += OnHolsterHandler;
            inputController.OnThrowGrenade += OnThrowGrenadeHandler;
            inputController.OnKnifeAttack1 += OnKnifeAttack1Handler;
            inputController.OnKnifeAttack2 += OnKnifeAttack2Handler;
            inputController.OnAimOn += OnAimOnHandler;
            inputController.OnAimOff += OnAimOffHandler;
            inputController.OnChangeWeapon += OnChangeWeaponHandler;
            inputController.OnWalk += OnWalkHandler;
            inputController.OnRun += OnRunHandler;
            inputController.OnSliderAmmoLeft += OnSliderAmmoLeftHandler;
            inputController.OnSliderOutOfAmmo += OnSliderOutOfAmmoHandler;
        }

        #endregion
    }
}
