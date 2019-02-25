using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseManager))]
[RequireComponent(typeof(QuadManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(UIManager))]
public class LevelManager : MonoBehaviour
{
    #region Delegates
    public Action OnMenuEnter;
    public Action OnGameStart;
    public Action OnGameEnd;
    #endregion

    private MouseManager mouseMng;
    private QuadManager quadMng;
    private ScoreManager scoreMng;
    private UIManager uiMng;
    private TimerManager timerMng;

    #region Start
    public void Init()
    {
        mouseMng = GetComponent<MouseManager>();
        if (mouseMng != null)
            mouseMng.Init(this);

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
    #endregion
}
