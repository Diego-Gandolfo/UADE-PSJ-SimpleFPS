using UnityEngine;

namespace SimpleFPS.Life
{
    [CreateAssetMenu(fileName = "LifeStats", menuName = "Flyweight/Life", order = 0)]
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
