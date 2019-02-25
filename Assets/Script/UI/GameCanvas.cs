using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCanvas : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    [Tooltip("Reference to the text where the score will be displayed")]
    private Text scoreText;
    [SerializeField]
    [Tooltip("Reference to the text where the time will be displayed")]
    private Text timeText;

    #region API
    /// <summary>
    /// Initialize this Canvas
    /// </summary>
    /// <param name="_lvlMng"></param>
    public void Init(LevelManager _lvlMng)
    {
        scoreText.text = "Punteggio: 0";
        timeText.text = "Tempo: 30";

        _lvlMng.GetScoreMng().ScoreUpdate += HandleScoreUpdate;
        _lvlMng.GetTimerMng().TimeUpdate += HandleTimeUpdate;
        _lvlMng.OnGameStart += HandleOnGameStart;

        this.gameObject.SetActive(false);
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Update the score text on score update
    /// </summary>
    /// <param name="_score"></param>
    private void HandleScoreUpdate(int _score)
    {
        scoreText.text = "Punteggio: " + _score;
    }

    /// <summary>
    /// Update the time text on time update
    /// </summary>
    /// <param name="_timer"></param>
    private void HandleTimeUpdate(float _timer)
    {
        timeText.text = "Tempo: " + (int)_timer;
    }

    /// <summary>
    /// Reset texts on every game start
    /// </summary>
    private void HandleOnGameStart()
    {
        scoreText.text = "Punteggio: 0";
        timeText.text = "Tempo: 30";
    }
    #endregion
}
