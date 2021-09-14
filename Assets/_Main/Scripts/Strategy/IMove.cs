using UnityEngine;

namespace SimpleFPS.Strategy.Movement
{
    public interface IMove
    {
        #region Public Methods

        void DoMove(Vector3 direction, float speed);

        #endregion
    }
}
