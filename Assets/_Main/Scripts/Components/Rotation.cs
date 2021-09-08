using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Component
{
    public class Rotation : MonoBehaviour, IRotate
    {
        #region Private Fields

        private float _mouseMove;

        #endregion

        #region Public Methods

        public void Rotate(float value)
        {
            _mouseMove += value * Time.deltaTime;
            var angles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(angles.x, _mouseMove, angles.z);
            //transform.eulerAngles = new Vector3(0.0f, _mouseMove, 0.0f);
        }

        #endregion
    }
}
