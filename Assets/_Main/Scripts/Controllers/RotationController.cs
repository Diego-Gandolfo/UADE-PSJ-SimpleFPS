using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class RotationController : MonoBehaviour, IRotate
    {
        #region Private Fields

        private float _mouseMove;

        #endregion

        #region Public Methods

        public void Rotate(float value)
        {
            _mouseMove += value * Time.deltaTime;
            transform.eulerAngles = new Vector3(0.0f, _mouseMove, 0.0f);
        }

        #endregion
    }
}