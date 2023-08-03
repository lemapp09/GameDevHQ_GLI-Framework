using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start()
    {
        SpawnManager.Instance.Spawn();
    }

    public void AddScore(int killScore)
    {
        _score += killScore;
        UIManager.Instance.UpdateScore(_score);
    }
}
