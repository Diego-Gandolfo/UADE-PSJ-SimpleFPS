using SimpleFPS.Projectiles;
using UnityEngine;
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

        [Header("Factory Prefabs")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;
        [SerializeField] private Explosion _explotionPrefab;

        #endregion

        #region Private Fields

        private BulletFactory _bulletFactory;
        private BulletImpactFactory _bulletImpactFactory;
        private ExplosionFactory _explosionFactory;

        #endregion

        #region Propertys

        // Character
        public FPSCharacterController Character => _character;

        // Factorys
        public BulletFactory BulletFactory => _bulletFactory;
        public BulletImpactFactory BulletImpactFactory => _bulletImpactFactory;
        public ExplosionFactory ExplosionFactory => _explosionFactory;

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
            _explosionFactory = new ExplosionFactory(_explotionPrefab);
        }

        private void Start()
        {
            GameManager.Instance.SetCharacter(_character);
        }

        #endregion
    }
}
