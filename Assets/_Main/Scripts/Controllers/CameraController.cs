using SimpleFPS.Strategy.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Controllers.Cameras
{
    public class CameraController : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Cameras")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _weaponCamera;

        [Header("Look Up-Down")]
        [SerializeField] private float _lookUp;
        [SerializeField] private float _lookDown;

        [Header("Aim")]
        [SerializeField] private float _defaultFOV;
        [SerializeField] private float _aimFOV;
        [SerializeField] private float _speedFOV;

        #endregion

        #region Private Fields

        private bool _isAiming;

        #endregion

        #region Unity Methods

        private void Update()
        {
            if (_isAiming)
                _weaponCamera.fieldOfView = Mathf.Lerp(_weaponCamera.fieldOfView, _aimFOV, _speedFOV * Time.deltaTime);
            else
                _weaponCamera.fieldOfView = Mathf.Lerp(_weaponCamera.fieldOfView, _defaultFOV, _speedFOV * Time.deltaTime);
        }

        #endregion

        #region Private Fields

        private float _mouseMove;

        #endregion

        #region Private Methods

        private void OnAimOnHandler()
        {
            _isAiming = true;
        }

        private void OnAimOffHandler()
        {
            _isAiming = false;
        }

        #endregion

        #region Public Methods

        public void LookUpDown(float value)
        {
            _mouseMove -= value * Time.deltaTime;
            _mouseMove = Mathf.Clamp(_mouseMove, _lookUp, _lookDown);
            var angles = _mainCamera.transform.eulerAngles;
            _mainCamera.transform.eulerAngles = new Vector3(_mouseMove, angles.y, angles.z);
        }

        public void SuscribeEvents(IInputController inputController)
        {
            inputController.OnAimOn += OnAimOnHandler;
            inputController.OnAimOff += OnAimOffHandler;
        }
        #endregion
    }
}
