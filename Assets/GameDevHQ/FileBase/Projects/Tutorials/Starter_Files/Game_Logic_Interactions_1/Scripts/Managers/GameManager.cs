using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private int _killCount = 0;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public float _timeInGame = 0;
    [SerializeField]
    public float _timeAllowedInGame = 300f;
    private bool _isWarningTime;
    private bool _isDangerTime;
    public float _timeSliderValue;
    [SerializeField]
    public int _numberOfKillsToWin = 25;
    [SerializeField]
    public int _numberOfAIRespawn = 0;
    public int _ammo = 100;
    private bool _isGameOver;
    
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        SpawnManager.Instance.Spawn();
    }

    private void Update() {
        _timeInGame += Time.deltaTime; 
        _timeSliderValue = 1 - (_timeInGame / _timeAllowedInGame);
        if ( !_isWarningTime && _timeInGame >=  _timeAllowedInGame * 0.3333f) { 
            UIManager.Instance.WarningTime();
        } 
        if ( !_isDangerTime && _timeInGame >= _timeAllowedInGame * 0.6667f ) { 
            UIManager.Instance.DangerTime();
        }

        if (!_isGameOver)
        {
            // ------ Win ----
            if (_killCount >= _numberOfKillsToWin)
            {
                GameWon();
            }
            // ------ Lose ----

            if (_ammo == 0)
            {
                GameLost();
            }

            if (_timeInGame >= _timeAllowedInGame)
            {
                UIManager.Instance.OutOfTime();
                GameLost();
            }

            if (_numberOfAIRespawn > (_numberOfKillsToWin / 2))
            {
                GameLost();
            }
        }
    }

    private void GameLost()
    {
        _isGameOver = true;
        UIManager.Instance.GameLost();
        Actions.GameOver();
    }

    private void GameWon()
    {
        _isGameOver = false;
        UIManager.Instance.GameWon();
        Actions.GameOver();
    }

    public void AIAgentDeath(int killScore) {
        AudioManager.Instance.PlayAIDeath();
        _score += killScore;
        UIManager.Instance.UpdateScore(_score);
        _killCount++;
        UIManager.Instance.UpdateKillCount(_killCount);
    }

    public void AIRespawned() {
        _numberOfAIRespawn++;
    }

    public void gunFired() {
        _ammo--;
        UIManager.Instance.UpdateAmmo(_ammo);
    }
}
