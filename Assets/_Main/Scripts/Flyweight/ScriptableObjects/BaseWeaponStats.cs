using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Flyweight.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseWeaponStats", menuName = "Flyweight/Stats/Weapons/BaseWeapon", order = 0)]
    public class BaseWeaponStats : ScriptableObject
    {
        #region Serialize Fields

        [SerializeField] private float _damage;

        #endregion

        #region Propertys

        public float Damage => _damage;

        #endregion
    }
}
