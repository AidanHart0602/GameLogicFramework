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
    private int _gameOverScore;
    private float _timerCount = 300;
    [SerializeField]
    private Text _quotaText;
    [SerializeField]
    private Text _winningText;
    [SerializeField]
    private Text _gameOverScoreText;
    [SerializeField]
    private Image _reticle;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _timerText;
    [SerializeField]
    private Text _aiCount;
    private bool _quotaMet = false;

    private void Awake()
    {
        _uiInstance = this;
    }

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _quotaText.text = "Quota: " + 90;
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
        _gameOverScore = ScoreValue;
    }

    public void QuotaCount(int QuotaNum)
    {
        _quotaText.text = "Quota: " + QuotaNum;
        if(QuotaNum == 0)
        {
            _quotaMet = true;
        }
    }

    public void EndScene()
    {
        _gameOverScoreText.gameObject.SetActive(true);
        _gameOverScoreText.text = "Score: " + _gameOverScore;
        _gameOverText.gameObject.SetActive(true);
        _scoreText.gameObject.SetActive(false);
        _aiCount.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(false);
        _reticle.gameObject.SetActive(false);
        _quotaText.gameObject.SetActive(false);
        if(_quotaMet == true)
        {
            _winningText.gameObject.SetActive(true);
        }

        if(_quotaText == false)
        {
            _winningText.text = "Failed to meet quota.";
            _winningText.gameObject.SetActive(true);
        }
    }
}
