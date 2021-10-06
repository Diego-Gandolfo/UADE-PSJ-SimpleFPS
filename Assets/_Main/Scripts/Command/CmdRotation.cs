using UnityEngine;

namespace SimpleFPS.Command
{
    public class CmdRotation : ICommand
    {
        #region Private Fields

        private Transform _subjectTranform;
        private Quaternion _rotateTo;
        private float _speed;

        #endregion

        #region Constructor

        public CmdRotation(Transform subjectTranform, Quaternion rotateTo, float speed)
        {
            _subjectTranform = subjectTranform;
            _rotateTo = rotateTo;
            _speed = speed;
        }

        #endregion

        #region Public Methods

        public void Execute()
        {
            if (_subjectTranform != null)
            {
                _subjectTranform.rotation = Quaternion.RotateTowards(_subjectTranform.rotation, _rotateTo, _speed);
            }
        }

        #endregion
    }
}
