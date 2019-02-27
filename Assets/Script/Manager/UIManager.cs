using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField]
    [Tooltip("Reference to the gameCanvas")]
    private GameCanvas gameCanvas;
    [SerializeField]
    [Tooltip("Reference to the menuCanvas")]
    private MenuCanvas menuCanvas;
    [SerializeField]
    [Tooltip("Reference to the finishCanvas")]
    private FinishCanvas finishCanvas;
    [SerializeField]
    [Tooltip("Reference to the leaderboardCanvas")]
    private LeaderboardCanvas leaderboardCanvas;

    #region API
    /// <summary>
    /// Initialize every canvas
    /// </summary>
    /// <param name="_lvlMng"></param>
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
        _lvlMng.OnGameEnd += HandleOnGameEnd;
        _lvlMng.OnLeaderboard += HandleOnLeaderboard;
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Enable the menu canvas and check the leaderboard presence
    /// </summary>
    private void HandleOnMenuEnter()
    {
        menuCanvas.CheckLeaderboard();

        gameCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// enable the game canvas
    /// </summary>
    private void HandleOnGameStart()
    {
        menuCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// enable the finish game canvas
    /// </summary>
    private void HandleOnGameEnd()
    {
        gameCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// enable the leaderboard canvas and reload leaderboard
    /// </summary>
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
