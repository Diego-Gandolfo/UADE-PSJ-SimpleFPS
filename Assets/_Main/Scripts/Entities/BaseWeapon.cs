using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
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
