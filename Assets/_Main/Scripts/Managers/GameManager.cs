using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
        }

        #endregion

        #region Public Methods

        public void SetIsPaused(bool value)
        {
            IsPaused = value;
        }

        public void GameOver()
        {
            print("GameOver");
        }

        public void Victory()
        {
            print("Victory");
        }

        #endregion
    }
}
