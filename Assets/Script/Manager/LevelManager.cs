using System;
using UnityEngine;
using System.Collections;

/*
 * Author: Francesco Musio
 * 
 * This class initialize all managers and signals a new state of the game.
 */

[RequireComponent(typeof(MouseManager))]
[RequireComponent(typeof(QuadManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(UIManager))]
public class LevelManager : MonoBehaviour
{
    #region Delegates
    /// <summary>
    /// Event called when the state switch to menu
    /// </summary>
    public Action OnMenuEnter;
    /// <summary>
    /// Event called when the state switch to the begin of the game
    /// </summary>
    public Action OnGameStart;
    /// <summary>
    /// Event called when the state switch to the end of the game
    /// </summary>
    public Action OnGameEnd;
    /// <summary>
    /// Event called when the state switch to the leaderboard screen
    /// </summary>
    public Action OnLeaderboard;
    #endregion

    /// <summary>
    /// reference to the MouseManager
    /// </summary>
    private MouseManager mouseMng;
    /// <summary>
    /// reference to the QuadManager
    /// </summary>
    private QuadManager quadMng;
    /// <summary>
    /// reference to the ScoreManager
    /// </summary>
    private ScoreManager scoreMng;
    /// <summary>
    /// reference to the UIManager
    /// </summary>
    private UIManager uiMng;
    /// <summary>
    /// reference to the TimerManager
    /// </summary>
    private TimerManager timerMng;
    /// <summary>
    /// reference to the LeaderboardManager
    /// </summary>
    private LeaderboardManager leaderboardMng;

    #region Start
    /// <summary>
    /// initialize all the managers
    /// </summary>
    public void Init()
    {
        mouseMng = GetComponent<MouseManager>();
        if (mouseMng != null)
            mouseMng.Init(this);

        leaderboardMng = GetComponent<LeaderboardManager>();
        if (leaderboardMng != null)
            leaderboardMng.Init();

        scoreMng = GetComponent<ScoreManager>();
        if (scoreMng != null)
            scoreMng.Init(this);

        quadMng = GetComponent<QuadManager>();
        if (quadMng != null)
            quadMng.Init(this);

        timerMng = GetComponent<TimerManager>();
        if (timerMng != null)
            timerMng.Init(this);

        uiMng = GetComponent<UIManager>();
        if (uiMng != null)
            uiMng.Init(this);
    }
    #endregion

    #region Getters
    public ScoreManager GetScoreMng()
    {
        return scoreMng;
    }

    public MouseManager GetMouseMng()
    {
        return mouseMng;
    }

    public TimerManager GetTimerMng()
    {
        return timerMng;
    }

    public LeaderboardManager GetLeaderboardMng()
    {
        return leaderboardMng;
    }
    #endregion
}
