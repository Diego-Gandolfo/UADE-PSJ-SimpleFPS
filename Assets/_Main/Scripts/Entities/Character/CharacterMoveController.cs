using UnityEngine;

namespace Assets._Main.Scripts.Entities.Character
{
    public class CharacterMoveController : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _runSpeed = 14f;

        #endregion

        #region Propertys

        public float MoveSpeed => _moveSpeed;
        public float RunSpeed => _runSpeed;

        #endregion

        #region Public Methods

        public void Move(Vector3 direction, float speed)
        {
            transform.position += (direction * (speed * Time.deltaTime));
        }

        #endregion
    }
}
