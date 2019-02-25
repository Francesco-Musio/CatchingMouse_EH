using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardCanvas : MonoBehaviour
{
    [Header("Leaderboard Options")]
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject leaderboardEntry;

    private LeaderboardManager leaderboardMng;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        leaderboardMng = _lvlMng.GetLeaderboardMng();

        gameObject.SetActive(false);
    }

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

        GetComponentInChildren<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }
    #endregion

    #region OnClick
    public void MainMenu()
    {
        GameManager.OnStateChange(GameState.MainMenu);
    }
    #endregion
}
