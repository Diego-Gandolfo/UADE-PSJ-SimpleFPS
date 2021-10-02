using SimpleFPS.Command;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Static

        public static EnemyManager Instance { get; private set; }

        #endregion

        #region Private Fields

        private List<ICommand> _commandsToExecute = new List<ICommand>();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            ExecuteCommands();
        }

        #endregion

        #region Private Methods

        private void ExecuteCommands()
        {
            for (int i = _commandsToExecute.Count - 1; i >= 0; i--)
            {
                _commandsToExecute[i].Execute();
                _commandsToExecute.RemoveAt(i);
            }
        }

        #endregion

        #region Public Methods

        public void AddCommand(ICommand command)
        {
            _commandsToExecute.Add(command);
        }

        #endregion
    }
}
