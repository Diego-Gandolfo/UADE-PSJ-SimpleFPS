using SimpleFPS.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Command
{
    public class CommandManager : MonoBehaviour
    {
        #region Static

        public static CommandManager Instance { get; private set; }

        #endregion

        #region Private Fields

        private GameManager _gameManager;
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

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (!_gameManager.IsPaused)
            {
                ExecuteCommands();
            }
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
