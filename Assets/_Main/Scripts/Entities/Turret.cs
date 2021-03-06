using SimpleFPS.Command;
using SimpleFPS.Factory;
using SimpleFPS.Life;
using SimpleFPS.Managers;
using SimpleFPS.Projectiles;
using SimpleFPS.Sounds;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Enemy.Turret
{
    public class Turret : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Stats")]
        [SerializeField] private BulletStats _bulletStats;

        [Header("Sounds")]
        [SerializeField] private AudioSource _mainAudioSource;
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private FXSounds _sounds;

        [Header("Overlaps Settings")]
        [SerializeField] private LayerMask _characterLayer;
        [SerializeField] private float _detectionRadius;
        [SerializeField] private float _shootingRadius;

        [Header("Rotation")]
        [SerializeField] private Transform _gameObjectToRotate;
        [SerializeField] private float _rotationSpeed;

        [Header("Shooting")]
        [SerializeField] private Transform _bulletSpawnpoint;
        [SerializeField] private float _shootingCooldown;
        [SerializeField] private float _damage;

        [Header("Sparks")]
        [SerializeField] private ParticleSystem _sparkParticles;
        [SerializeField] private int _minSparks = 1;
        [SerializeField] private int _maxSparks = 7;

        [Header("Muzzle Flash")]
        [SerializeField] private ParticleSystem _muzzleFlashParticles;
        [SerializeField] private Light _muzzleFlashLight;

        [Header("Destroy")]
        [SerializeField] private GameObject _destroyOnDie;
        [SerializeField] private List<Collider> _colliders;

        [Header("Smoke")]
        [SerializeField] private ParticleSystem _damageParticles1;
        [SerializeField] private ParticleSystem _damageParticles2;
        [SerializeField] private ParticleSystem _damageParticles3;

        #endregion

        #region Private Fields

        // Components
        private GameManager _gameManager;
        private Transform _characterTransform;
        private Health _healthComponent;
        private CommandManager _commandManager;

        // Flags
        private bool _canRotate;
        private bool _canShoot;

        // Parameters
        private Vector3 _direction;
        private float _shootingTimer;
        private const float BULLET_FORCE = 100f;

        #endregion

        #region Unity Methods

        private void Start()
        {
            var levelManager = Managers.LevelManager.Instance;
            _characterTransform = levelManager.Character.transform;
            _gameManager = GameManager.Instance;

            _commandManager = CommandManager.Instance;

            _healthComponent = GetComponent<Health>();
            if (_healthComponent == null) Debug.LogError($"{this.gameObject.name} no tiene asignado un HealthComponent");
            else
            {
                _healthComponent.OnDie += OnDieHandler;
                _healthComponent.OnRecieveDamage += OnRecieveDamageHandler;
            }

            _shootingTimer = _shootingCooldown;
        }

        private void Update()
        {
            if (!_gameManager.IsPaused) 
            {
                SetDirection();

                var lookRotation = Quaternion.LookRotation(_direction);

                if (_canRotate)
                {
                    RotateTo(lookRotation);
                }

                if (_canShoot)
                {
                    if (_shootingTimer <= 0f)
                    {
                        if (Quaternion.Angle(_gameObjectToRotate.rotation, lookRotation) <= 30)
                        {
                            Shoot();
                            _shootingTimer = _shootingCooldown;
                        }
                    }
                    else
                    {
                        _shootingTimer -= Time.deltaTime;
                    }
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
            //Gizmos.color = new Color(0f, 0.5f, 0f, 0.25f);
            //Gizmos.DrawSphere(transform.position, _detectionRadius);

            Gizmos.color = new Color(0.5f, 0f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, _shootingRadius);
        }

        #endregion

        #region Private Methods

        private void SetDirection()
        {
            var xzCharacterPosition = new Vector3(_characterTransform.position.x, _gameObjectToRotate.position.y, _characterTransform.position.z);
            _direction = xzCharacterPosition - _gameObjectToRotate.position;
            _direction.Normalize();
        }

        private void RotateTo(Quaternion rotateTo)
        {
            _commandManager.AddCommand(new CmdRotation(_gameObjectToRotate, rotateTo, _rotationSpeed));

            if (!_mainAudioSource.isPlaying && Quaternion.Angle(_gameObjectToRotate.rotation, rotateTo) >= 1)
            {
                _mainAudioSource.PlayOneShot(_sounds.TurretRotationRound);
            }
        }

        private void Shoot()
        {
            RaycastHit hit;

            if (Physics.Raycast(_bulletSpawnpoint.position, _bulletSpawnpoint.forward, out hit, Mathf.Infinity, _bulletStats.TargetsLayers))
            {
                if (hit.collider.gameObject.layer == 3)
                {
                    _shootAudioSource.PlayOneShot(_sounds.ShootSound);
                    _commandManager.AddCommand(new CmdShoot(_bulletSpawnpoint, _bulletStats, _damage, BULLET_FORCE));
                    _muzzleFlashLight.enabled = true;
                    Invoke("TurnMuzzleFlashLightOff", 0.02f);
                    PlayMuzzleFlashParticles();
                    PlaySparkParticles();
                }
            }
        }

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

        private void OnRecieveDamageHandler()
        {
            _mainAudioSource.PlayOneShot(_sounds.HitSound);

            if (!_damageParticles1.isPlaying && _healthComponent.CurrentLife <= ((_healthComponent.MaxLife / 2) + (_healthComponent.MaxLife / 4)))
            {
                _damageParticles1.Play();
            }
            else if (!_damageParticles2.isPlaying && _healthComponent.CurrentLife <= (_healthComponent.MaxLife / 2))
            {
                _damageParticles2.Play();
            }
            else if (!_damageParticles3.isPlaying && _healthComponent.CurrentLife <= (_healthComponent.MaxLife / 4))
            {
                _damageParticles3.Play();
            }
        }

        private void OnDieHandler()
        {
            _commandManager.AddCommand(new CmdExplosion(transform.position, transform.rotation));

            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }

            Destroy(_destroyOnDie);

            _healthComponent.OnRecieveDamage -= OnRecieveDamageHandler;
            _healthComponent.OnDie -= OnDieHandler;

            this.enabled = false;
        }

        #endregion
    }
}
