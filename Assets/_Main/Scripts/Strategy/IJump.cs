namespace SimpleFPS.Movement
{
    public interface IJump
    {
        #region Public Methods

        void DoJump();
        bool CheckIsGrounded();

        #endregion
    }
}
