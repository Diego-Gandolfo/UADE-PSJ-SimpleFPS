using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Component
{
    public class BaseWeapon : MonoBehaviour, IWeapon
    {
        #region Serialize Fields

        [SerializeField] protected BaseWeaponStats _baseWeaponStats;

        #endregion
        public float Damage => _baseWeaponStats.Damage;

        public virtual void Attack() { }
    }
}
