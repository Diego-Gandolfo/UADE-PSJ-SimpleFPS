using System.Collections.Generic;
using UnityEngine;
using SimpleFPS.Weapons;
using SimpleFPS.LevelManagers;
using SimpleFPS.Player;

namespace SimpleFPS.FPS
{
    public class FPSWeaponsController : MonoBehaviour, IFPSController
    {
        #region Serialize Fields

        [SerializeField] private List<BaseWeapon> _weaponsList = new List<BaseWeapon>();
        [SerializeField] private MeleeAttack _cutAttack;
        [SerializeField] private MeleeAttack _stabAttack;

        #endregion

        #region Private Fields

        // Parameters
        private int _currentWeaponIndex;

        #endregion

        #region Propertys

        public IWeapon CurrentWeapon => _weaponsList[_currentWeaponIndex];
        public List<BaseWeapon> WeaponList => _weaponsList;


        #endregion

        #region Unity Methods

        private void Start()
        {
            foreach (var weapon in _weaponsList)
            {
                if (weapon is IGun)
                {
                    ((IGun)weapon).SetBulletPool(LevelManager.Instance.PlayerBulletPool);
                }
            }
        }

        #endregion

        #region Private Methods

        private void OnChangeWeaponHandler(IWeapon currtenWeapon)
        {
            for (int i = 0; i < _weaponsList.Count; i++)
            {
                if (_weaponsList[i] == (BaseWeapon)currtenWeapon)
                {
                    _currentWeaponIndex = i;
                    _weaponsList[i].gameObject.SetActive(true);
                }
                else
                {
                    _weaponsList[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnAttackHandler(IWeapon currtenWeapon)
        {
            if (currtenWeapon is IGun)
            {
                if (!((IGun)currtenWeapon).IsMagazineEmpty) currtenWeapon.Attack();
            }
        }

        private void OnReloadHandler(IWeapon currtenWeapon)
        {
            if (currtenWeapon is IGun && !((IGun)currtenWeapon).IsOutOfAmmo)
            {
                ((BaseGun)currtenWeapon).Invoke("Reload", 1.5f);
            }
        }

        private void OnThrowGrenadeHandler()
        {
            // TODO: Throw Grenade
        }

        private void OnKnifeAttack1Handler()
        {
            _cutAttack.gameObject.SetActive(true);
            Invoke("DoStabAttack", _cutAttack.AutoDisabler.TimeToDisable);
        }

        private void OnKnifeAttack2Handler()
        {
            _cutAttack.gameObject.SetActive(true);
        }

        private void DoStabAttack()
        {
            _stabAttack.gameObject.SetActive(true);
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(FPSCharacterController characterController)
        {
            characterController.OnReload += OnReloadHandler;
            characterController.OnAttack += OnAttackHandler;
            characterController.OnThrowGrenade += OnThrowGrenadeHandler;
            characterController.OnKnifeAttack1 += OnKnifeAttack1Handler;
            characterController.OnKnifeAttack2 += OnKnifeAttack2Handler;
            characterController.OnChangeWeapon += OnChangeWeaponHandler;
        }

        #endregion
    }
}
