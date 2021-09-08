using UnityEngine;

namespace Assets._Main.Scripts.Strategy
{
    public interface IMove
    {
        #region Public Methods

        void DoMove(Vector3 direction, float speed);

        #endregion
    }
}
