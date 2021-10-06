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

        [Header("UI")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;

        [Header("Settings")]
        [SerializeField, Range(0.0f, 0.1f)] private float _initualVolumen = 0.025f;

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
            _slider.value = _initualVolumen * 10;
            _audioSource.Play();
            _canvas.gameObject.SetActive(false);
        }

        #endregion

        #region Public Methods

        public void OnSliderValueChange()
        {
            _audioSource.volume = (_slider.value / 10);
        }

        public void OnClickBackButton()
        {
            OnBackButtonClicked?.Invoke();
            _canvas.gameObject.SetActive(false);
        }

        #endregion
    }
}
