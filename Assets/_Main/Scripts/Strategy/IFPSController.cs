using SimpleFPS.Player;

namespace SimpleFPS.FPS
{
    public interface IFPSController
    {
        #region Public Methods

        void SuscribeEvents(IInputController inputController);

        #endregion
    }
}
