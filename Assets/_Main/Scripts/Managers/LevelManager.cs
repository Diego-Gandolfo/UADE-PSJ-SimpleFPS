using SimpleFPS.Projectiles;
using SimpleFPS.Generics.Pool;
using UnityEngine;

namespace SimpleFPS.LevelManagers
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        #region Static

        public static LevelManager Instance { get; private set; }

        #endregion

        #region Serialize Fields

        [Header("Pool Prefabs")]
        [SerializeField] private Bullet _playerBulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;

        #endregion

        #region Private Fields

        protected Pool<Bullet> _playerBulletPool;
        protected Pool<BulletImpact> _bulletImpactPool;

        #endregion

        #region Propertys

        // Pools
        public Pool<Bullet> PlayerBulletPool => _playerBulletPool;
        public Pool<BulletImpact> BulletImpactPool => _bulletImpactPool;

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
        }

        private void Start()
        {
            _playerBulletPool = new Pool<Bullet>(_playerBulletPrefab);
            _bulletImpactPool = new Pool<BulletImpact>(_bulletImpactPrefab);
        }

        #endregion
    }
}
