using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleFPS.Managers.Screens
{
    public class ScreenManager : MonoBehaviour
    {
        #region Unity Methods

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        #endregion

        #region Public Methods

        public void OnClickReplayButton()
        {
            SceneManager.LoadScene("Game");
        }

        public void OnClickMenuButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        #endregion
    }
}
