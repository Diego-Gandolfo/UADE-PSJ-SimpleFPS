using SimpleFPS.Projectiles;
using SimpleFPS.Generics.Pool;
using UnityEngine;
using SimpleFPS.Player;
using System.Collections.Generic;
using SimpleFPS.Components;

namespace SimpleFPS.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Static

        public static LevelManager Instance { get; private set; }

        #endregion

        #region Serialize Fields

        [Header("Character")]
        [SerializeField] private FPSInputController _character;

        [Header("Pool Prefabs")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;
        [SerializeField] private Explotion _explotionPrefab;

        [Header("Objectives")]
        [SerializeField] private List<Transform> _objectivesList = new List<Transform>();

        #endregion

        #region Private Fields

        protected Pool<Bullet> _bulletPool;
        protected Pool<BulletImpact> _bulletImpactPool;
        protected Pool<Explotion> _explotionPool;

        #endregion

        #region Propertys

        // Character
        public FPSInputController Character => _character;

        // Pools
        public Pool<Bullet> BulletPool => _bulletPool;
        public Pool<BulletImpact> BulletImpactPool => _bulletImpactPool;
        public Pool<Explotion> ExplotionPool => _explotionPool;

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

            _bulletPool = new Pool<Bullet>(_bulletPrefab);
            _bulletImpactPool = new Pool<BulletImpact>(_bulletImpactPrefab);
            _explotionPool = new Pool<Explotion>(_explotionPrefab);
        }

        private void Start()
        {
        }

        #endregion
    }
}
