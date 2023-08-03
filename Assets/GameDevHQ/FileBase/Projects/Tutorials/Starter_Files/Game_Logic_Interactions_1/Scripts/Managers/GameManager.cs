using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score;


    public void AddScore(int killScore)
    {
        _score += killScore;
        Singleton.Instance.UIManager.UpdateScore(_score);
    }
}
