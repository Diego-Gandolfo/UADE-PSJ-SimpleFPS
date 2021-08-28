using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class AnimationController : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private List<GameObject> _gos = new List<GameObject>();

        #endregion

        #region Private Fields

        private Animator _animator;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        #endregion

        #region Public Methods

        public void ChangeWeapon(int index)
        {
            for (int i = 0; i < _gos.Count; i++)
            {
                _gos[i].SetActive(i == index);
            }
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        #endregion
    }
}
/*
anim.SetBool("Aim", true);
anim.Play("Knife Attack 1", 0, 0f);
anim.Play("Knife Attack 2", 0, 0f);
anim.Play("GrenadeThrow", 0, 0.0f);
anim.SetBool("Out Of Ammo Slider", true);
anim.Play("Fire", 0, 0f);
anim.Play("Aim Fire", 0, 0f);
anim.SetTrigger("Inspect");
anim.SetBool("Holster", true);
anim.Play("Reload Out Of Ammo", 0, 0f);
anim.Play("Reload Ammo Left", 0, 0f);
*/