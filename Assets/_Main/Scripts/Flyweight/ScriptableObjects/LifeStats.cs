using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Flyweight.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LifeStats", menuName = "Flyweight/Stats/Life/Life", order = 0)]
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
