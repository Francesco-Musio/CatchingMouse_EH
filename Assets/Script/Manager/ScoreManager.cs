using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    #region Delegates
    public delegate void ScoreEvent(int _score, int _quads);
    public ScoreEvent Score;

    public delegate void ScoreUpdateEvent(int _score);
    public ScoreUpdateEvent ScoreUpdate;
    #endregion

    [Header("Score Manager Options")]
    [SerializeField]
    private int score;
    [SerializeField]
    private int quads;

    private void Setup()
    {
        score = 0;
    }

    #region API
    public void Init(LevelManager _lvlMng)
    {
        _lvlMng.OnGameStart += HandleOnGameStart;
        Score += HandleScore;
    }
    #endregion

    #region Handlers
    private void HandleOnGameStart()
    {
        Setup();
    }

    private void HandleScore(int _score, int _quads)
    {
        score += _score;
        quads += _quads;
        ScoreUpdate(score);
    }
    #endregion

    #region Getters
    public int GetScore()
    {
        return score;
    }

    public int GetQuads()
    {
        return quads;
    } 
    #endregion
}
