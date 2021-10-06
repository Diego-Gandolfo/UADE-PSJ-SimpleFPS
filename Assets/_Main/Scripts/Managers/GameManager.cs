using SimpleFPS.FPS;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleFPS.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Static

        public static GameManager Instance { get; private set; }

        #endregion

        #region Private Fields

        private FPSCharacterController _character;
        private int _batteryDeadCounter;

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

        #region Private Methods

        private void GameOver()
        {
            SceneManager.LoadScene("Defeated");
        }

        private void Victory()
        {
            SceneManager.LoadScene("Victory");
        }

        private void OnDieHandler()
        {
            Invoke("GameOver", 1.5f);
        }

        #endregion

        #region Public Methods

        public void SetIsPaused(bool value)
        {
            IsPaused = value;
        }

        public void SetCharacter(FPSCharacterController character)
        {
            _character = character;
            _character.OnDie += OnDieHandler;
        }

        public void IncreaseBatteryDeadCounter()
        {
            _batteryDeadCounter++;

            if (_batteryDeadCounter >= 5)
            {
                Invoke("Victory", 1.5f);
            }
        }

        #endregion
    }
}
