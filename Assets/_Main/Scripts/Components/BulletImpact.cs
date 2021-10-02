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

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                Managers.LevelManager.Instance.BulletImpactPool.StoreInstance(this);
            }
        }

        #endregion
    }
}
