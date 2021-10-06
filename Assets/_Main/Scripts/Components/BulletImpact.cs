using SimpleFPS.Managers;
using UnityEngine;


namespace SimpleFPS.Projectiles
{
    public class BulletImpact : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDestroy = 10f;
        [SerializeField] private AudioClip[] _impactSounds;

        #endregion

        #region Private Fields

        private GameManager _gameManager;
        private AudioSource _audioSource;
        private float _timer;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _timer = _timeToDestroy;
            _audioSource.clip = _impactSounds[Random.Range(0, _impactSounds.Length)];
            _audioSource.Play();
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (!_gameManager.IsPaused) 
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0f)
                {
                    Managers.LevelManager.Instance.BulletImpactFactory.StoreBulletImpact(this);
                }
            }
        }

        #endregion
    }
}
