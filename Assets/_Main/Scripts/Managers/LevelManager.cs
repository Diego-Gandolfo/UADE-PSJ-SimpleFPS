using SimpleFPS.Projectiles;
using SimpleFPS.Generics.Pool;
using UnityEngine;
using SimpleFPS.Player;
using System.Collections.Generic;

namespace SimpleFPS.LevelManagers
{
    public class LevelManager : MonoBehaviour
    {
        #region Static

        public static LevelManager Instance { get; private set; }

        #endregion

        #region Serialize Fields

        [Header("Character")]
        [SerializeField] private InputController _character;

        [Header("Pool Prefabs")]
        [SerializeField] private Bullet _playerBulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;

        [Header("Objectives")]
        [SerializeField] private List<Transform> _objectivesList = new List<Transform>();

        #endregion

        #region Private Fields

        protected Pool<Bullet> _playerBulletPool;
        protected Pool<BulletImpact> _bulletImpactPool;

        #endregion

        #region Propertys

        // Character
        public InputController Character => _character;

        // Pools
        public Pool<Bullet> PlayerBulletPool => _playerBulletPool;
        public Pool<BulletImpact> BulletImpactPool => _bulletImpactPool;

        // Objectives
        public List<Transform> ObjectivesList => _objectivesList;

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

            _playerBulletPool = new Pool<Bullet>(_playerBulletPrefab);
            _bulletImpactPool = new Pool<BulletImpact>(_bulletImpactPrefab);
        }

        private void Start()
        {
        }

        #endregion
    }
}
