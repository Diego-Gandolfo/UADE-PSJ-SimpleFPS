using SimpleFPS.Strategy.Movement;
using UnityEngine;

namespace Assets._Main.Scripts.Component
{
    public class RotationComponent : MonoBehaviour, IRotate
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
        }

        #endregion
    }
}
