using SimpleFPS.Flyweight.Weapons;
using SimpleFPS.Strategy.Weapons;
using UnityEngine;

namespace SimpleFPS.Entities.Weapons
{
    public class BaseWeapon : MonoBehaviour, IWeapon
    {
        #region Serialize Fields

        [SerializeField] protected BaseWeaponStats _baseWeaponStats;

        #endregion

        #region Propertys

        public float Damage => _baseWeaponStats.Damage;

        #endregion

        #region Public Methods

        public virtual void Attack(IWeaponController weaponController) { }

        #endregion
    }
}
