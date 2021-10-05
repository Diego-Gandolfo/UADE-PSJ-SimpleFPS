using SimpleFPS.Projectiles;
using SimpleFPS.Generics.Pool;
using UnityEngine;
using SimpleFPS.Player;
using System.Collections.Generic;
using SimpleFPS.Components;
using SimpleFPS.FPS;
using SimpleFPS.Factory;

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

        private BulletFactory _bulletFactory;
        private BulletImpactFactory _bulletImpactFactory;

        private Pool<Explosion> _explotionPool;

        #endregion

        #region Propertys

        // Character
        public FPSCharacterController Character => _character;

        // Factorys
        public BulletFactory BulletFactory => _bulletFactory;
        public BulletImpactFactory BulletImpactFactory => _bulletImpactFactory;

        // Pools
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

            _bulletFactory = new BulletFactory(_bulletPrefab);
            _bulletImpactFactory = new BulletImpactFactory(_bulletImpactPrefab);

            _explotionPool = new Pool<Explosion>(_explotionPrefab);
        }

        #endregion
    }
}
