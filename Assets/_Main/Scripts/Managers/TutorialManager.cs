using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleFPS.Managers.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Animator _animator;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        #endregion

        #region Public Methods

        public void OnClickSkipButton()
        {
            _animator.SetTrigger("GoFadeOut");
            SceneManager.LoadScene("Game");
        }

        public void OnClickContinueButtonObjectives()
        {
            _animator.SetTrigger("GoKeyMap");
        }

        public void OnClickContinueButtonKeyMap()
        {
            _animator.SetTrigger("GoFadeOut");
            SceneManager.LoadScene("Game");
        }

        #endregion
    }
}
