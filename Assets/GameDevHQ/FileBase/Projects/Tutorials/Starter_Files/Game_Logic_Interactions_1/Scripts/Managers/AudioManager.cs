using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _weaponFireSource;
    [SerializeField]
    private AudioSource _backgroundSource;
    [SerializeField]
    private AudioSource _deathSource;
    [SerializeField]
    private AudioSource _shotBarrierSource;
    [SerializeField]
    private AudioSource _aiCompletedSource;
    [SerializeField]
    private AudioSource _spaceAmbienceSource;
    

    
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    
    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        _spaceAmbienceSource.Play();
        _backgroundSource.Play();
    }

    private void OnEnable() {
        Actions.GameOver += GameOver;
    }

    private void GameOver()
    {
        _spaceAmbienceSource.Stop();
        _backgroundSource.Stop();
    }

    public void PlayAIDeath() {
        _deathSource.Play();
    }

    public void PlayShotBarrier() {
        _shotBarrierSource.Play();
    }

    public void PlayAIComplete() {
        _aiCompletedSource.Play();
    }

    private void OnDisable() {
        Actions.GameOver -= GameOver;
    }
}
