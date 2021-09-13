using Assets._Main.Scripts.Components;
using Assets._Main.Scripts.Entities;
using Assets._Main.Scripts.Generics;
using Assets._Main.Scripts.Strategy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class WeaponsController : MonoBehaviour, IWeaponController
    {
        #region Serialize Fields

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;
        [SerializeField] private List<BaseWeapon> _weaponsList = new List<BaseWeapon>();
        [SerializeField] private MeleeAttack _cutAttack;
        [SerializeField] private MeleeAttack _stabAttack;

        #endregion

        #region Private Fields

        // Parameters
        private int _currentWeaponIndex;

        // Pool
        protected Pool<Bullet> _bulletPool;
        protected Pool<BulletImpact> _bulletImpactPool;

        #endregion

        #region Propertys

        public Pool<Bullet> BulletPool => _bulletPool;
        public Pool<BulletImpact> BulletImpactPool => _bulletImpactPool;
        public IWeapon CurrentWeapon => _weaponsList[_currentWeaponIndex];
        public List<BaseWeapon> WeaponList => _weaponsList;


        #endregion

        #region Unity Methods

        private void Start()
        {
            _bulletPool = new Pool<Bullet>(_bulletPrefab);
            _bulletImpactPool = new Pool<BulletImpact>(_bulletImpactPrefab);
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
                if (!((IGun)currtenWeapon).IsMagazineEmpty) currtenWeapon.Attack(this);
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
