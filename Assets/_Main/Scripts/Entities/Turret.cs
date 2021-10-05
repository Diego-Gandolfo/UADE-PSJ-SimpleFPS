using SimpleFPS.Factory;
using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Enemy
{
    public class Turret : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private BulletStats _bulletStats;
        [SerializeField] private LayerMask _characterLayer;
        [SerializeField] private float _detectionRadius;
        [SerializeField] private float _shootingRadius;
        [SerializeField] private Transform _gameObjectToRotate;
        [SerializeField] private Transform _bulletSpawnpoint;
        [SerializeField] private float _shootingCooldown;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _damage;

        [Header("Sparks")]
        [SerializeField] private ParticleSystem _sparkParticles;
        [SerializeField] private int _minSparks = 1;
        [SerializeField] private int _maxSparks = 7;

        [Header("Muzzle Flash")]
        [SerializeField] private ParticleSystem _muzzleFlashParticles;
        [SerializeField] protected Light _muzzleFlashLight;

        #endregion

        #region Private Fields

        // Components
        private Transform _character;

        // Factorys
        private BulletFactory _bulletFactory;

        // Flags
        private bool _canRotate;
        private bool _canShoot;

        // Parameters
        private Vector3 _direction;
        private float _shootingTimer;
        private const float BULLET_FORCE = 50f;

        #endregion

        #region Unity Methods

        private void Start()
        {
            var levelManager = Managers.LevelManager.Instance;
            _character = levelManager.Character.transform;
            _bulletFactory = levelManager.BulletFactory;
            _shootingTimer = _shootingCooldown;
        }

        private void Update()
        {
            if (_canRotate)
            {
                var xzCharacterPosition = new Vector3(_character.position.x, _gameObjectToRotate.position.y, _character.position.z);
                _direction = xzCharacterPosition - _gameObjectToRotate.position;
                _direction.Normalize();

                var lookRotation = Quaternion.LookRotation(_direction);
                _gameObjectToRotate.rotation = Quaternion.RotateTowards(_gameObjectToRotate.rotation, lookRotation, _rotationSpeed);
            }

            if(_canShoot)
            {
                if (_shootingTimer <= 0f)
                {
                    print($"POW!");
                    _bulletFactory.GetBullet(_bulletStats, _bulletSpawnpoint.position, _bulletSpawnpoint.rotation, _damage, BULLET_FORCE);
                    _muzzleFlashLight.enabled = true;
                    Invoke("TurnMuzzleFlashLightOff", 0.02f);
                    PlayMuzzleFlashParticles();
                    PlaySparkParticles();
                    _shootingTimer = _shootingCooldown;
                }
                else
                {
                    _shootingTimer -= Time.deltaTime;
                }
            }
        }

        private void FixedUpdate()
        {
            var characterDetected = Physics.OverlapSphere(transform.position, _detectionRadius, _characterLayer);
            _canRotate = (characterDetected.Length > 0);

            var characterInRange = Physics.OverlapSphere(transform.position, _shootingRadius, _characterLayer);
            _canShoot = (characterInRange.Length > 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.5f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, _detectionRadius);

            Gizmos.color = new Color(0.5f, 0f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, _shootingRadius);
        }

        #endregion

        #region Private Methods

        private void TurnMuzzleFlashLightOff()
        {
            _muzzleFlashLight.enabled = false;
        }

        private void PlayMuzzleFlashParticles()
        {
            _muzzleFlashParticles.Emit(3);
        }

        private void PlaySparkParticles()
        {
            _sparkParticles.Emit(Random.Range(_minSparks, _maxSparks));
        }

        #endregion
    }
}
