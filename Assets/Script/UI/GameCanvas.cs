using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCanvas : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timeText;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        scoreText.text = "Punteggio: 0";
        timeText.text = "Tempo: 30";

        _lvlMng.GetScoreMng().ScoreUpdate += HandleScoreUpdate;
        _lvlMng.GetTimerMng().TimeUpdate += HandleTimeUpdate;

        this.gameObject.SetActive(false);
    }
    #endregion

    #region Handlers
    private void HandleScoreUpdate(int _score)
    {
        scoreText.text = "Punteggio: " + _score;
    }

    private void HandleTimeUpdate(float _timer)
    {
        timeText.text = "Tempo: " + (int)_timer;
    }
    #endregion
}
