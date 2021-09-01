using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class AnimationsController : MonoBehaviour
    {
        #region Private Fields

        // Components
        private Animator _animator;

        #endregion

        #region Private Methods

        private void OnAimHandler(bool value)
        {
            if (!_animator.GetBool("Run"))
            {
                _animator.SetBool("Aim", value);
            }
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

        private void OnAttackHandler()
        {
            var w =_animator.gameObject.GetComponentInParent<WeaponsController>().CurrentWeapon;

            if (w is BaseGunController)
            {
                if (((BaseGunController)w).CurrentMagazineAmmo > 0)
                {
                    if (_animator.GetBool("Aim")) _animator.Play("Aim Fire", 0, 0f);
                    else _animator.Play("Fire", 0, 0f);
                }
            }
        }

        private void OnReloadHandler()
        {
            // TODO: Pasar animaciones de Reload
        }

        private void OnChangeWeaponHandler(IWeapon baseWeaponController)
        {
            if (baseWeaponController is BaseWeaponController)
                _animator = ((BaseWeaponController)baseWeaponController).gameObject.GetComponent<Animator>();
        }

        private void OnRunHandler(bool value)
        {
            _animator.SetBool("Run", value);
        }

        private void OnWalkHandler(bool value)
        {
            _animator.SetBool("Walk", value);
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
            inputController.OnAim += OnAimHandler;
            inputController.OnChangeWeapon += OnChangeWeaponHandler;
            inputController.OnWalk += OnWalkHandler;
            inputController.OnRun += OnRunHandler;
        }

        #endregion
    }
}
