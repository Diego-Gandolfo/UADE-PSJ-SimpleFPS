using Assets._Main.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private float _timeToDestroy = 10f;
    [SerializeField] private AudioClip[] _impactSounds;

    #endregion

    #region Private Fields

    private AudioSource _audioSource;
    private float _timer;
    private IWeaponController _weaponController;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _timer = _timeToDestroy;
        _audioSource.clip = _impactSounds[Random.Range(0, _impactSounds.Length)];
        _audioSource.Play();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            //print($"BulletImpact: {_weaponController}");
            _weaponController?.BulletImpactPool.StoreInstance(this);
        }
    }

    #endregion

    #region Public Methods

    public void SetWeaponControlller(IWeaponController weaponController)
    {
        _weaponController = weaponController;
    }

    #endregion
}
