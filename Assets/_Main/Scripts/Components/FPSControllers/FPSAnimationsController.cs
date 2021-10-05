using SimpleFPS.Player;
using SimpleFPS.Weapons;
using UnityEngine;

namespace SimpleFPS.FPS
{
    public class FPSAnimationsController : MonoBehaviour, IFPSController
    {
        #region Serialized Fields

        [SerializeField] private Animator _canvasAnimator;

        #endregion

        #region Private Fields

        // Components
        private Animator _weaponAnimator;

        #endregion

        #region Propertys

        public Animator Animator => _weaponAnimator;

        #endregion

        #region Private Methods

        private void OnAimOnHandler()
        {
            if (!_weaponAnimator.GetBool("Run"))
            {
                _weaponAnimator.SetBool("Aim", true);
            }
        }

        private void OnAimOffHandler()
        {
            _weaponAnimator.SetBool("Aim", false);
        }

        private void OnKnifeAttack2Handler()
        {
            _weaponAnimator.Play("Knife Attack 2", 0, 0f);
        }

        private void OnKnifeAttack1Handler()
        {
            _weaponAnimator.Play("Knife Attack 1", 0, 0f);
        }

        private void OnThrowGrenadeHandler()
        {
            _weaponAnimator.Play("GrenadeThrow", 0, 0.0f);
        }

        private void OnHolsterHandler()
        {
            _weaponAnimator.SetBool("Holster", !_weaponAnimator.GetBool("Holster"));
        }

        private void OnInspectHander()
        {
            _weaponAnimator.SetTrigger("Inspect");
        }

        private void OnAttackHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is BaseGun)
            {
                if (((BaseGun)currentWeapon).CurrentMagazineAmmo > 0)
                {
                    if (_weaponAnimator.GetBool("Aim")) _weaponAnimator.Play("Aim Fire", 0, 0f);
                    else _weaponAnimator.Play("Fire", 0, 0f);
                }
            }
        }

        private void OnReloadHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
            {
                if (((BaseGun)currentWeapon).IsMagazineEmpty && !((BaseGun)currentWeapon).IsOutOfAmmo)
                {
                    _weaponAnimator.Play("Reload Out Of Ammo", 0, 0f);
                }
                else
                {
                    _weaponAnimator.Play("Reload Ammo Left", 0, 0f);
                }
            }
        }

        private void OnChangeWeaponHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
                _weaponAnimator = ((BaseWeapon)currentWeapon).gameObject.GetComponent<Animator>();
        }

        private void OnRunHandler(bool value)
        {
            _weaponAnimator.SetBool("Run", value);
        }

        private void OnWalkHandler(bool value)
        {
            _weaponAnimator.SetBool("Walk", value);
        }

        private void OnSliderOutOfAmmoHandler()
        {
            _weaponAnimator.SetBool("Out Of Ammo Slider", true);
        }

        private void OnSliderAmmoLeftHandler()
        {
            _weaponAnimator.SetBool("Out Of Ammo Slider", false);
        }

        private void OnRecieveDamageHandler()
        {
            _canvasAnimator.SetTrigger("OnRecieveDamage");
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
            characterController.OnRecieveDamage += OnRecieveDamageHandler;
        }

        #endregion
    }
}
