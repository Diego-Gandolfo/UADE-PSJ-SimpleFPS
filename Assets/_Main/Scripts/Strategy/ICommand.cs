using UnityEngine;

namespace SimpleFPS.Command
{
    public interface ICommand
    {
        #region Public Methods

        void Execute();

        #endregion
    }
}
