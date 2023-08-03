using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    private int _score;
    [SerializeField] private Slider _leftTimeSlider;
    [SerializeField] private Image _leftTimeFill;    
    [SerializeField] private Slider _rightTimeSlider;
    [SerializeField] private Image _rightTimeFill;
    [SerializeField] private TextMeshProUGUI _TimeText;
    private float _timeInGame = 0;
    private bool _isWarningTime;
    private bool _isDangerTime;
    [FormerlySerializedAs("_timeAlledInGame")] [SerializeField]
    private float _timeAllowedInGame = 30f;
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Update()
    {
        _timeInGame += Time.deltaTime;
        _leftTimeSlider.value = _rightTimeSlider.value = 1 - (_timeInGame / _timeAllowedInGame);
        if ( !_isWarningTime && _timeInGame >= _timeAllowedInGame / 3f) {
            WarningTime();
        } 
        if ( !_isDangerTime && _timeInGame >= (_timeAllowedInGame * 2f) / 3f) {
            DangerTime();
        }
    }

    public void UpdateScore(int score) {

    }

    public void WarningTime() {
        _leftTimeFill.color = Color.yellow;
        _rightTimeFill.color = Color.yellow;
        _isWarningTime = true;
    }

    public void DangerTime() {
        _leftTimeFill.color = Color.red;
        _rightTimeFill.color = Color.red;
        _isDangerTime = true;
    }

}
