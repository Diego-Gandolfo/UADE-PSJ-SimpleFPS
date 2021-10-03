using UnityEngine;

namespace SimpleFPS.Command
{
    public class CmdMovement : ICommand
    {
        #region Private Fields

        private GameObject _subject;
        private Vector3 _direction;
        private float _speed;

        #endregion

        #region Constructor

        public CmdMovement(GameObject subject, Vector3 direction, float speed)
        {
            _subject = subject;
            _direction = direction;
            _speed = speed;
        }

        #endregion

        #region Public Methods

        public void Execute()
        {
            if (_subject != null)
            {
                var rigidbody = _subject.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
                    rigidbody.AddForce(_direction * _speed, ForceMode.VelocityChange);
                }
                else
                {
                    _subject.transform.position += (_direction * _speed * Time.deltaTime);
                }
            }
        }

        #endregion
    }
}
