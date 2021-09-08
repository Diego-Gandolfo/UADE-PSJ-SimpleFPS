using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Main.Scripts.Strategy
{
    public interface IJump
    {
        #region Public Methods

        void DoJump();
        bool CheckIsGrounded();

        #endregion
    }
}
