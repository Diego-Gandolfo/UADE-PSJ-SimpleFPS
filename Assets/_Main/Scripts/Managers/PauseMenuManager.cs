using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SimpleFPS.Managers.PauseMenu
{
    public class PauseMenuManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private KeyCode _pauseKey;
        [SerializeField] private GameObject _pauseMenu;

        #endregion

        #region Private Fields

        private GraphicRaycaster _graphicRaycaster;
        private MusicManager _musicManager;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _musicManager = MusicManager.Instance;
            _musicManager.OnBackButtonClicked += OnBackButtonClickedHandler;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_pauseKey))
            {
                if (GameManager.Instance.IsPaused)
                {
                    Unpause();
                }
                else
                {
                    Pause();
                }
            }
        }

        #endregion

        #region Private Methods

        private void OnBackButtonClickedHandler()
        {
            _graphicRaycaster.enabled = true;
        }

        private void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.SetIsPaused(true);
            _pauseMenu.SetActive(true);
        }

        private void Unpause()
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.SetIsPaused(false);
            _pauseMenu.SetActive(false);
            _musicManager.gameObject.SetActive(false);
        }

        #endregion

        #region Public Methods

        public void OnClickMenuButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnClickMusicButton()
        {
            _graphicRaycaster.enabled = false;
            _musicManager.Canvas.gameObject.SetActive(true);
        }

        public void OnClickBackButton()
        {
            Unpause();
        }

        #endregion
    }
}
