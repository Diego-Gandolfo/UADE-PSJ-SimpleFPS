using System;
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
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;

        [Header("Settings")]
        [SerializeField] private float _fadeInTime;

        #endregion

        #region Private Fields

        private bool _canCount;
        private float _fadeInCounter;
        private float _currentVolume;

        #endregion

        #region Propertys

        public Canvas Canvas => _canvas;

        #endregion

        #region Events

        public event Action OnBackButtonClicked;

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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Start()
        {
            _currentVolume = _slider.value;
            _canCount = true;
            _audioSource.Play();
            _canvas.gameObject.SetActive(false);
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
            OnBackButtonClicked?.Invoke();
            _canvas.gameObject.SetActive(false);
        }

        #endregion
    }
}
