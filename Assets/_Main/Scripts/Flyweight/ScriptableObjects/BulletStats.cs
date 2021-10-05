using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Projectiles
{
    [CreateAssetMenu(fileName = "BulletStats", menuName = "Flyweight/Projectiles/Bullet", order = 0)]
    public class BulletStats : ScriptableObject
    {
        #region Serialize Fields

        [SerializeField] private LayerMask _targetsLayers;

        #endregion

        #region Propertys

        public LayerMask TargetsLayers => _targetsLayers;

        #endregion
    }
}
