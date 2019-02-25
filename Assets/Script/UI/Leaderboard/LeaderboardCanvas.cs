using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardCanvas : MonoBehaviour
{
    [Header("Leaderboard Options")]
    [SerializeField]
    [Tooltip("Reference to the Scrool View Content Area")]
    private Transform content;
    [SerializeField]
    [Tooltip("Prefab of a leaderboard Entry")]
    private GameObject leaderboardEntry;

    /// <summary>
    /// reference to the Leaderboard Manager
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

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy all current leaderboardEntry and create new ones based on the current Leaderboard
    /// </summary>
    public void LoadLeaderboard()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        List<LeaderboardEntry> leaderboard = leaderboardMng.GetLeaderboard();

        foreach (LeaderboardEntry _current in leaderboard)
        {
            LeaderboardEntryUI _new = Instantiate(leaderboardEntry, content).GetComponent<LeaderboardEntryUI>();
            _new.Setup(_current.position, _current.name, _current.score);
        }

        // always scroll to top
        GetComponentInChildren<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }
    #endregion

    #region OnClick
    /// <summary>
    /// Go To main menu on click
    /// </summary>
    public void MainMenu()
    {
        GameManager.OnStateChange(GameState.MainMenu);
    }
    #endregion
}
