using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleFPS.Managers.Screens
{
    public class ScreenManager : MonoBehaviour
    {
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
