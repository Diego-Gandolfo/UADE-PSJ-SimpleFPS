using SimpleFPS.Projectiles;
using SimpleFPS.Generics.Pool;
using UnityEngine;
using SimpleFPS.Player;
using System.Collections.Generic;
using SimpleFPS.Components;
using SimpleFPS.FPS;

namespace SimpleFPS.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Static

        public static LevelManager Instance { get; private set; }

        #endregion

        #region Serialize Fields

        [Header("Character")]
        [SerializeField] private FPSCharacterController _character;

        [Header("Pool Prefabs")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;
        [SerializeField] private Explosion _explotionPrefab;

        #endregion

        #region Private Fields

        protected Pool<Bullet> _bulletPool;
        protected Pool<BulletImpact> _bulletImpactPool;
        protected Pool<Explosion> _explotionPool;

        #endregion

        #region Propertys

        // Character
        public FPSCharacterController Character => _character;

        // Pools
        public Pool<Bullet> BulletPool => _bulletPool;
        public Pool<BulletImpact> BulletImpactPool => _bulletImpactPool;
        public Pool<Explosion> ExplotionPool => _explotionPool;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _bulletPool = new Pool<Bullet>(_bulletPrefab);
            _bulletImpactPool = new Pool<BulletImpact>(_bulletImpactPrefab);
            _explotionPool = new Pool<Explosion>(_explotionPrefab);
        }

        #endregion
    }
}
