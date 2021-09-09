using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Main.Scripts.Strategy
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
