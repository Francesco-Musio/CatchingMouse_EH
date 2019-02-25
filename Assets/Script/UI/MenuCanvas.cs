using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuCanvas : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField]
    public Button leaderboardButton;

    private LeaderboardManager leaderboardMng;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        leaderboardMng = _lvlMng.GetLeaderboardMng();

        this.gameObject.SetActive(false);
    }

    public void CheckLeaderboard()
    {
        leaderboardMng.CheckLeaderboard();
        leaderboardButton.gameObject.SetActive(leaderboardMng.GetLeaderboardPresence());
    }
    #endregion

    #region OnClick
    public void StartGame()
    {
        GameManager.OnStateChange(GameState.Game);
    }

    public void Leaderboard()
    {
        GameManager.OnStateChange(GameState.Leaderboard);
    }
    #endregion
}
