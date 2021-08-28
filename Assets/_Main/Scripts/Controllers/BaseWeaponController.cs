using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using Assets._Main.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class BaseWeaponController : MonoBehaviour, IWeapon
    {
        #region Serialize Fields

        [SerializeField] protected BaseWeaponStats _baseWeaponStats;

        #endregion
        public float Damage => _baseWeaponStats.Damage;

        public virtual void Attack() { }
    }
}
