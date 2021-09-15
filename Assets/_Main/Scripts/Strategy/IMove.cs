using UnityEngine;

namespace SimpleFPS.Movement
{
    public interface IMove
    {
        #region Public Methods

        void DoMove(Vector3 direction, float speed);

        #endregion
    }
}
