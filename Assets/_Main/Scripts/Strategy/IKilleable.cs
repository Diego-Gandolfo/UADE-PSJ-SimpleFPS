using System;

namespace SimpleFPS.Life
{
    public interface IKilleable
    {
        #region Events

        event Action OnDie;

        #endregion
    }
}
