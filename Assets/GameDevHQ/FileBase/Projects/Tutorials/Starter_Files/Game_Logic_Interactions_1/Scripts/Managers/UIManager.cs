using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _leftTimeSlider;
    [SerializeField] private Image _leftTimeFill;    
    [SerializeField] private Slider _rightTimeSlider;
    [SerializeField] private Image _rightTimeFill;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _agentKillText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GameObject _gameWonPanel;
    [SerializeField] private GameObject _gameLostPanel;

    private bool _isWarningTime;
    private bool _isDangerTime;

    
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Update() {
        _leftTimeSlider.value = _rightTimeSlider.value = GameManager.Instance._timeSliderValue;
    }

    public void WarningTime() {
        _timeText.text = "warning";
        _leftTimeFill.color = Color.yellow;
        _rightTimeFill.color = Color.yellow;
        _isWarningTime = true;
    }

    public void DangerTime() {
        _timeText.text = "danger";
        _leftTimeFill.color = Color.red;
        _rightTimeFill.color = Color.red;
        _isDangerTime = true;
    }

    public void OutOfTime() {
        _timeText.text = "game over";
        
    }
    
    public void UpdateKillCount(int killCount) {
        _agentKillText.text = killCount.ToString();
    }

    public void UpdateScore(int score) {
        _scoreText.text = score.ToString();
    }

    public void UpdateAmmo(int ammoCount) {
            _ammoText.text = ammoCount.ToString();
    }

    public void GameWon() {
        _gameWonPanel.SetActive(true);
    }
    public void GameLost() {
        _gameLostPanel.SetActive(true);
    }
}
