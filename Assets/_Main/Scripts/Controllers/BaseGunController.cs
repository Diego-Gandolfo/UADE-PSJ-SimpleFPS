using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using Assets._Main.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class BaseGunController : BaseWeaponController, IGun
    {
        #region Serialize Fields

        [SerializeField] protected BaseGunStats _baseGunStats;

        #endregion

        #region Protected Fields

        protected int _currentExtraAmmo;
        protected int _currentMagazineAmmo;

        #endregion

        #region Propertys

        public int MaxExtraAmmo => _baseGunStats.MaxExtraAmmo;
        public int CurrentExtraAmmo => _currentExtraAmmo;
        public int MaxMagazineAmmo => _baseGunStats.MaxMagazineAmmo;
        public int CurrentMagazineAmmo => _currentMagazineAmmo;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _currentExtraAmmo = _baseGunStats.MaxExtraAmmo;
            _currentMagazineAmmo = _baseGunStats.MaxMagazineAmmo;
        }

        #endregion

        #region Public Methods

        public virtual void Reload() { }

        #endregion

        /// -------------------------------------------------------------------
        /// ----------------------------- TESTEOS -----------------------------
        /// -------------------------------------------------------------------

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _currentExtraAmmo = 0;
                _currentMagazineAmmo = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _currentExtraAmmo = _baseGunStats.MaxExtraAmmo;
                _currentMagazineAmmo = _baseGunStats.MaxMagazineAmmo;
            }
        }
    }
}
