using SimpleFPS.FPS;
using SimpleFPS.Player;
using UnityEngine;

namespace SimpleFPS.Cameras
{
    public class FPSCameraController : MonoBehaviour, IFPSController
    {
        #region Serialize Fields

        [Header("Cameras")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _weaponCamera;

        [Header("Look Up-Down")]
        [SerializeField] private float _lookUp = -80f;
        [SerializeField] private float _lookDown = 60f;

        [Header("Aim")]
        [SerializeField] private float _defaultFOV = 40f;
        [SerializeField] private float _aimFOV = 15f;
        [SerializeField] private float _speedFOV = 15f;

        #endregion

        #region Private Fields

        private bool _isAiming;

        #endregion

        #region Unity Methods

        private void Update()
        {
            print(_isAiming);
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

        public void SuscribeEvents(FPSCharacterController characterController)
        {
            characterController.OnAimOn += OnAimOnHandler;
            characterController.OnAimOff += OnAimOffHandler;
        }
        #endregion
    }
}
