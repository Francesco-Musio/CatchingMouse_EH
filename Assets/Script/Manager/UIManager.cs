using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField]
    private GameCanvas gameCanvas;
    [SerializeField]
    private MenuCanvas menuCanvas;
    [SerializeField]
    private FinishCanvas finishCanvas;
    [SerializeField]
    private LeaderboardCanvas leaderboardCanvas;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        if (gameCanvas != null)
            gameCanvas.Init(_lvlMng);

        if (menuCanvas != null)
            menuCanvas.Init(_lvlMng);

        if (finishCanvas != null)
            finishCanvas.Init(_lvlMng);

        if (leaderboardCanvas != null)
            leaderboardCanvas.Init(_lvlMng);

        _lvlMng.OnMenuEnter += HandleOnMenuEnter;
        _lvlMng.OnGameStart += HandleOnGameStart;
        _lvlMng.OnGameEnd += HandleOngameEnd;
        _lvlMng.OnLeaderboard += HandleOnLeaderboard;
    }
    #endregion

    #region Handlers
    private void HandleOnMenuEnter()
    {
        menuCanvas.CheckLeaderboard();

        gameCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }

    private void HandleOnGameStart()
    {
        menuCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
    }

    private void HandleOngameEnd()
    {
        gameCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(true);
    }

    private void HandleOnLeaderboard()
    {
        leaderboardCanvas.LoadLeaderboard();

        menuCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(true);
    }
    #endregion
}
