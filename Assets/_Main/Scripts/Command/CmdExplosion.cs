using SimpleFPS.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Command
{
    public class CmdExplosion : ICommand
    {
        private Vector3 _position;
        private Quaternion _rotation;

        public CmdExplosion(Vector3 position, Quaternion rotation)
        {
            _position = position;
            _rotation = rotation;
        }

        public void Execute()
        {
            var explotion = Managers.LevelManager.Instance.ExplotionPool.GetInstance();
            explotion.transform.position = _position;
            explotion.transform.rotation = _rotation;
        }
    }
}
