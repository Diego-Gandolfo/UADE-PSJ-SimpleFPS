using UnityEngine;

namespace SimpleFPS.Movement
{
    public class RotationComponent : MonoBehaviour, IRotate
    {
        #region Private Fields

        private float _rotation;

        #endregion

        #region Public Methods

        public void Rotate(float value)
        {
            _rotation += value * Time.deltaTime;
            var angles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(angles.x, _rotation, angles.z);
        }

        #endregion
    }
}
