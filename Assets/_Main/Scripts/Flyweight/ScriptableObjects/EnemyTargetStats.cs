using UnityEngine;

namespace SimpleFPS.Enemy
{
    [CreateAssetMenu(fileName = "EnemyTargetStats", menuName = "Flyweight/Enemy/Target", order = 0)]
    public class EnemyTargetStats : ScriptableObject
    {
        #region Serialize Fields

        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _retreatDistance;
        [SerializeField] private float _shootDistance;

        #endregion

        #region Propertys

        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
        public float RetreatDistance => _retreatDistance;
        public float ShootDistance => _shootDistance;

        #endregion
    }
}
