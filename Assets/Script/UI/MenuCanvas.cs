using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuCanvas : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField]
    [Tooltip("Reference to the button that will guide to the leaderboard")]
    public Button leaderboardButton;

    /// <summary>
    /// reference to the leaderboard manager
    /// </summary>
    private LeaderboardManager leaderboardMng;

    #region API
    /// <summary>
    /// Initialize this Canvas
    /// </summary>
    /// <param name="_lvlMng"></param>
    public void Init(LevelManager _lvlMng)
    {
        leaderboardMng = _lvlMng.GetLeaderboardMng();

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Check if a leaderbord exists for this game
    /// </summary>
    public void CheckLeaderboard()
    {
        leaderboardMng.CheckLeaderboard();
        leaderboardButton.gameObject.SetActive(leaderboardMng.GetLeaderboardPresence());
    }
    #endregion

    #region OnClick
    /// <summary>
    /// Start Game on click
    /// </summary>
    public void StartGame()
    {
        GameManager.OnStateChange(GameState.Game);
    }

    /// <summary>
    /// Go to Leaderboard on click
    /// </summary>
    public void Leaderboard()
    {
        GameManager.OnStateChange(GameState.Leaderboard);
    }
    #endregion
}
