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
            Managers.LevelManager.Instance.ExplosionFactory.GetExplosion(_position, _rotation);
        }
    }
}
