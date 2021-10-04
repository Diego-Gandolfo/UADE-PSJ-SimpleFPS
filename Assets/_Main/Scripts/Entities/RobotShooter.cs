using UnityEngine;

namespace SimpleFPS.Enemy
{
    public class RobotShooter : MonoBehaviour
    {
        #region Private Fields
        // Character
        private Transform _character;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _character = Managers.LevelManager.Instance.Character.transform;        
        }

        #endregion
    }
}
