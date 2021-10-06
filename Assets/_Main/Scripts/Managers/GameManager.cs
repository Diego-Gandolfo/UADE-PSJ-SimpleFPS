using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleFPS.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Static

        public static GameManager Instance { get; private set; }

        #endregion

        #region Propertys

        public bool IsPaused { get; private set; }

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

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F12)) Victory();
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F11)) GameOver();
        }

        #endregion

        #region Public Methods

        public void SetIsPaused(bool value)
        {
            IsPaused = value;
        }

        public void GameOver()
        {
            SceneManager.LoadScene("Defeated");
        }

        public void Victory()
        {
            SceneManager.LoadScene("Victory");
        }

        #endregion
    }
}
