using UnityEngine;
using System.Collections;

/*
 * Author: Francesco Musio
 * 
 * This class handles the score of the current game
 */

public class ScoreManager : MonoBehaviour
{
    #region Delegates
    public delegate void ScoreEvent(int _score, int _quads);

    /// <summary>
    /// Event to be called every time the score needs to be incremented
    /// </summary>
    public ScoreEvent Score;

    public delegate void ScoreUpdateEvent(int _score);
    /// <summary>
    /// Event called every time the score has been updated
    /// </summary>
    public ScoreUpdateEvent ScoreUpdate;
    #endregion

    [Header("Score Manager Options")]
    [SerializeField]
    // score of the current game
    private int score;
    [SerializeField]
    // quad selected during the current game
    private int quads;

    /// <summary>
    /// Reference to the leaderboard manager
    /// </summary>
    private LeaderboardManager leaderboardMng;

    /// <summary>
    /// Reset score and qauds
    /// </summary>
    private void Setup()
    {
        score = 0;
        quads = 0;
    }

    #region API
    /// <summary>
    /// Initialize this manager
    /// </summary>
    /// <param name="_lvlMng"></param>
    public void Init(LevelManager _lvlMng)
    {
        leaderboardMng = _lvlMng.GetLeaderboardMng();

        _lvlMng.OnGameStart += HandleOnGameStart;
        Score += HandleScore;
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Called every time the game starts
    /// </summary>
    private void HandleOnGameStart()
    {
        Setup();
    }

    /// <summary>
    /// called every time the score has to be updated
    /// </summary>
    /// <param name="_score"></param>
    /// <param name="_quads"></param>
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
