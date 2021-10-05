using SimpleFPS.Command;
using SimpleFPS.Life;
using SimpleFPS.Patrol;
using SimpleFPS.Sounds;
using UnityEngine;

namespace SimpleFPS.Enemy.Scout
{
    public class Scout : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Patrol Settings")]
        [SerializeField] private DetectTarget _visionCone;

        [Header("Sounds")]
        [SerializeField] private AudioSource _mainAudioSource;
        [SerializeField] private FXSounds _sounds;

        [Header("Health")]
        [SerializeField] private ParticleSystem _damageParticles1;
        [SerializeField] private ParticleSystem _damageParticles2;
        [SerializeField] private ParticleSystem _damageParticles3;

        #endregion

        #region Private Fields

        // Componentes
        private PatrolArea _patrolArea;
        private FollowTarget _followTarget;
        private Health _healthComponent;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _patrolArea = GetComponent<PatrolArea>();
            _followTarget = GetComponent<FollowTarget>();
            
            _healthComponent = GetComponent<Health>();
            if (_healthComponent == null) Debug.LogError($"{this.gameObject.name} no tiene asignado un HealthComponent");
            else
            {
                _healthComponent.OnDie += OnDieHandler;
                _healthComponent.OnRecieveDamage += OnRecieveDamageHandler;
            }

            if (_visionCone == null) Debug.LogError($"{this.gameObject.name} no tiene asignado un DetectTarget");
            else _visionCone.OnDetection += OnDetectionHandler;

            _patrolArea.enabled = true;
            _visionCone.gameObject.SetActive(true);
            _followTarget.enabled = false;
        }

        private void Update()
        {
            if ((_patrolArea.enabled && _patrolArea.CurrentSpeed != 0) || _followTarget.enabled)
            {
                if (!_mainAudioSource.isPlaying)
                {
                    _mainAudioSource.PlayOneShot(_sounds.ScoutMoveSound);
                }
            }
        }

        private void OnDestroy()
        {
            if (_healthComponent != null)
            {
                _healthComponent.OnDie -= OnDieHandler;
                _healthComponent.OnRecieveDamage -= OnRecieveDamageHandler;
            }

            if (_visionCone != null) _visionCone.OnDetection -= OnDetectionHandler;
        }

        #endregion

        #region Private Methods

        private void OnDetectionHandler()
        {
            _patrolArea.enabled = false;
            _visionCone.gameObject.SetActive(false);
            _followTarget.enabled = true;
        }

        private void OnDieHandler()
        {
            EnemyManager.Instance.AddCommand(new CmdExplosion(transform.position, transform.rotation));
            Destroy(gameObject);
        }

        private void OnRecieveDamageHandler()
        {
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

        #endregion
    }
}
