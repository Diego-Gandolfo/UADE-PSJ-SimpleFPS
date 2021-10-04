using UnityEngine;

namespace SimpleFPS.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        #region Serialize Fields

        [SerializeField] protected BaseWeaponStats _baseWeaponStats;

        #endregion

        #region Propertys

        public float Damage => _baseWeaponStats.Damage;

        #endregion

        #region Public Methods

        public virtual void Attack() { }

        #endregion
    }
}
