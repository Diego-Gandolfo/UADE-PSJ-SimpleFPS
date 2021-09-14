namespace SimpleFPS.Strategy.Pool
{
    interface IPool<T>
    {
        #region Propertys

        bool IsEmpty { get; }

        #endregion

        #region Public Methods

        T CreateInstance();
        void StoreInstance(T instance);
        T GetInstance();

        #endregion
    }
}
