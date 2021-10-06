using SimpleFPS.Command;
using SimpleFPS.Life;
using SimpleFPS.Managers;
using SimpleFPS.Patrol;
using SimpleFPS.Projectiles;
using SimpleFPS.Sounds;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Enemy.Boss
{
    public class Boss : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Stats")]
        [SerializeField] private BulletStats _bulletStats;

        [Header("Overlaps Settings")]
        [SerializeField] private LayerMask _characterLayer;
        [SerializeField] private float _detectionRadius;
        [SerializeField] private float _shootingRadius;

        [Header("Shooting")]
        [SerializeField] private Transform _bulletSpawnpoint;
        [SerializeField] private List<float> _shootingCooldowns = new List<float>();
        [SerializeField] private float _damage;

        [Header("Sparks")]
        [SerializeField] private ParticleSystem _sparkParticles;
        [SerializeField] private int _minSparks = 1;
        [SerializeField] private int _maxSparks = 7;

        [Header("Muzzle Flash")]
        [SerializeField] private ParticleSystem _muzzleFlashParticles;
        [SerializeField] private Light _muzzleFlashLight;

        [Header("Sounds")]
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private FXSounds _sounds;

        [Header("Health")]
        [SerializeField] private ParticleSystem _damageParticles1;
        [SerializeField] private ParticleSystem _damageParticles2;
        [SerializeField] private ParticleSystem _damageParticles3;
        [SerializeField] private Light _ligthLife;

        #endregion

        #region Private Fields

        // Componentes
        private GameManager _gameManager;
        private FollowTarget _followTarget;
        private Health _healthComponent;
        private CommandManager _commandManager;
        private Transform _characterTransform;
        private Animator _animator;

        // Flags
        private bool _canShoot;

        // Parameters
        private float _shootingTimer;
        private float _shootingCurrentCooldown;
        private const float BULLET_FORCE = 100f;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _followTarget = GetComponent<FollowTarget>();
            _animator = GetComponent<Animator>();
            _gameManager = GameManager.Instance;

            _healthComponent = GetComponent<Health>();
            if (_healthComponent == null) Debug.LogError($"{this.gameObject.name} no tiene asignado un HealthComponent");
            else
            {
                _healthComponent.OnDie += OnDieHandler;
                _healthComponent.OnRecieveDamage += OnRecieveDamageHandler;
            }

            _followTarget.enabled = false;

            var levelManager = Managers.LevelManager.Instance;
            _characterTransform = levelManager.Character.transform;

            _commandManager = CommandManager.Instance;

            _shootingCurrentCooldown = _shootingCooldowns[0];
            _ligthLife.color = Color.white;
        }

        private void Update()
        {
            if (!_gameManager.IsPaused)
            {
                if (_canShoot)
                {
                    if (_shootingTimer <= 0f)
                    {
                        Shoot();
                        _shootingTimer = _shootingCurrentCooldown;
                    }
                    else
                    {
                        _shootingTimer -= Time.deltaTime;
                    }

                    var xzTargetPosition = new Vector3(_characterTransform.position.x, transform.position.y, _characterTransform.position.z);
                    transform.LookAt(xzTargetPosition);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_followTarget.enabled)
            {
                _animator.SetFloat("VelocityX", _followTarget.Direction.x);
                _animator.SetFloat("VelocityY", _followTarget.Direction.y);
            }
            else
            {
                _animator.SetFloat("VelocityX", 0f);
                _animator.SetFloat("VelocityY", 0f);
            }

            var characterDetected = Physics.OverlapSphere(transform.position, _detectionRadius, _characterLayer);
            _followTarget.enabled = (characterDetected.Length > 0);

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

        private void Shoot()
        {
            RaycastHit hit;

            var direction = (_characterTransform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, _bulletStats.TargetsLayers))
            {
                if (hit.collider.gameObject.layer == 3)
                {
                    _animator.SetTrigger("DoShoot");
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

        private void OnDieHandler()
        {
            _animator.SetTrigger("DoDie");
            Invoke("Explote", 1f);
        }

        private void OnRecieveDamageHandler()
        {
            if (!_damageParticles1.isPlaying && _healthComponent.CurrentLife <= ((_healthComponent.MaxLife / 2) + (_healthComponent.MaxLife / 4)))
            {
                _damageParticles1.Play();
                _shootingCurrentCooldown = _shootingCooldowns[1];
                _ligthLife.color = Color.blue;
            }
            else if (!_damageParticles2.isPlaying && _healthComponent.CurrentLife <= (_healthComponent.MaxLife / 2))
            {
                _damageParticles2.Play();
                _shootingCurrentCooldown = _shootingCooldowns[2];
                _ligthLife.color = Color.yellow;
                _animator.SetFloat("Fase", 0.5f);
            }
            else if (!_damageParticles3.isPlaying && _healthComponent.CurrentLife <= (_healthComponent.MaxLife / 4))
            {
                _damageParticles3.Play();
                _shootingCurrentCooldown = _shootingCooldowns[3];
                _ligthLife.color = Color.red;
                _animator.SetFloat("Fase", 1f);
            }
        }

        private void Explote()
        {
            CommandManager.Instance.AddCommand(new CmdExplosion(transform.position, transform.rotation));
            Destroy(gameObject);
        }

        #endregion
    }
}
