namespace SimpleFPS.Life
{
    public interface IAlive
    {
        #region Propertys

        LifeStats LifeStats { get; }
        float MaxLife { get; }
        float CurrentLife { get; }

        #endregion
    }
}
