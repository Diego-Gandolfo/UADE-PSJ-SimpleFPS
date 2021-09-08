using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Flyweight.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LifeGunStats", menuName = "Flyweight/Stats/Life", order = 1)]
    public class LifeStats : ScriptableObject
    {
        #region Serialize Fields

        [SerializeField] private float _maxLife;

        #endregion

        #region Propertys

        public float MaxLife => _maxLife;

        #endregion
    }
}
