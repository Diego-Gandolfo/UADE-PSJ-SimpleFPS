using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SimpleFPS.Managers
{
    public class MusicManager : MonoBehaviour
    {
        #region Static

        public static MusicManager Instance { get; private set; }

        #endregion

        #region Serialized Fields

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        [Header("UI")]
        [SerializeField] private Slider _slider;

        [Header("Settings")]
        [SerializeField] private float _fadeInTime;

        #endregion

        #region Private Fields

        private bool _canCount;
        private float _fadeInCounter;
        private float _currentVolume;

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
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            _currentVolume = _slider.value;
            _canCount = true;
            _audioSource.Play();
        }

        private void Update()
        {
            if (_canCount)
            {
                _fadeInCounter += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(0f, _currentVolume, (_fadeInCounter / _fadeInTime));

                if (_fadeInCounter >= _fadeInTime)
                {
                    _canCount = false;
                }
            }
        }

        #endregion

        #region Public Methods

        public void OnSliderValueChange()
        {
            _audioSource.volume = _slider.value;
        }

        public void OnClickBackButton()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
