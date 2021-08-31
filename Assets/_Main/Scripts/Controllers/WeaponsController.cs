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

        [SerializeField] private List<BaseWeaponController> _weaponsList = new List<BaseWeaponController>();

        #endregion

        #region Private Fields

        // Components
        private Animator _animator;

        // Parameters
        private int _currentWeaponIndex;

        #endregion

        #region Propertys

        //public Animator Animator => _animator;
        public BaseWeaponController CurrentWeapon => _weaponsList[_currentWeaponIndex];
        public List<BaseWeaponController> WeaponList => _weaponsList;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            
        }

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

        private void OnChangeWeaponHandler(BaseWeaponController currtenWeapon)
        {
            for (int i = 0; i < _weaponsList.Count; i++)
            {
                if (_weaponsList[i] == currtenWeapon)
                {
                    _currentWeaponIndex = i;
                    _weaponsList[i].gameObject.SetActive(true);
                    _animator = _weaponsList[i].gameObject.GetComponent<Animator>();
                }
                else
                {
                    _weaponsList[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnAttackHandler()
        {
            if (CurrentWeapon is BaseGunController && !IsMagazineEmpty())
            {
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
            // TODO: Throw Grenade
        }

        private void OnKnifeAttack1Handler()
        {
            // TODO: KnifeAttack1
        }

        private void OnKnifeAttack2Handler()
        {
            // TODO: KnifeAttack2
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(ICharacterBehaviour characterBehaviour)
        {
            characterBehaviour.OnReload += OnReloadHandler;
            characterBehaviour.OnAttack += OnAttackHandler;
            characterBehaviour.OnThrowGrenade += OnThrowGrenadeHandler;
            characterBehaviour.OnKnifeAttack1 += OnKnifeAttack1Handler;
            characterBehaviour.OnKnifeAttack2 += OnKnifeAttack2Handler;
            characterBehaviour.OnChangeWeapon += OnChangeWeaponHandler;
        }

        #endregion
    }
}
