using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Flyweight.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseWeaponStats", menuName = "Flyweight/Stats/BaseWeapon", order = 0)]
    public class BaseWeaponStats : ScriptableObject
    {
        #region Serialize Fields

        [SerializeField] private int _damage;

        #endregion

        #region Propertys

        public int Damage => _damage;

        #endregion
    }
}
