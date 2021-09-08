using Assets._Main.Scripts.Component;
using Assets._Main.Scripts.Strategy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class WeaponsController : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private List<BaseWeapon> _weaponsList = new List<BaseWeapon>();

        #endregion

        #region Private Fields

        // Parameters
        private int _currentWeaponIndex;

        #endregion

        #region Propertys

        public IWeapon CurrentWeapon => _weaponsList[_currentWeaponIndex];
        public List<BaseWeapon> WeaponList => _weaponsList;


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
            if (currtenWeapon is BaseGun)
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
            // TODO: KnifeAttack1
        }

        private void OnKnifeAttack2Handler()
        {
            // TODO: KnifeAttack2
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(IInputController inputController)
        {
            inputController.OnReload += OnReloadHandler;
            inputController.OnAttack += OnAttackHandler;
            inputController.OnThrowGrenade += OnThrowGrenadeHandler;
            inputController.OnKnifeAttack1 += OnKnifeAttack1Handler;
            inputController.OnKnifeAttack2 += OnKnifeAttack2Handler;
            inputController.OnChangeWeapon += OnChangeWeaponHandler;
        }

        #endregion
    }
}
