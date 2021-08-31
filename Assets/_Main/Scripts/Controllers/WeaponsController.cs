using Assets._Main.Scripts.Entities;
using Assets._Main.Scripts.Strategy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class WeaponsController : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private List<BaseWeaponController> _weapons = new List<BaseWeaponController>();

        #endregion

        #region Private Fields

        // Components
        private Animator _animator;

        // Parameters
        private int _currentWeaponIndex;

        #endregion

        #region Propertys

        public Animator Animator => _animator;
        public BaseWeaponController CurrentWeapon => _weapons[_currentWeaponIndex];

        #endregion

        #region Unity Methods

        private void Update()
        {
            CheckCurrentAmmo();
        }

        #endregion

        #region Private Methods

        private void CheckCurrentAmmo()
        {
            if (CurrentWeapon is HandgunController)
                _animator.SetBool("Out Of Ammo Slider", IsMagazineEmpty());
        }

        private bool IsOutOfAmmo()
        {
            var currentExtraAmmo = ((BaseGunController)CurrentWeapon).CurrentExtraAmmo;
            var currentMagazineAmmo = ((BaseGunController)CurrentWeapon).CurrentMagazineAmmo;
            var currentTotalAmmo = currentExtraAmmo + currentMagazineAmmo;
            return (currentTotalAmmo <= 0);
        }

        private bool IsMagazineEmpty()
        {
            return (((BaseGunController)CurrentWeapon).CurrentMagazineAmmo <= 0);
        }

        private void OnAttackHandler()
        {
            if (CurrentWeapon is BaseGunController && !IsMagazineEmpty())
            {
                if (_animator.GetBool("Aim")) _animator.Play("Aim Fire", 0, 0f);
                else _animator.Play("Fire", 0, 0f);

                CurrentWeapon.Attack();
            }
        }

        private void OnReloadHandler()
        {
            if (CurrentWeapon is BaseGunController && !IsOutOfAmmo())
            {
                if (IsMagazineEmpty())
                {
                    //_animator.SetBool("Out Of Ammo Slider", _weaponController.IsMagazineEmpty());
                    _animator.Play("Reload Out Of Ammo", 0, 0f);
                }
                else
                {
                    _animator.Play("Reload Ammo Left", 0, 0f);
                }

                ((BaseGunController)CurrentWeapon).Invoke("Reload", 1.5f);
            }
        }

        private void OnThrowGrenadeHandler()
        {
            _animator.Play("GrenadeThrow", 0, 0.0f);
            // TODO: Throw Grenade
        }

        private void OnKnifeAttack1Handler()
        {
            _animator.Play("Knife Attack 1", 0, 0f);
            // TODO: KnifeAttack1
        }

        private void OnKnifeAttack2Handler()
        {
            _animator.Play("Knife Attack 2", 0, 0f);
            // TODO: KnifeAttack2
        }

        private void OnInspectHander()
        {
            _animator.SetTrigger("Inspect");
        }

        private void OnHolsterHandler()
        {
            _animator.SetBool("Holster", !_animator.GetBool("Holster"));
        }

        private void OnAimHandler(bool value)
        {
            if (!_animator.GetBool("Run"))
            {
                _animator.SetBool("Aim", value);
            }
        }

        #endregion

        #region Public Methods

        public void ChangeWeapon(int index)
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                if (i == index)
                {
                    _currentWeaponIndex = i;
                    _weapons[i].gameObject.SetActive(true);
                    _animator = _weapons[i].gameObject.GetComponent<Animator>();
                }
                else
                {
                    _weapons[i].gameObject.SetActive(false);
                }
            }
        }

        public void SuscribeEvents(ICharacterBehaviour characterBehaviour)
        {
            characterBehaviour.OnReload += OnReloadHandler;
            characterBehaviour.OnAttack += OnAttackHandler;
            characterBehaviour.OnInspect += OnInspectHander;
            characterBehaviour.OnHolster += OnHolsterHandler;
            characterBehaviour.OnThrowGrenade += OnThrowGrenadeHandler;
            characterBehaviour.OnKnifeAttack1 += OnKnifeAttack1Handler;
            characterBehaviour.OnKnifeAttack2 += OnKnifeAttack2Handler;
            characterBehaviour.OnAim += OnAimHandler;
        }

        #endregion
    }
}
