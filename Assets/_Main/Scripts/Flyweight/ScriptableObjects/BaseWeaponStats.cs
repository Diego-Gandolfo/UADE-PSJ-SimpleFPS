using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Weapons
{
    [CreateAssetMenu(fileName = "BaseWeaponStats", menuName = "Flyweight/Weapons/BaseWeapon", order = 0)]
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
