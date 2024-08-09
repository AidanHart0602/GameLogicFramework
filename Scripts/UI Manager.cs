using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager _uiInstance;

    public static UIManager UIinstance
    {
        get
        {
            return _uiInstance;
        }
    }
    private float _timerCount = 300;

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _timerText;
    [SerializeField]
    private Text _aiCount;

    private void Awake()
    {
        _uiInstance = this;
    }

    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    void Update()
    {
        if (_timerCount > 0)
        {
            _timerCount = _timerCount - Time.deltaTime;
        }                     
        int IntTimer = Mathf.RoundToInt(_timerCount);
        _timerText.text = "Time: " + IntTimer;
    }

    public void RemainingBots(int BotNum)
    {
        _aiCount.text = "Robots: " + BotNum;
    }

    public void Score(int ScoreValue)
    {
        _scoreText.text = "Score: " + ScoreValue;
    }
}
