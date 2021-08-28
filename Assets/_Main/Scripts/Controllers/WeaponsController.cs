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

        public void Attack()
        {
            CurrentWeapon.Attack();
        }

        public void Reload()
        {
            ((BaseGunController)CurrentWeapon).Reload();
        }

        public bool IsOutOfAmmo()
        {
            var currentExtraAmmo = ((BaseGunController)CurrentWeapon).CurrentExtraAmmo;
            var currentMagazineAmmo = ((BaseGunController)CurrentWeapon).CurrentMagazineAmmo;
            var currentTotalAmmo = currentExtraAmmo + currentMagazineAmmo;
            return (currentTotalAmmo <= 0);
        }

        public bool IsMagazineEmpty()
        {
            return (((BaseGunController)CurrentWeapon).CurrentMagazineAmmo <= 0);
        }

        #endregion
    }
}
