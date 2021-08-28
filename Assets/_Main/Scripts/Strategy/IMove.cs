using UnityEngine;

namespace Assets._Main.Scripts.Strategy
{
    public interface IMove
    {
        #region Public Methods

        void Move(Vector3 direction, float speed);

        #endregion
    }
}
