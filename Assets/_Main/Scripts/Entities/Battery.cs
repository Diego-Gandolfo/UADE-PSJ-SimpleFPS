using SimpleFPS.Life;
using SimpleFPS.Sounds;
using UnityEngine;

namespace SimpleFPS.Battery
{
    public class Battery : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private FXSounds _sounds;

        [Header("Smoke")]
        [SerializeField] private ParticleSystem _damageParticles1;
        [SerializeField] private ParticleSystem _damageParticles2;
        [SerializeField] private ParticleSystem _damageParticles3;

        #endregion

        #region Private Fields

        private AudioSource _audioSource;
        private Health _healthComponent;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null) Debug.LogError($"{this.gameObject.name} no tiene un AudioSource");
            else
            {
                _audioSource.clip = _sounds.EnergySound;
                _audioSource.loop = true;
                _audioSource.Play();
            }

            _healthComponent = GetComponent<Health>();
            if (_healthComponent == null) Debug.LogError($"{this.gameObject.name} no tiene un HealthComponent");
            else
            {
                _healthComponent.OnDie += OnDieHandler;
                _healthComponent.OnRecieveDamage += OnRecieveDamageHandler;
            }
        }

        #endregion

        #region Private Methods

        private void OnDieHandler()
        {
            Managers.LevelManager.Instance.IncreaseBatteryDeadCounter();
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
