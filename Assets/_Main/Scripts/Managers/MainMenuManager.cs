using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SimpleFPS.Managers.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Private Fields

        private GraphicRaycaster _graphicRaycaster;
        private MusicManager _musicManager;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Start()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _musicManager = MusicManager.Instance;
            _musicManager.OnBackButtonClicked += OnBackButtonClickedHandler;
        }

        #endregion

        #region Private Methods

        private void OnBackButtonClickedHandler()
        {
            _graphicRaycaster.enabled = true;
        }

        #endregion

        #region Public Methods

        public void OnClickPlayButton()
        {
            SceneManager.LoadScene("Tutorial");
        }

        public void OnClickMusicButton()
        {
            _graphicRaycaster.enabled = false;
            _musicManager.Canvas.gameObject.SetActive(true);
        }

        public void OnClickExitButton()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }

        #endregion
    }
}
